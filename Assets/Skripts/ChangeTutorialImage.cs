using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeTutorialImage : MonoBehaviour
{
    [SerializeField] private List<Sprite> images;
    [SerializeField] Image image;
    [SerializeField] Button prev;
    [SerializeField] Button next;
    int position;

    public void Next()
    {
        if (position + 1 < images.Count)
        {
            position += 1;
            image.sprite = images[position];
        }
        if (position == images.Count - 1)
        {
            next.enabled = false;
            next.gameObject.SetActive(false);
        }
        if (position == 1)
        {
            prev.enabled = true;
            prev.gameObject.SetActive(true);
        }
    }


    public void Previous()
    {
        if (position - 1 >= 0)
        {
            position -= 1;
            image.sprite = images[position];
        }
        if (position == 0)
        {
            prev.enabled = false;
            prev.gameObject.SetActive(false);
        }
        if (position == images.Count - 2)
        {
            next.enabled = true;
            next.gameObject.SetActive(true);
        }
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
