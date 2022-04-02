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
    public const int DeckSize = 10;

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
    public List<CardBase> EnemyHand;

    /// <summary>
    /// Рука игрока
    /// </summary>
    public List<CardBase> PlayerHand;

    /// <summary>
    /// Поле противника
    /// </summary>
    public List<CardBase> EnemyField;

    /// <summary>
    /// Поле игрока
    /// </summary>
    public List<CardBase> PlayerField;

    /// <summary>
    /// Сброс противника
    /// </summary>
    public List<CardBase> EnemyFold;

    /// <summary>
    /// Сброс игрока
    /// </summary>
    public List<CardBase> PlayerFold;

    private void Awake()
    {
        CardManagerSrc.GenerateCards();

        EnemyDeck = GiveDeckCard(false);
        PlayerDeck = GiveDeckCard(true);

        EnemyField = new List<CardBase>();
        PlayerField = new List<CardBase>();

        EnemyHand = new List<CardBase>();
        PlayerHand = new List<CardBase>();

        EnemyFold = new List<CardBase>();
        PlayerFold = new List<CardBase>();
    }


    /// <summary>
    /// выдает  стартовую колоду карт
    /// </summary>
    /// <returns></returns>
    List<CardModelBase> GiveDeckCard(bool isPlayer)
    {
        List<CardModelBase> cards = new List<CardModelBase>();

        for (int i = 0; i < DeckSize; i++)
        {
            var card = CardManagerSrc.AllCards[Random.Range(0, CardManagerSrc.AllCards.Count)];
            cards.Add(card);
        }

        return cards;
    }
}