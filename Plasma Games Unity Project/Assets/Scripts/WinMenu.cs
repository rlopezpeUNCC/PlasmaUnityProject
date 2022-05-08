/*
    Win menu button functionality.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour { 
    // Closes the win menu.
    public void Continue() {
        gameObject.SetActive(false);
    }
    // Returns to the main menu.
    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
    // Closes the application
    public void Quit() {
        Application.Quit();
    }
}
