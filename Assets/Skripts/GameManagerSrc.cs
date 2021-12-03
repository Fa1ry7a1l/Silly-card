using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Random = UnityEngine.Random;

public class Game
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

    public Game()
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

public class GameManagerSrc : MonoBehaviour
{
    public Game CurrentGame;
    public Transform EnemyHand, PlayerHand;
    public Transform EnemyField, PlayerField;
    public GameObject CardPref;
    int Turn, TurnTime = 30;
    public TextMeshProUGUI TurnTimeText;
    public Button EndTurnButton;

    // Start is called before the first frame update
    void Start()
    {
        Turn = 0;
        StartCoroutine(TurnFunc());

        CurrentGame = new Game();

        GiveHandCards(CurrentGame.EnemyDeck, EnemyHand);
        GiveHandCards(CurrentGame.PlayerDeck, PlayerHand);
    }

    /// <summary>
    /// ������ ��������� ����� ����
    /// </summary>
    /// <param name="deck"></param>
    /// <param name="hand"></param>
    void GiveHandCards(List<Card> deck, Transform handTransform)
    {
        for (int i = 0; i < Game.StartHandSize; i++)
        {
            GiveCardToHand(deck, handTransform);
        }
    }

    /// <summary>
    /// ��������� ����� � ����
    /// </summary>
    /// <param name="deck"></param>
    /// <param name="hand"></param>
    void GiveCardToHand(List<Card> deck, Transform handTransform)
    {
        if (deck.Count == 0)
            return;
        Card card = deck[0];
        deck.Remove(card);

        GameObject CardGo = Instantiate(CardPref, handTransform, false);
        if (handTransform == EnemyHand)
        {
            CardGo.GetComponent<CardShowSrc>().HideCardInfo(card);
            CurrentGame.EnemyHand.Add(CardGo.GetComponent<CardShowSrc>());
        }
        else
        {
            CardGo.GetComponent<CardShowSrc>().ShowCardInfo(card);
            CurrentGame.PlayerHand.Add(CardGo.GetComponent<CardShowSrc>());


        }

    }

    public bool IsPlayerTurn
    {
        get { return Turn % 2 == 0; }
    }

    IEnumerator TurnFunc()
    {
        TurnTime = 30;
        TurnTimeText.text = TurnTime.ToString();

        if (IsPlayerTurn)
        {
            while (TurnTime-- > 0)
            {
                TurnTimeText.text = TurnTime.ToString();
                yield return new WaitForSeconds(1);
            }
            ChangeTurn();

        }
        else
        {
            while (TurnTime-- > 27)
            {
                TurnTimeText.text = "��� ����������";
                yield return new WaitForSeconds(1);
            }
            EnemyTurn();
            ChangeTurn();
        }
    }

    // todo ��������
    void EnemyTurn()
    {
        if (CurrentGame.EnemyHand.Count > 0)
        {
            int count = Random.Range(0, CurrentGame.EnemyHand.Count + 1);
            for (int i = 0; i < count && CurrentGame.EnemyField.Count < Game.MaxFieldSize; i++)
            {
                Console.WriteLine(i);
                CurrentGame.EnemyHand[i].ShowCardInfo(CurrentGame.EnemyHand[i].SelfCard);
                CurrentGame.EnemyHand[i].transform.SetParent(GetNextPosition(Random.Range(0, Game.MaxFieldSize - CurrentGame.EnemyField.Count)));

                CurrentGame.EnemyField.Add(CurrentGame.EnemyHand[i]);
                CurrentGame.EnemyHand.Remove(CurrentGame.EnemyHand[i]);
            }
        }
    }

    Transform GetNextPosition(int pos)
    {
        if (pos < 0 || pos > Game.MaxFieldSize - CurrentGame.EnemyField.Count)
        {
            throw new ArgumentException($"cant get position for {pos} in range [0,{Game.MaxFieldSize - CurrentGame.EnemyField.Count})");
        }
        pos++;
        int counter = 0;
        for (int i = 0; i < EnemyField.childCount; i++)
        {
            if (counter == pos && EnemyField.GetChild(i).childCount == 0)
                return EnemyField.GetChild(i);

            if (EnemyField.GetChild(i).childCount == 0)
                counter++;
            if (counter == pos && EnemyField.GetChild(i).childCount == 0)
                return EnemyField.GetChild(i);

        }
        throw new ArgumentException($"cant get position for {pos} in range [0,{Game.MaxFieldSize - CurrentGame.EnemyField.Count}) in for loop");

    }


    /// <summary>
    /// ��������� ����
    /// </summary>
    public void ChangeTurn()
    {
        StopAllCoroutines();

        Turn++;
        EndTurnButton.enabled = IsPlayerTurn;
        if (IsPlayerTurn)
        {
            GiveNewCards();
        }

        StartCoroutine(TurnFunc());
    }

    /// <summary>
    /// ������ ������� ������ �� �����
    /// </summary>
    public void GiveNewCards()
    {
        GiveCardToHand(CurrentGame.EnemyDeck, EnemyHand);
        GiveCardToHand(CurrentGame.PlayerDeck, PlayerHand);
    }




}
