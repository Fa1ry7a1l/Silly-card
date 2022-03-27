using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Random = UnityEngine.Random;

public class Game:MonoBehaviour
{
    /// <summary>
    /// ������ ����������� ������
    /// </summary>
    public const int DeckSize = 10;

    public const int StartHandSize = 4;

    public const int MaxFieldSize = 6;


    /// <summary>
    /// ������ ����������
    /// </summary>
    public List<Card> EnemyDeck;

    /// <summary>
    /// ������ ������
    /// </summary>
    public List<Card> PlayerDeck;

    /// <summary>
    /// ���� ����������
    /// </summary>
    public List<CardShowSrc> EnemyHand;

    /// <summary>
    /// ���� ������
    /// </summary>
    public List<CardShowSrc> PlayerHand;

    /// <summary>
    /// ���� ����������
    /// </summary>
    public List<CardShowSrc> EnemyField;

    /// <summary>
    /// ���� ������
    /// </summary>
    public List<CardShowSrc> PlayerField;

    /// <summary>
    /// ����� ����������
    /// </summary>
    public List<CardShowSrc> EnemyFold;

    /// <summary>
    /// ����� ������
    /// </summary>
    public List<CardShowSrc> PlayerFold;

    private void Start()
    {
        EnemyDeck = GiveDeckCard();
        PlayerDeck = GiveDeckCard();

        EnemyField = new List<CardShowSrc>();
        PlayerField = new List<CardShowSrc>();

        EnemyHand = new List<CardShowSrc>();
        PlayerHand = new List<CardShowSrc>();

        EnemyFold = new List<CardShowSrc>();
        PlayerFold = new List<CardShowSrc>();
    }


    /// <summary>
    /// ������  ��������� ������ ����
    /// </summary>
    /// <returns></returns>
    List<Card> GiveDeckCard()
    {
        List<Card> cards = new List<Card>();

        for (int i = 0; i < DeckSize; i++)
        {
            cards.Add(CardManagerSrc.AllCards[Random.Range(0, CardManagerSrc.AllCards.Count)]);
        }

        return cards;
    }
}