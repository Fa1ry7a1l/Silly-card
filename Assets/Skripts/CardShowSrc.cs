using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardShowSrc : MonoBehaviour
{
    public Image Logo, ElementLogo;
    public TextMeshProUGUI Name, Attack, Defense, ManaCost;
    public GameObject HideObj, HilightedObj;
    [SerializeField] private GameObject BottomBar;

    public void HideCardInfo()
    {
        HideObj.SetActive(true);
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
            switch (unitCard.Element)
            {
                case Element.Void:
                    ElementLogo.sprite = Resources.Load<Sprite>("Pin/void_element");
                    break;
                case Element.Neutral:
                    ElementLogo.sprite = Resources.Load<Sprite>("Pin/neutral_element");
                    break;
                case Element.Metter:
                    ElementLogo.sprite = Resources.Load<Sprite>("Pin/matter_element");
                    break;
                case Element.Inferno:
                    ElementLogo.sprite = Resources.Load<Sprite>("Pin/inferno_element");
                    break;
                case Element.Time:
                    ElementLogo.sprite = Resources.Load<Sprite>("Pin/time_element");
                    break;

            }
            BottomBar.SetActive(true);
            Attack.text = unitCard.Attack.ToString();
            Defense.text = unitCard.Defense.ToString();
            ElementLogo.preserveAspect = true;

        }
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
