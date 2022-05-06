using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {
    [SerializeField]
    Animator animator;
    int state = 1;
    public void ToggleIngredientMenu() {
        print("Button pressed");
        if (state == 1) {
            animator.SetInteger("State", 2);
            state = 2;
        } else {
            animator.SetInteger("State", 1);
            state = 1;
        }
    }
}
