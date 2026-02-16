using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
	//objects
	public AudioMixer audioMixer;
	Resolution[] resolutions;
	public Dropdown resolutionDropdown;
	
	//volume
	public void SetVolume(float volume){
		audioMixer.SetFloat("volume", volume);
	}
	
	//graphics
	public void SetQuality (int qualityIndex){
		QualitySettings.SetQualityLevel(qualityIndex);
	}
	
	//fullscreen
	public void SetFullscreen(bool isFullScreen){
		Screen.fullScreen = isFullScreen;
	} 
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       resolutions =  Screen.resolutions;
	   resolutionDropdown.ClearOptions();
	   
	   List<string> options = new List<string>();
	   int currentResolutionIndex = 0;
	   for(byte i = 0; i < resolutions.Length; i++)
	   {
		   string option = resolutions[i].width + " x " + resolutions[i].height;
		   options.Add(option);
		   
		   if(resolutions[i].width == Screen.currentResolution.width 
			&& resolutions[i].height == Screen.currentResolution.height){
			   currentResolutionIndex = i;
		   }
	   }
		resolutionDropdown.AddOptions(options);
		resolutionDropdown.value = currentResolutionIndex;
		resolutionDropdown.RefreshShownValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
