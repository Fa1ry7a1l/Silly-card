using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour
{
    /// <summary>
    /// Размер изначальной колоды
    /// </summary>
    public const int DeckSize = 20;

    public const int StartHandSize = 3;

    public const int MaxFieldSize = 6;

    [SerializeField] public Player Player;
    [SerializeField] public Enemy Enemy;


    /// <summary>
    /// Колода противника
    /// </summary>
    public List<CardModelBase> EnemyDeck;

    /// <summary>
    /// Колода игрока
    /// </summary>
    public List<CardModelBase> PlayerDeck;

    /// <summary>
    /// Рука противника
    /// </summary>
    public List<Card> EnemyHand;

    /// <summary>
    /// Рука игрока
    /// </summary>
    public List<Card> PlayerHand;

    /// <summary>
    /// Поле противника
    /// </summary>
    public List<Card> EnemyField;

    /// <summary>
    /// Поле игрока
    /// </summary>
    public List<Card> PlayerField;

    /// <summary>
    /// Сброс противника
    /// </summary>
    public List<Card> EnemyFold;

    /// <summary>
    /// Сброс игрока
    /// </summary>
    public List<Card> PlayerFold;

    private void Awake()
    {
        CardManagerSrc.GenerateCards();

        EnemyDeck = GiveDeckCard();
        PlayerDeck = TryLoadDeck();

        EnemyField = new List<Card>();
        PlayerField = new List<Card>();

        EnemyHand = new List<Card>();
        PlayerHand = new List<Card>();

        EnemyFold = new List<Card>();
        PlayerFold = new List<Card>();
    }


    /// <summary>
    /// выдает  стартовую колоду карт
    /// </summary>
    /// <returns></returns>
    List<CardModelBase> GiveDeckCard()
    {
        List<CardModelBase> cards = new List<CardModelBase>();
        for (int i = 0; i < CardManagerSrc.AllMassiveTargetSpellCards.Count; i++)
            cards.Add(CardManagerSrc.AllMassiveTargetSpellCards[i]);
        for (int i = 0; i < CardManagerSrc.AllSingleTargetCards.Count; i++)
            cards.Add(CardManagerSrc.AllSingleTargetCards[i]);

        for (int i = cards.Count; i < DeckSize; i++)
        {
            var card = CardManagerSrc.AllCards[Random.Range(0, CardManagerSrc.AllCards.Count)].Clone();
            cards.Add(card);
        }

        return ShuffleCards(cards);
    }

    List<CardModelBase> TryLoadDeck()
    {
        List<CardModelBase> cards = new List<CardModelBase>();

        if (PlayerPrefs.HasKey("Cards"))
        {
            string[] data = PlayerPrefs.GetString("Cards").Split("***");
            for (int i = 0; i < data.Length; i++)
            {
                var c =CardManagerSrc.AllCards.Find(x => x.Name.Equals(data[i]));
                cards.Add(c);
                cards.Add(c);
            }
        }
        else
        {
            cards.Add(CardManagerSrc.AllCards[20]);
            cards.Add(CardManagerSrc.AllCards[20]);
            cards.Add(CardManagerSrc.AllCards[21]);
            cards.Add(CardManagerSrc.AllCards[21]);
            cards.Add(CardManagerSrc.AllCards[22]);
            cards.Add(CardManagerSrc.AllCards[22]);
            cards.Add(CardManagerSrc.AllCards[23]);
            cards.Add(CardManagerSrc.AllCards[23]);
            cards.Add(CardManagerSrc.AllCards[18]);
            cards.Add(CardManagerSrc.AllCards[18]);
            cards.Add(CardManagerSrc.AllCards[19]);
            cards.Add(CardManagerSrc.AllCards[19]);
            cards.Add(CardManagerSrc.AllCards[7]);
            cards.Add(CardManagerSrc.AllCards[7]);
            cards.Add(CardManagerSrc.AllCards[0]);
            cards.Add(CardManagerSrc.AllCards[0]);
            cards.Add(CardManagerSrc.AllCards[4]);
            cards.Add(CardManagerSrc.AllCards[4]);
            cards.Add(CardManagerSrc.AllCards[6]);
            cards.Add(CardManagerSrc.AllCards[6]);
        }

        return ShuffleCards(cards);
    }

    List<CardModelBase> ShuffleCards(List<CardModelBase> cards)
    {
        for (int i = 0; i < cards.Count - 1; i++)
        {
            int pos = Random.Range(i + 1, cards.Count);
            CardModelBase b = cards[pos];
            cards[pos] = cards[i];
            cards[i] = b;
        }
        return cards;
    }


}