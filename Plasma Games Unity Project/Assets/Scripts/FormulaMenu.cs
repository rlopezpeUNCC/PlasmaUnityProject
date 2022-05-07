using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormulaMenu : MonoBehaviour {
    [SerializeField]
    Text cokeMentosText, elephantText, briggsText, currentFormulaText;
    string[] ingredients = new string[12] {"H202", "KIO3", "H2SO4", "CH2(COOH)", "MnSO4",
    "Mentos", "Coke", "Yeast", "Water (distilled)", "Water (warm)", "Food coloring", "Dish soap"};
    void Start() {
        
    }

    public void UpdateCurrentFormula(int ingredient) {
        if ( currentFormulaText.text != "")
            currentFormulaText.text += " + ";
            
        currentFormulaText.text += ingredients[ingredient];
    }
    public void ClearCurrentFormula() {
        currentFormulaText.text = "";
    }
}
