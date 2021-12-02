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

    public void HideCardInfo(Card card)
    {
        Logo.sprite = null;
        Name.text = "";
    }

    public void ShowCardInfo(Card card)
    {
        SelfCard = card;

        //Logo.sprite = card.Logo;
        //Logo.preserveAspect = true;
        Name.text = card.Name;
        Attack.text = card.Attack.ToString();
        Defense.text = card.Defense.ToString();
    }

    /*
    private void Start()
    {
        ShowCardInfo(CardManagerSrc.AllCards[transform.GetSiblingIndex() % CardManagerSrc.AllCards.Count]);
    }*/

}
