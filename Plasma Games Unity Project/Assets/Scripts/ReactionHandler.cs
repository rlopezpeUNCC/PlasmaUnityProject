/*
    Performs all of the actions for each of the reactions.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ReactionHandler : MonoBehaviour {
    [SerializeField]
    ParticleSystem[] cokeMentos; // The particles for the coke + mentos reaction.
    [SerializeField]
    ParticleSystem elephantToothpaste; // The particles for the elephant toothpaste reaction.
    [SerializeField]
    CinemachineVirtualCamera camCinemachine; // The cinemachine virtual cameral component.
    [SerializeField]
    Material material; // The flask liquid material.
    Camera mainCam;
    CinemachineBasicMultiChannelPerlin shake; // The cinemachine shake component.
    FormulaHandler formulaHandler;
    float shakeTimer, startingIntensity, shakeTimerTotal; // These are used for controlling the shake durration and intensity.
    Color startColor; // The origional background starting color.
    bool updateColor = false, menuOpened = false, updateMaterial = false;
    float duration; // The durration of the reactions.
    Color newColor;
    MenuController menuController;    
    void Start() {
        mainCam = FindObjectOfType<Camera>();
        formulaHandler = FindObjectOfType<FormulaHandler>();
        shake = camCinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        startColor = mainCam.backgroundColor;
        menuController = FindObjectOfType<MenuController>();
    }
    void Update() {
        // Shake the camera.
        if (shakeTimer > 0) {
            shakeTimer -= Time.deltaTime;
            shake.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1-shakeTimer/shakeTimerTotal);            
        }
        // Change the background color.
        if (updateColor) {
            mainCam.backgroundColor = Color.Lerp(mainCam.backgroundColor, newColor, Time.deltaTime * .5f);
        }
        // Change the flask liquid color.
        if (updateMaterial) {
            material.SetColor("_Color1", Color.Lerp( material.GetColor("_Color1"), newColor, Time.deltaTime * 2f));
        }
    }
    // The coke + mentos reaction.
    public IEnumerator CokeReaction() {
        // Closes the ingredients menu if it is open.
        if (menuController.state == 2) {
            menuController.ToggleIngredientMenu();
            menuOpened = true;
        }
        ShakeCamera(2f, 2);
        duration = 6;
        StartCoroutine(UpdateBackground());
        // play eruption sfx -------------------------------------- remove once implemented
        // Plays the first particle effect.
        cokeMentos[0].Play();
        yield return new WaitForSeconds(2);
        // Clears the formula.
        formulaHandler.ClearFormula(true);
        yield return new WaitForSeconds(1);
        // Plays the second particle effect.
        cokeMentos[1].Play();
        yield return new WaitForSeconds(3);
        formulaHandler.reactionStarted = false; 
        // Re-opens the ingredients menu if it was open when the reaction started.
        if (menuOpened) {
            menuController.ToggleIngredientMenu();     
            menuOpened = false;
        }           
    }
    // The elephant toothpaste reaction.
    public IEnumerator ElephantToothpasteReaction() {
        // Closes the ingredients menu if it is open.
        if (menuController.state == 2) {
            menuController.ToggleIngredientMenu();
            menuOpened = true;
        }
        ShakeCamera(2f, 6);
        duration = 6;
        StartCoroutine(UpdateBackground());
        // play eruption sfx
        elephantToothpaste.Play();
        yield return new WaitForSeconds(6);
        formulaHandler.ClearFormula(true);
        formulaHandler.reactionStarted = false;
        // Re-opens the ingredients menu if it was open when the reaction started. 
        if (menuOpened) {
            menuController.ToggleIngredientMenu();      
            menuOpened = false;
        }        
    }
    public IEnumerator BriggsReaction() {
        // Closes the ingredients menu if it is open.
        if (menuController.state == 2) {
            menuController.ToggleIngredientMenu();
            menuOpened = true;
        }
        print("Starting briggs reaction");
        // Starts updating the background and flask colors. Switches between red and blue every 3 seconds.
        updateColor = true;
        updateMaterial = true;
        newColor = Color.blue;        
        yield return new WaitForSeconds(3);
        newColor = Color.red;   
        yield return new WaitForSeconds(3);
        newColor = Color.blue;   
        yield return new WaitForSeconds(3);
        newColor = Color.red;    
        yield return new WaitForSeconds(3);
        newColor = Color.blue;   
        yield return new WaitForSeconds(3);
        newColor = Color.red;    
        yield return new WaitForSeconds(3);
        newColor = startColor;
        formulaHandler.reactionStarted = false;
        formulaHandler.ClearFormula(true);
        updateMaterial = false;
        yield return new WaitForSeconds(8);
        // Stops updating the background color
        updateColor = false;

        // Re-opens the ingredients menu if it was open when the reaction started.
        if (menuOpened) {
            menuController.ToggleIngredientMenu();      
            menuOpened = false;
        } 
    }
    // Shakes the camera for a given durration and intensity.
    public void ShakeCamera(float intensity, float time) {
        shake.m_AmplitudeGain = intensity;
        shakeTimer = time;
        startingIntensity = intensity;
        shakeTimerTotal = time;
    }
    // Changes the background color to a random color.
    IEnumerator UpdateBackground() {
        updateColor = true;
        newColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);        
        yield return new WaitForSeconds(duration/3);
        newColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);   
        yield return new WaitForSeconds(duration/3);
        newColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);   
        yield return new WaitForSeconds(duration/3);
        newColor = startColor;
        yield return new WaitForSeconds(3);
        updateColor = false;
    }
}
