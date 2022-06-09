using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.IO;

public class OptionHandler : MonoBehaviour
{
    #region Audio
    public AudioMixer masterAudio;
    public string currentSlider;
    public Slider tempSlider;

    public void GetSlider(Slider slider)
    {
        tempSlider = slider;
    }

    public void MuteToggle(bool isMuted)
    {
        if (isMuted)
        {
            masterAudio.SetFloat(currentSlider, -80);
            tempSlider.interactable = false;
        }
        else
        {
            masterAudio.SetFloat(currentSlider, tempSlider.value);
            tempSlider.interactable = true;
        }
    }

    public void CurrentSlider(string sliderName)
    {
        currentSlider = sliderName;
    }
    public void ChangeVolume(float volume)
    {
        masterAudio.SetFloat(currentSlider, volume);
    }

    #endregion
    #region Quality
    public void Quality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    #endregion
    #region Resolution
    public Resolution[] resolutions;
    public Dropdown resDropdown;

    public void FullscreenToggle(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    private void Start()
    {
        //update resolution options
        resolutions = Screen.resolutions;
        resDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {//find current resolution and set it to current option
                currentResolutionIndex = i;
            }
        }
        resDropdown.AddOptions(options);
        resDropdown.value = currentResolutionIndex;
        resDropdown.RefreshShownValue();
        //load options from menu on opening game scene
        LoadOptions();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
    #endregion
    #region Saving
    public string path = Path.Combine(Application.streamingAssetsPath, "Save/Options.txt");
    public GameObject masterSlider, musicSlider, sfxSlider;
    public GameObject masterToggle, musicToggle, sfxToggle, fullToggle;
    public Dropdown qDropdown;
    //save options to file
    public void SaveOptions()
    {
        //links writer to file
        StreamWriter writer = new StreamWriter(path, false);//true = add, false = overwrite
        writer.WriteLine("This file has something in it");

        writer.WriteLine(masterSlider.GetComponent<Slider>().value);
        writer.WriteLine(musicSlider.GetComponent<Slider>().value);
        writer.WriteLine(sfxSlider.GetComponent<Slider>().value);

        writer.WriteLine(masterToggle.GetComponent<Toggle>().isOn);
        writer.WriteLine(musicToggle.GetComponent<Toggle>().isOn);
        writer.WriteLine(sfxToggle.GetComponent<Toggle>().isOn);
        writer.WriteLine(fullToggle.GetComponent<Toggle>().isOn);

        writer.WriteLine(qDropdown.value);
        writer.WriteLine(resDropdown.value);

        //writing is done
        writer.Close();
    }
    //load options from file
    public void LoadOptions()
    {
        //Read text from file
        StreamReader reader = new StreamReader(path);
        //reference to the line we are reading
        //first line confirms file has something in it, does not contain actual data
        if (reader.ReadLine() == null)
        {
            Debug.Log("Error! No save file detected!");
            return;
        }
        float value;
        //mastervolume, volume is also changed just in case
        value = float.Parse(reader.ReadLine());
        masterAudio.SetFloat("masterVolume", value);
        masterSlider.GetComponent<Slider>().value = value;
        //musicvolume
        value = float.Parse(reader.ReadLine());
        masterAudio.SetFloat("musicVolume", value);
        musicSlider.GetComponent<Slider>().value = value;
        //sfxvolume
        value = float.Parse(reader.ReadLine());
        masterAudio.SetFloat("sfxVolume", value);
        sfxSlider.GetComponent<Slider>().value = value;

        //sets toggle to correct value, also sets actual values just in case
        masterToggle.GetComponent<Toggle>().isOn = bool.Parse(reader.ReadLine());
        if (masterToggle.GetComponent<Toggle>().isOn)
        {
            masterAudio.SetFloat("masterVolume", 0);
        }
        musicToggle.GetComponent<Toggle>().isOn = bool.Parse(reader.ReadLine());
        if (masterToggle.GetComponent<Toggle>().isOn)
        {
            masterAudio.SetFloat("musicVolume", 0);
        }
        sfxToggle.GetComponent<Toggle>().isOn = bool.Parse(reader.ReadLine());
        if (masterToggle.GetComponent<Toggle>().isOn)
        {
            masterAudio.SetFloat("sfxVolume", 0);
        }
        fullToggle.GetComponent<Toggle>().isOn = bool.Parse(reader.ReadLine());
        FullscreenToggle(fullToggle.GetComponent<Toggle>().isOn);

        //sets dropdowns to the correct index, also changes actual values just in case
        qDropdown.value = int.Parse(reader.ReadLine());
        Quality(qDropdown.value);
        resDropdown.value = int.Parse(reader.ReadLine());
        SetResolution(qDropdown.value);

        //reading is done
        reader.Close();
    }
    #endregion
}
