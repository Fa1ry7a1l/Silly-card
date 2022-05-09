using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AllCardsDrop : MonoBehaviour
{
    [SerializeField] public DeckCreator DeckCreator;
    [SerializeField] public GameObject Content;

    public void TryDrop(CardLineView card)
    {
        int index = DeckCardManager.DeckCards.FindIndex(x => x.CardName.Equals(card.Name));
        Content.transform.GetChild(index).GetComponent<CardView>().Selected.gameObject.SetActive(false);
        DeckCreator.myDeckCards.RemoveAll(x => x.CardName.Equals(card.Name));
        Destroy(card.gameObject);
    }
}
