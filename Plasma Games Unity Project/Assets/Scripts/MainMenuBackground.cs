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
        mainCam.backgroundColor = Color.Lerp(mainCam.backgroundColor, newColor, Time.deltaTime * .5f);        
    }
    IEnumerator UpdateBackground() {
        newColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);        
        yield return new WaitForSeconds(3);
        StartCoroutine(UpdateBackground());
    }
}
