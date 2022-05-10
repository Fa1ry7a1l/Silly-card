using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class Settings : MonoBehaviour
{
    public Dropdown dropdown;
    public TextMeshProUGUI Vsync;

    // Start is called before the first frame update

    string[] resolutions = { "1920x1080", "2560x1440", "3840x2160" };
    void Start()
    {
        Debug.Log("Началааааа");    


        dropdown.ClearOptions();
        dropdown.AddOptions(resolutions.ToList());

        if (PlayerPrefs.HasKey("Resolution"))
        {
            dropdown.value = PlayerPrefs.GetInt("Resolution");
            Debug.Log(dropdown.value + "aaaaaaaaaaaaaaaaaaaaaaa");

        }
        else
            dropdown.value = 0;

        if (!PlayerPrefs.HasKey("VSync"))
        {
            PlayerPrefs.SetInt("VSync", 1);
        }

        SetSettings();
    }

    public void SetQuality()
    {
        string[] res = resolutions[dropdown.value].Split("x");
        Screen.SetResolution(int.Parse(res[0]), int.Parse(res[1]),true);
        Debug.Log(dropdown.value + "bbbbbbbb");
        PlayerPrefs.SetInt("Resolution", dropdown.value);
        PlayerPrefs.Save();

    }

    public void SetSettings()
    {
        SetQuality();
        if (PlayerPrefs.HasKey("VSync"))
        {
            if (PlayerPrefs.GetInt("VSync") == 0)
            {
                Debug.Log("FPS locked at 60 value");
                QualitySettings.vSyncCount = 0;
                Vsync.alpha = 0.5f;
                Vsync.text = "Выключена";
                Application.targetFrameRate = 60;
            }
            else
            {
                Debug.Log("FPS unlocked");
                QualitySettings.vSyncCount = 4;
                Application.targetFrameRate = 500;
                Vsync.alpha = 1f;
                Vsync.text = "Включена";
            }
            PlayerPrefs.Save();

        }
    }


    public void LockFPS ()
    {
        if (PlayerPrefs.HasKey("VSync"))
        {
            if (PlayerPrefs.GetInt("VSync") != 0)
            {
                Debug.Log("FPS locked at 60 value");
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = 60;

                Vsync.alpha = 0.5f;
                Vsync.text = "Выключена";
                PlayerPrefs.SetInt("VSync", 0);
            }
            else
            {
                Debug.Log("FPS unlocked");
                QualitySettings.vSyncCount = 4;
                Application.targetFrameRate = 500;

                Vsync.alpha = 1f;
                Vsync.text = "Включена";
                PlayerPrefs.SetInt("VSync", 1);

            }
            PlayerPrefs.Save();

        }
    }


}
