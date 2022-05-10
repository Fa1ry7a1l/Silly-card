using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MyDeckDrop:MonoBehaviour
{
    [SerializeField] DeckCreator DeckCreator;
    [SerializeField] GameObject CardLinePref;

    public void TryDrop(CardView card)
    {
        if(DeckCreator.myDeckCards.Count >=10)
        {
            card.PlaceBack();
            return;
        }

        else
        {
            card.transform.SetParent(card.MainParent);
            card.transform.SetSiblingIndex(card.position);
            card.Selected.gameObject.SetActive(true);
            DeckCard cardBody = DeckCardManager.DeckCards.Find(x => x.CardName.Equals(card.Name));
            if(cardBody != null)
            {
                GameObject cardLineView = Instantiate(CardLinePref, transform, false);
                cardLineView.GetComponent<CardLineView>().Init(cardBody);
                DeckCreator.myDeckCards.Add(cardBody);
            }
        }
        DeckCreator.UpdateCounter();

    }
}
