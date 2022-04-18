using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardShowSrc : MonoBehaviour
{
    public Image Logo;
    public TextMeshProUGUI Attack, Defense, ManaCost;
    public Text Description, Name;
    public GameObject HideObj, HilightedObj;
    [SerializeField] private GameObject BottomBar;

    public void HideCardInfo()
    {
        HideObj.SetActive(true);
    }

    public void UnHideCardInfo()
    {
        HideObj.SetActive(false);
    }

    public void ShowCardInfo(CardModelBase card)
    {
        Logo.sprite = card.Logo;
        Logo.preserveAspect = true;


        Name.text = card.Name;
        ManaCost.text = card.Manacost.ToString();
        HideObj.SetActive(false);
        HilightedObj.SetActive(false);

        if (card is UnitCard unitCard)
        {
            BottomBar.SetActive(true);
            Attack.text = unitCard.Attack.ToString();
            Defense.text = unitCard.Defense.ToString();

        }
        Description.text = card.Description;

    }

    public void HighlightCard()
    {
        HilightedObj.SetActive(true);
    }

    public void DeHighlightCard()
    {
        HilightedObj.SetActive(false);

    }

    public void RefreshData(UnitCard card)
    {
        Attack.text = card.Attack.ToString();
        Defense.text = card.Defense.ToString();

    }

}
