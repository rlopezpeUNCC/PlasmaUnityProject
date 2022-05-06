using UnityEngine;
using UnityEngine.UI;

public class IngredientsMenu : MonoBehaviour {
    [SerializeField]
    GameObject[] menus;
    [SerializeField]
    Animator animator;
    [SerializeField]
    Text title;
    string[] titles = {"Chemicals", "Food", "Home"};
    int state = 0;

    public void ToggleMenu(int menu) {
        if (menu == state)
            return;

        menus[state].SetActive(false);
        menus[menu].SetActive(true);        

        title.text = titles[menu];

        state = menu;

        animator.SetInteger("State", state);
    }
}
