/*
    Updates and handles the formula menu with new information when a ingredient is added or the flask is reset.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormulaMenu : MonoBehaviour {
    [SerializeField]
    Text[] formulaTexts;
    [SerializeField]
    Text currentFormulaText;
    // The names of all of the ingredients
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
    // Updates the currently inputted formula
    public void UpdateCurrentFormula(int ingredient) {
        if ( currentFormulaText.text != "")
            currentFormulaText.text += " + ";
            
        currentFormulaText.text += ingredients[ingredient];
        UpdateForumlas(ingredient);
    }
    // Clears the current formula
    public void ClearCurrentFormula() {
        currentFormulaText.text = "";
        currentFormulaText.color = Color.white;
        ResetFormulas();
    }
    // Resets the possible formulas back to their ??? state
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
    // Updates the possible formulas with ingredients that they contain
    void UpdateForumlas(int ingredient) {
        for (int i = 0; i < formulaHandler.GetNumberOfFormulas(); i++) {
            if (formulaHandler.FindInCurrentFormula(ingredient, i)) {
                formulaTexts[i].text = ReplaceFirst(formulaTexts[i].text, "???", "<color=green>" + ingredients[ingredient] + "</color>");
            } 
        }
    }
    // Replaces the first occurance of a string (search) in a string (text) with a new string (replace), and returns the new string
    public string ReplaceFirst(string text, string search, string replace) {
        int pos = text.IndexOf(search);
        if (pos < 0) {
            return text;
        }
        return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
    }

    public void UnknownFormula() {
        currentFormulaText.text = "(Unknown!) " + currentFormulaText.text;
        currentFormulaText.color = Color.red;
    }
}
