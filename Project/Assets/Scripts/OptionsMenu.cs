using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public float backgroundVolume, sfxVolume, volume;
    public bool postProcessingOn;

    [SerializeField]
    private Toggle postProcessingToggle, fullscreenToggle;

    //[SerializeField]
    //private Slider volumeSliderBG, volumeSliderSFX, volumeSliderMaster;
    //[SerializeField]
    //private AudioMixer mixer;

    [SerializeField]
    private Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    [SerializeField]
    private InputField nameSelect;

    [SerializeField]
    private GameObject optionsMenu;

    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;
        List<Resolution> tempRes = new List<Resolution>();
        
        int highestRefRate = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            if(resolutions[i].refreshRate > highestRefRate)
            {
                highestRefRate = resolutions[i].refreshRate;
            }
        }

        for (int i = 0; i < resolutions.Length; i++)
        {
            float temp = (float)resolutions[i].width / (float)resolutions[i].height;
            temp = Mathf.Round(temp * 100f);

            float goodRes = (16f / 9f);
            goodRes = Mathf.Round(goodRes * 100f);

            Debug.Log(temp + "  " + goodRes);
            if (temp == goodRes)
            {
                tempRes.Add(resolutions[i]);
            }
        }
        resolutions = tempRes.ToArray();

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResIndex = 0;

        if (!PlayerPrefs.HasKey("fullscreen"))
        {
            PlayerPrefs.SetInt("fullscreen", 1);
            PlayerPrefs.SetInt("resX", Screen.currentResolution.width);
            PlayerPrefs.SetInt("resY", Screen.currentResolution.width);
        }

        if (!PlayerPrefs.HasKey("playername"))
        {            
            PlayerPrefs.SetString("playername", "Unknown Assassin");
            GetComponent<MainMenu>().ToggleNamePanel();
        }

        int resX = PlayerPrefs.GetInt("resX");
        int resY = PlayerPrefs.GetInt("resY");

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == resX
                && resolutions[i].height == resY)
            {
                currentResIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();

        //if (!PlayerPrefs.HasKey("volume"))
        //{
        //    PlayerPrefs.SetFloat("volume", 1f);
        //    PlayerPrefs.SetFloat("volumeBG", 1f);
        //    PlayerPrefs.SetFloat("volumeSFX", 1f);
        //    PlayerPrefs.SetInt("postprocess", 1);
        //    PlayerPrefs.Save();
        //}

        if (!PlayerPrefs.HasKey("postprocess"))
        {
        //    PlayerPrefs.SetFloat("volume", 1f);
        //    PlayerPrefs.SetFloat("volumeBG", 1f);
        //    PlayerPrefs.SetFloat("volumeSFX", 1f);
            PlayerPrefs.SetInt("postprocess", 1);
           PlayerPrefs.Save();
        }

        //volume = PlayerPrefs.GetFloat("volume");
        //volumeSliderMaster.value = volume;
        //SetVolume(volume);
        //backgroundVolume = PlayerPrefs.GetFloat("volumeBG");
        //volumeSliderBG.value = backgroundVolume;
        //SetBGVolume(backgroundVolume);
        //sfxVolume = PlayerPrefs.GetFloat("volumeSFX");
        //volumeSliderSFX.value = sfxVolume;
        //SetSFXVolume(sfxVolume);

        postProcessingOn = PlayerPrefs.GetInt("postprocess") == 1 ? true : false;
        postProcessingToggle.isOn = postProcessingOn;
        //postProcessingToggle.onValueChanged.AddListener(delegate { SetPostProcessing(postProcessingToggle); });

        bool fullscreenOn = PlayerPrefs.GetInt("fullscreen") == 1 ? true : false;
        fullscreenToggle.isOn = fullscreenOn;
        Screen.fullScreen = fullscreenOn;
    }

    public void SetPostProcessing(bool value)
    {
        AkSoundEngine.PostEvent("ui_wood_panel_options_checkbox", gameObject, gameObject);
        postProcessingOn = value;
        
        PlayerPrefs.SetInt("postprocess", value ? 1 : 0);

        FindObjectOfType<CameraPostProcessingManager>().UpdatePostProcessing();
    }

    //public void SetVolume(float value)
    //{
    //    PlayerPrefs.SetFloat("volume", value);
    //    mixer.SetFloat("masterVol", Mathf.Log10(value) * 20);
    //}

    //public void SetBGVolume(float value)
    //{
    //    PlayerPrefs.SetFloat("volumeBG", value);
    //    mixer.SetFloat("bgVol", Mathf.Log10(value) * 20);
    //}

    //public void SetSFXVolume(float value)
    //{
    //    PlayerPrefs.SetFloat("volumeSFX", value);
    //    mixer.SetFloat("sfxVol", Mathf.Log10(value) * 20);
    //}

    public void SetFullscreen(bool value)
    {
        AkSoundEngine.PostEvent("ui_wood_panel_options_checkbox", gameObject, gameObject);
        Screen.fullScreen = value;
        PlayerPrefs.SetInt("fullscreen", value ? 1 : 0);
    }

    public void SetResolution(int value)
    {
        Resolution res = resolutions[value];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        PlayerPrefs.SetInt("resX", res.width);
        PlayerPrefs.SetInt("resY", res.height);
    }

    public void SetName(string value)
    {
        PlayerPrefs.SetString("playername", value);
    }

    public void ToggleOptions()
    {
        if (optionsMenu.activeInHierarchy)
        {
            optionsMenu.SetActive(false);
        }
        else
        {
            optionsMenu.SetActive(true);
        }
    }

    public void ShowCurrentName()
    {
        if(nameSelect != null)
        {
            nameSelect.text = PlayerPrefs.GetString("playername");
        }
    }
}
