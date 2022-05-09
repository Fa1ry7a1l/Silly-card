using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckCreator : MonoBehaviour
{
    [HideInInspector] public List<DeckCard> myDeckCards = new List<DeckCard>();
    [SerializeField] private GameObject MyDeck;
    [SerializeField] private GameObject DeckTable;

    [SerializeField] private GameObject CardPref;
    [SerializeField] private GameObject CardLinePref;



    // Start is called before the first frame update
    void Start()
    {
        DeckCardManager.GenerateDeck();
        //myDeckCards = CardManagerSrc.AllCards;

        myDeckCards.Add(DeckCardManager.DeckCards[0]);
        myDeckCards.Add(DeckCardManager.DeckCards[2]);
        myDeckCards.Add(DeckCardManager.DeckCards[3]);
        myDeckCards.Add(DeckCardManager.DeckCards[4]);

        //тут нужно сделать зарузку текущей колоды или стандартной, если кастомной нет
        LoadDeck();

        LoadAllCards();
        LoadLineCards();


    }

    private void LoadDeck()
    {
        //провести загрузку текущей деки
    }

    private void LoadAllCards()
    {
        for(int i = 0;i<DeckCardManager.DeckCards.Count;i++)
        {
            GameObject card = Instantiate(CardPref, DeckTable.transform, false);
            card.GetComponent<CardView>().Init(DeckCardManager.DeckCards[i], myDeckCards.Find(x => x.CardName.Equals(DeckCardManager.DeckCards[i].CardName)) != null, i);
            
        }
    }

    private void LoadLineCards()
    {
        foreach(DeckCard deckCard in myDeckCards)
        {
            GameObject card = Instantiate(CardLinePref, MyDeck.transform, false);
            card.GetComponent<CardLineView>().Init(deckCard);
        }
    }


}
