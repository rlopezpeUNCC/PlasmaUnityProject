using System;
using System.Collections.Generic;
using UnityEngine;

public class FormulaHandler : MonoBehaviour {
    [SerializeField]
    MeshRenderer materialMesh;
    [SerializeField]
    Material material;
    List<int> currentFormula = new List<int>();
    const int possibleFormulas = 3;
    int[,] formulas;
    bool[] activeFormula;
    public bool reactionStarted = false;
    ReactionHandler reactionHandler;
    void Start() {
        formulas = new int[possibleFormulas, 6] {{6, 5, -1, -1, -1, -1}, {0, 7, 9, 11, 10, -1}, {1, 8, 2, 3, 4, 0}};
        activeFormula = new bool[possibleFormulas] {true, true, true};
        reactionHandler = FindObjectOfType<ReactionHandler>();
    }
    public void AddIngredient(int ingredient) {
        if (reactionStarted)
            return;

        currentFormula.Add(ingredient);
        print("Ingredient Added: " + ingredient);
        CheckFormula(ingredient); 
        UpdateFlask(ingredient);   
    }
    public void ClearFormula(bool overide) {
        if (!overide && reactionStarted)
            return;

        currentFormula = new List<int>();
        materialMesh.enabled = false;
    }
    void CheckFormula(int ingredient) {
        for (int i = 0; i < possibleFormulas; i++) {
            if (activeFormula[i]) {
                if (!FindInCurrentFormula(ingredient, i)) {
                    activeFormula[i] = false;
                }
            }
        }

        if (activeFormula[0] && currentFormula.Count == 2) {
            StartReaction(0);
        } else if (activeFormula[1] && currentFormula.Count == 5) {
            StartReaction(1);
        } else if (activeFormula[2] && currentFormula.Count == 6) {
            StartReaction(2);
        }
    } 
    void StartReaction(int reaction) {
        reactionStarted = true;
        switch(reaction) {
            case 0:
                StartCoroutine(reactionHandler.CokeReaction());
                break;
            case 1:
                StartCoroutine(reactionHandler.ElephantToothpasteReaction());
                break;
            case 2:
                StartCoroutine(reactionHandler.BriggsReaction());
                break;
        }
    }  
    void UpdateFlask(int ingredient) {
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
    bool FindInCurrentFormula(int value, int index) {
        for (int i = 0; i < 6; i++) {
            if (formulas[index, i] != -1) {
                if (formulas[index, i] == value)
                    return true;
            }
        }
        return false;
    }
}
