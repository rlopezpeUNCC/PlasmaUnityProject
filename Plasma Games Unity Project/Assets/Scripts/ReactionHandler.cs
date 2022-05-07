using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ReactionHandler : MonoBehaviour {
    [SerializeField]
    ParticleSystem[] cokeMentos;
    [SerializeField]
    ParticleSystem elephantToothpase;
    [SerializeField]
    CinemachineVirtualCamera camCinemachine;
    [SerializeField]
    Material material;
    Camera mainCam;
    CinemachineBasicMultiChannelPerlin shake;
    FormulaHandler formulaHandler;
    float shakeTimer, startingIntensity, shakeTimerTotal;
    Color startColor;
    bool updateColor = false, menuOpened = false, updateMaterial = false;
    float duration;
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
        if (shakeTimer > 0) {
            shakeTimer -= Time.deltaTime;
            shake.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1-shakeTimer/shakeTimerTotal);            
        }
        if (updateColor) {
            mainCam.backgroundColor = Color.Lerp(mainCam.backgroundColor, newColor, Time.deltaTime * .5f);
        }
        if (updateMaterial) {
            material.SetColor("_Color1", Color.Lerp( material.GetColor("_Color1"), newColor, Time.deltaTime * 2f));
        }
    }
    public IEnumerator CokeReaction() {
        if (menuController.state == 2) {
            menuController.ToggleIngredientMenu();
            menuOpened = true;
        }
        ShakeCamera(2f, 2);
        duration = 6;
        StartCoroutine(UpdateBackground());
        // play eruption sfx
        cokeMentos[0].Play();
        yield return new WaitForSeconds(2);
        formulaHandler.ClearFormula(true);
        yield return new WaitForSeconds(1);
        cokeMentos[1].Play();
        yield return new WaitForSeconds(3);
        formulaHandler.reactionStarted = false; 
        if (menuOpened) {
            menuController.ToggleIngredientMenu();     
            menuOpened = false;
        }           
    }
    public IEnumerator ElephantToothpasteReaction() {
        if (menuController.state == 2) {
            menuController.ToggleIngredientMenu();
            menuOpened = true;
        }
        ShakeCamera(2f, 6);
        duration = 6;
        StartCoroutine(UpdateBackground());
        // play eruption sfx
        elephantToothpase.Play();
        yield return new WaitForSeconds(6);
        formulaHandler.ClearFormula(true);
        formulaHandler.reactionStarted = false; 
        if (menuOpened) {
            menuController.ToggleIngredientMenu();      
            menuOpened = false;
        }        
    }
    public IEnumerator BriggsReaction() {
        if (menuController.state == 2) {
            menuController.ToggleIngredientMenu();
            menuOpened = true;
        }
        print("Starting briggs reaction");
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
        yield return new WaitForSeconds(8);
        updateColor = false;
        updateMaterial = false;
        if (menuOpened) {
            menuController.ToggleIngredientMenu();      
            menuOpened = false;
        } 
    }

    public void ShakeCamera(float intensity, float time) {
        shake.m_AmplitudeGain = intensity;
        shakeTimer = time;
        startingIntensity = intensity;
        shakeTimerTotal = time;
    }

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
