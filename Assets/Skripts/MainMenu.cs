using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

    public static void Play()
    {
        SceneManager.LoadScene(1);
    }

    public static void OpenDeck()
    {
        SceneManager.LoadScene(3);
    }

    public void ShowPannel(GameObject Obj)
    {
        Obj.SetActive(true);
    }

    public void HidePannel(GameObject Obj)
    {
        Obj.SetActive(false);
    }

    public static void Exit()
    {
        Application.Quit();
    }

    public static void Tutorial()
    {
        SceneManager.LoadScene(2);
    }
}
