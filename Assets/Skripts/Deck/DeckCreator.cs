using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeckCreator : MonoBehaviour
{
    [HideInInspector] public List<DeckCard> myDeckCards = new List<DeckCard>();
    [SerializeField] private GameObject MyDeck;
    [SerializeField] private GameObject DeckTable;
    [SerializeField] public TextMeshProUGUI Counter;
    [SerializeField] private GameObject CardPref;
    [SerializeField] private GameObject CardLinePref;



    // Start is called before the first frame update
    void Start()
    {
        if(DeckCardManager.DeckCards.Count == 0)
        DeckCardManager.GenerateDeck();
        //myDeckCards = CardManagerSrc.AllCards;

        //тут нужно сделать зарузку текущей колоды или стандартной, если кастомной нет
        LoadDeck();

        LoadAllCards();
        LoadLineCards();
        UpdateCounter();
    }

    private void LoadDeck()
    {
        if(PlayerPrefs.HasKey("Cards"))
        {
           string[] data =  PlayerPrefs.GetString("Cards").Split("***");
            for(int i = 0;i< data.Length;i++)
            {
                myDeckCards.Add(DeckCardManager.DeckCards.Find(x => x.CardName.Equals(data[i])));
            }
        }
        else
        {
            myDeckCards.Add(DeckCardManager.DeckCards[20]);
            myDeckCards.Add(DeckCardManager.DeckCards[21]);
            myDeckCards.Add(DeckCardManager.DeckCards[22]);
            myDeckCards.Add(DeckCardManager.DeckCards[23]);
            myDeckCards.Add(DeckCardManager.DeckCards[18]);
            myDeckCards.Add(DeckCardManager.DeckCards[19]);
            myDeckCards.Add(DeckCardManager.DeckCards[7]);
            myDeckCards.Add(DeckCardManager.DeckCards[0]);
            myDeckCards.Add(DeckCardManager.DeckCards[4]);
            myDeckCards.Add(DeckCardManager.DeckCards[6]);
        }
    }

    public void Save()
    {
        string[] arr = new string[myDeckCards.Count];
        for(int i=0;i<arr.Length;i++)
        {
            arr[i] = myDeckCards[i].CardName;
        }
        PlayerPrefs.SetString("Cards", string.Join("***", arr));
        PlayerPrefs.Save();
    }

    public void Reset()
    {
        PlayerPrefs.DeleteKey("Cards");
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

    public void UpdateCounter()
    {
        Counter.text = myDeckCards.Count.ToString() + "/10";

    }


}
