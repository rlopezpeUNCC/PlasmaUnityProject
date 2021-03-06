/*
    Controlls the ingredients menu navigation and interactions.
*/
using UnityEngine;
using UnityEngine.UI;

public class IngredientsMenu : MonoBehaviour {
    [SerializeField]
    GameObject[] menus;
    [SerializeField]
    Animator animator;
    [SerializeField]
    Text title;
    [SerializeField]
    GameObject mouseItem;
    AudioManager audioManager;
    string[] titles = {"Chemicals", "Food", "Home", "Formulas"}; // The titles for each sub menu.
    int state = 0;
    void Start() {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Naviagtes between the sub menus in the ingredients menu.
    public void ToggleMenu(int menu) {
        // Returns if the requested menu is already open.
        if (menu == state) 
            return;
        // Disables the previously opened menu and enables the requested one.
        menus[state].SetActive(false); 
        menus[menu].SetActive(true);        
        // Updates the tile
        title.text = titles[menu];

        state = menu;
        // Plays the animation for a menu opening.
        animator.SetInteger("State", state);
        audioManager.Play("ButtonClicked2");
    }
    // Updates the selected ingredient.
    public void IngredientSelected(int ingredient) {
        audioManager.Play("MushroomHit");
        mouseItem.GetComponent<MouseIngredient>().IngredientSelected(ingredient);
    }
}
