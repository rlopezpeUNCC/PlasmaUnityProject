/* 
    
*/
using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using UnityEngine.UI;
public class SetVolume : MonoBehaviour {
    [SerializeField]
    Text placeholderText, volumeText;
    [SerializeField]
    AudioMixer mixer;
    [SerializeField]
    string mixerName = "";
    [SerializeField]
    Slider slider;
    void Start() {
        float value = PlayerPrefs.GetFloat(mixerName);
        //print(mixerName + " " + value);
        if (value == 0) {
            value = .3f;
        }         
        slider.value = value; 
        SetLevel(value);
    }

    public void SetLevel(System.Single vol) {
        mixer.SetFloat(mixerName, Mathf.Log10(vol)*20);
        //print("saving" + mixerName + " " + vol);
        string volTx = (vol*100).ToString("F0");
        placeholderText.text = volTx;
        volumeText.text = volTx;

        PlayerPrefs.SetFloat(mixerName, vol);
    }
     public void SetLevelText(string volTx) {
        if (volTx == "") {
            return;
        }
        float vol = float.Parse(volTx)/100;
        mixer.SetFloat(mixerName, Mathf.Log10(vol)*20);
        slider.value = vol;

        placeholderText.text = volTx;

        PlayerPrefs.SetFloat(mixerName, vol);
    }
}
