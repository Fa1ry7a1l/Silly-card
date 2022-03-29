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
    public const int DeckSize = 10;

    public const int StartHandSize = 4;

    public const int MaxFieldSize = 6;

    [SerializeField] private Player player;
    [SerializeField] private Enemy enemy;


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

    private void Awake()
    {
        EnemyDeck = GiveDeckCard(false);
        PlayerDeck = GiveDeckCard(true);

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
    List<Card> GiveDeckCard(bool isPlayer)
    {
        List<Card> cards = new List<Card>();

        for (int i = 0; i < DeckSize; i++)
        {
            Card card = CardManagerSrc.AllCards[Random.Range(0, CardManagerSrc.AllCards.Count)];
            card.PlayerBase = (isPlayer ? (PlayerBase)player : (PlayerBase)enemy);
            cards.Add(card);
        }

        return cards;
    }
}