/*
    Updates the main menu background color.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBackground : MonoBehaviour {
    Camera mainCam;
    Color newColor;
    void Start() {
        mainCam = FindObjectOfType<Camera>();
        StartCoroutine(UpdateBackground());
    }
    void Update() {
        // Lerps between the current background color and the target color
        mainCam.backgroundColor = Color.Lerp(mainCam.backgroundColor, newColor, Time.deltaTime * .5f);        
    }
    // Changes the main menu background target color every 3 seconds to a random color
    IEnumerator UpdateBackground() {
        newColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);        
        yield return new WaitForSeconds(3);
        StartCoroutine(UpdateBackground());
    }
}
