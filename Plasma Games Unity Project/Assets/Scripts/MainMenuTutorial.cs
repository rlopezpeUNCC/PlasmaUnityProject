/* 
    Main menu interactivity/tutorial.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTutorial : MonoBehaviour {
    [SerializeField]
    GameObject chemical, arrow1, arrow2;
    // The chemical was clicked, hide the button and change the arrows 
    public void DisableChemical() {
        chemical.SetActive(false);
        arrow1.SetActive(false);
        arrow2.SetActive(true);
    }
    // The player dropped the chemical out of bounds, re-enable the button
    public void EnableChemical() {
        chemical.SetActive(true);
        arrow1.SetActive(true);
        arrow2.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other) {
        EnableChemical();
    }
}
