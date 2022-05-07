using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormulaMenu : MonoBehaviour {
    [SerializeField]
    Text[] formulaTexts;
    [SerializeField]
    Text currentFormulaText;
    string[] ingredients = new string[12] {"H202", "KIO3", "H2SO4", "CH2(COOH)", "MnSO4",
    "Mentos", "Coke", "Yeast", "Water (distilled)", "Water (warm)", "Food coloring", "Dish soap"};
    public bool[] discoveredFormulas;
    FormulaHandler formulaHandler;
    void Start() {
        formulaHandler = FindObjectOfType<FormulaHandler>();
        discoveredFormulas = new bool[formulaTexts.Length];
        for (int i = 0; i < formulaTexts.Length; i++) {
            discoveredFormulas[i] = false;
        }
    }

    public void UpdateCurrentFormula(int ingredient) {
        if ( currentFormulaText.text != "")
            currentFormulaText.text += " + ";
            
        currentFormulaText.text += ingredients[ingredient];
        UpdateForumlas(ingredient);
    }

    public void ClearCurrentFormula() {
        currentFormulaText.text = "";
        ResetFormulas();
    }

    void ResetFormulas() {
        for (int j = 0; j < formulaTexts.Length; j++) {
            if (!discoveredFormulas[j]) {
                int length = formulaTexts[j].text.Split('+').Length;
                formulaTexts[j].text = "";
                for (int i = 0; i < length; i++) {
                    formulaTexts[j].text += "???";
                    if (i != length-1)
                        formulaTexts[j].text += " + ";
                }
            }
        }
    }

    void UpdateForumlas(int ingredient) {
        for (int i = 0; i < formulaHandler.GetNumberOfFormulas(); i++) {
            if (formulaHandler.FindInCurrentFormula(ingredient, i)) {
                formulaTexts[i].text = ReplaceFirst(formulaTexts[i].text, "???", "<color=green>" + ingredients[ingredient] + "</color>");
            } 
        }
    }

    public string ReplaceFirst(string text, string search, string replace) {
    int pos = text.IndexOf(search);
    if (pos < 0) {
        return text;
    }
    return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
    }
}
