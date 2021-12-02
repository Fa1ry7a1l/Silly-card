using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardShowSrc : MonoBehaviour
{
    public Card SelfCard;
    public Image Logo;
    public TextMeshProUGUI Name, Attack, Defense;
    public GameObject HideObj;

    public void HideCardInfo(Card card)
    {
        SelfCard = card;
        HideObj.SetActive(true);
    }

    public void ShowCardInfo(Card card)
    {
        SelfCard = card;

        //Logo.sprite = card.Logo;
        //Logo.preserveAspect = true;
        Name.text = card.Name;
        Attack.text = card.Attack.ToString();
        Defense.text = card.Defense.ToString();
        HideObj.SetActive(false);
    }

    /*
    private void Start()
    {
        ShowCardInfo(CardManagerSrc.AllCards[transform.GetSiblingIndex() % CardManagerSrc.AllCards.Count]);
    }*/

}
