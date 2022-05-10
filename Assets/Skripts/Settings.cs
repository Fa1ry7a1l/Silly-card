using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Settings : MonoBehaviour
{
    public Dropdown dropdown;

    // Start is called before the first frame update

    string[] resolutions = { "1920x1080", "2560x1440", "3840x2160" };
    void Start()
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(resolutions.ToList());

        if (PlayerPrefs.HasKey("Resolution"))
            dropdown.value = PlayerPrefs.GetInt("Resolution");
        else
            dropdown.value = 0;

    }

    public void SetQuality()
    {
        Debug.Log("Устанавливаем разрешение");
        string[] res = resolutions[dropdown.value].Split("x");
        Screen.SetResolution(int.Parse(res[0]), int.Parse(res[1]),true);
    }
}
