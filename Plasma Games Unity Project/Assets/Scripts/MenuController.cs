/*
    Toggles menus open/close.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {
    [SerializeField]
    Animator animatorIngredients;
    public int state = 1;
    AudioManager audioManager;
    void Start() {
        audioManager = FindObjectOfType<AudioManager>();
    }
    // Opens/closes the ingredients menu
    public void ToggleIngredientMenu() {        
        if (state == 1) {
            animatorIngredients.SetInteger("State", 2);
            audioManager.Play("SliderOpened");
            state = 2;
        } else {
            animatorIngredients.SetInteger("State", 1);
            audioManager.Play("SliderClosed");
            state = 1;
        }
    }
}
