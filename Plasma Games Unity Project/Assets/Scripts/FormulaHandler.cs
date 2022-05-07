/*
    Keeps track of the current inputed formula, and triggers reaction events once a 
    known formula is inputed.

    Also facilitates the ability to add ingredients to the formula.
*/
using System;
using System.Collections.Generic;
using UnityEngine;

public class FormulaHandler : MonoBehaviour {
    [SerializeField]
    MeshRenderer materialMesh; // The mesh of the flask liquid
    [SerializeField]
    Material material; // The materia of the liquid in the flask
    List<int> currentFormula = new List<int>(); // The current inputed formula
    const int possibleFormulas = 3; // The amount of possible reactions
    int[,] formulas; // An array containing all of the possible formulas. An integer is assigned to each of the ingredients
    bool[] activeFormula; // Keeps track of which formulas are possible
    public bool reactionStarted = false; // If a reaction has started. Used to prevent new chemicals from being added.
    ReactionHandler reactionHandler; 
    FormulaMenu FormulaMenu;
    void Start() {
                                                //   Coke + menutos         Elphant toothpaste      Briggs clock
        formulas = new int[possibleFormulas, 6] {{6, 5, -1, -1, -1, -1}, {0, 7, 9, 11, 10, -1}, {1, 8, 2, 3, 4, 0}};
        activeFormula = new bool[possibleFormulas] {true, true, true};
        reactionHandler = FindObjectOfType<ReactionHandler>();
        FormulaMenu = FindObjectOfType<FormulaMenu>();
    }
    // Adds a knew ingredient to the list.
    public void AddIngredient(int ingredient) {
        // Prevents the addition of new ingredients if the reaction has already started.
        if (reactionStarted) 
            return;

        currentFormula.Add(ingredient);
        FormulaMenu.UpdateCurrentFormula(ingredient);
        print("Ingredient Added: " + ingredient);
        CheckFormula(ingredient); 
        UpdateFlask(ingredient);   
    }
    // Clears the current formula.
    public void ClearFormula(bool overide) {
        // Returns if the reaction has already started, can be overriden by some reactions that can be cleared before 100% completion.
        if (!overide && reactionStarted)
            return;

        currentFormula.Clear();
        materialMesh.enabled = false;
        FormulaMenu.ClearCurrentFormula();
        for (int i = 0; i < possibleFormulas; i++) {
            activeFormula[i] = true;
        }
    }
    // Checks if a reaction has been completed.
    void CheckFormula(int ingredient) {
        // Itterates through the possible formulas and compares it to the newly added ingredient.
        for (int i = 0; i < possibleFormulas; i++) {
            // If the new ingredient is not in the current formula, update active formals to reflect this.
            if (activeFormula[i]) {
                if (!FindInCurrentFormula(ingredient, i)) {
                    activeFormula[i] = false;
                }
            }
        }
        // Checks if any of the formulas has been completed, and starts the reaction if it has.
        if (activeFormula[0] && currentFormula.Count == 2) {
            StartCoroutine(reactionHandler.CokeReaction());
            FormulaMenu.discoveredFormulas[0] = true;

        } else if (activeFormula[1] && currentFormula.Count == 5) {
            reactionStarted = true;
            StartCoroutine(reactionHandler.ElephantToothpasteReaction());
            FormulaMenu.discoveredFormulas[1] = true;
            reactionStarted = true;
        } else if (activeFormula[2] && currentFormula.Count == 6) {
            reactionStarted = true;
            StartCoroutine(reactionHandler.BriggsReaction());
            FormulaMenu.discoveredFormulas[2] = true;
        }
    } 
    // Updates the flask liquid color to reflect the knewly added ingredient.  
    void UpdateFlask(int ingredient) {
        // If this is the first ingredient added, enable the mesh.
        if (currentFormula.Count == 1) {
            materialMesh.enabled = true;
        }

        switch(ingredient) {
            case 0: // Hydrogen peroxide
                material.SetColor("_Color1", Color.white);
                break;
            case 1: // Potassium iodate
                material.SetColor("_Color1", Color.grey);
                break;
            case 2: // Sulfuric acid
                material.SetColor("_Color1", Color.magenta);
                break;
            case 3: // Malonic acid
                material.SetColor("_Color1", Color.white);
                break;
            case 4: // Manganese sulfate monohydrate
                material.SetColor("_Color1", Color.yellow);
                break;
            case 5: // Mentos
                material.SetColor("_Color1", new Color(0.1792453f, 0.06490906f, 0.002536485f));
                break;
            case 6: // Coke
                material.SetColor("_Color1", new Color(0.1792453f, 0.06490906f, 0.002536485f));
                break;
            case 7: // Yeast
                material.SetColor("_Color1", Color.yellow);
                break;
            case 8: // Water (distilled)
                Color col = material.GetColor("_Color1");
                col.r += .1f;
                col.b += .1f;
                col.g += .1f;
                material.SetColor("_Color1", col);
                break;
            case 9: // Water (warm)
                Color col2 = material.GetColor("_Color1");
                col2.r += .1f;
                col2.b += .1f;
                col2.g += .1f;
                material.SetColor("_Color1", col2);
                break;
            case 10: // Food coloring
                material.SetColor("_Color1", Color.green);
                break;
            case 11: // Dish soap 
                material.SetColor("_Color1", Color.blue);
                break;
        }
    }
    // Loops through the given formula (index) and returns if a value was found.
    public bool FindInCurrentFormula(int value, int index) {
        for (int i = 0; i < 6; i++) {
            if (formulas[index, i] != -1) {
                if (formulas[index, i] == value)
                    return true;
            }
        }
        return false;
    }

    public int GetNumberOfFormulas() {
        return possibleFormulas;
    }
}
