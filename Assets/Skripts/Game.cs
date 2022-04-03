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
    /// ������ ����������� ������
    /// </summary>
    public const int DeckSize = 20;

    public const int StartHandSize = 3;

    public const int MaxFieldSize = 6;

    [SerializeField] public Player Player;
    [SerializeField] public Enemy Enemy;


    /// <summary>
    /// ������ ����������
    /// </summary>
    public List<CardModelBase> EnemyDeck;

    /// <summary>
    /// ������ ������
    /// </summary>
    public List<CardModelBase> PlayerDeck;

    /// <summary>
    /// ���� ����������
    /// </summary>
    public List<Card> EnemyHand;

    /// <summary>
    /// ���� ������
    /// </summary>
    public List<Card> PlayerHand;

    /// <summary>
    /// ���� ����������
    /// </summary>
    public List<Card> EnemyField;

    /// <summary>
    /// ���� ������
    /// </summary>
    public List<Card> PlayerField;

    /// <summary>
    /// ����� ����������
    /// </summary>
    public List<Card> EnemyFold;

    /// <summary>
    /// ����� ������
    /// </summary>
    public List<Card> PlayerFold;

    private void Awake()
    {
        CardManagerSrc.GenerateCards();

        EnemyDeck = GiveDeckCard();
        PlayerDeck = GiveDeckCard();

        EnemyField = new List<Card>();
        PlayerField = new List<Card>();

        EnemyHand = new List<Card>();
        PlayerHand = new List<Card>();

        EnemyFold = new List<Card>();
        PlayerFold = new List<Card>();
    }


    /// <summary>
    /// ������  ��������� ������ ����
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