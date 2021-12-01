using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game
{
    /// <summary>
    /// Размер изначальной колоды
    /// </summary>
    public const int DeckSize = 10;

    public const int StartHandSize = 4;


    /// <summary>
    /// Колода противника
    /// </summary>
    public LinkedList<Card> EnemyDeck;

    /// <summary>
    /// Колода игрока
    /// </summary>
    public LinkedList<Card> PlayerDeck;

    /// <summary>
    /// Рука противника
    /// </summary>
    public LinkedList<Card> EnemyHand;

    /// <summary>
    /// Рука игрока
    /// </summary>
    public LinkedList<Card> PlayerHand;

    /// <summary>
    /// Поле противника
    /// </summary>
    public LinkedList<Card> EnemyField;

    /// <summary>
    /// Поле игрока
    /// </summary>
    public LinkedList<Card> PlayerField;

    /// <summary>
    /// Сброс противника
    /// </summary>
    public LinkedList<Card> EnemyFold;

    /// <summary>
    /// Сброс игрока
    /// </summary>
    public LinkedList<Card> PlayerFold;

    public Game()
    {
        EnemyDeck = GiveDeckCard();
        PlayerDeck = GiveDeckCard();

        EnemyField = new LinkedList<Card>();
        PlayerField = new LinkedList<Card>();

        EnemyHand = new LinkedList<Card>();
        PlayerHand = new LinkedList<Card>();

        EnemyFold = new LinkedList<Card>();
        PlayerFold = new LinkedList<Card>();
    }


    /// <summary>
    /// выдает  стартовую колоду карт
    /// </summary>
    /// <returns></returns>
    LinkedList<Card> GiveDeckCard()
    {
        LinkedList<Card> cards = new LinkedList<Card>();

        for (int i = 0; i < DeckSize; i++)
        {
            cards.AddFirst(CardManagerSrc.AllCards[Random.Range(0, CardManagerSrc.AllCards.Count)]);
        }

        return cards;
    }
}

public class GameManagerSrc : MonoBehaviour
{
    public Game CurrentGame;
    public Transform EnemyHand, PlayerHand;
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
    /// Выдает стартовый набор кард
    /// </summary>
    /// <param name="deck"></param>
    /// <param name="hand"></param>
    void GiveHandCards(LinkedList<Card> deck, Transform hand)
    {
        for (int i = 0; i < Game.StartHandSize; i++)
        {
            GiveCardToHand(deck, hand);
        }
    }

    /// <summary>
    /// Добавляет карту в руку
    /// </summary>
    /// <param name="deck"></param>
    /// <param name="hand"></param>
    void GiveCardToHand(LinkedList<Card> deck, Transform hand)
    {
        if (deck.Count == 0)
            return;
        Card card = deck.First.Value;
        deck.RemoveFirst();

        GameObject CardGo = Instantiate(CardPref, hand, false);
        if (hand == EnemyHand)
        {
            CardGo.GetComponent<CardShowSrc>().HideCardInfo(card);
        }
        else
        {
            CardGo.GetComponent<CardShowSrc>().ShowCardInfo(card);

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

        if(IsPlayerTurn)
        {
            while(TurnTime-- >0)
            {
                TurnTimeText.text = TurnTime.ToString();
                yield return new WaitForSeconds(1);
            }
            ChangeTurn();

        }
        else
        {
            while(TurnTime-- > 27)
            {
                TurnTimeText.text = "Ход противника";
                yield return new WaitForSeconds(1);
            }
            ChangeTurn();
        }
    }

    /// <summary>
    /// Изменение хода
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
    /// Выдает каждому игроку по карте
    /// </summary>
    public void GiveNewCards()
    {
        GiveCardToHand(CurrentGame.EnemyDeck, EnemyHand);
        GiveCardToHand(CurrentGame.PlayerDeck, PlayerHand);
    }


}
