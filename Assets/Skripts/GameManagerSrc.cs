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
    /// Размер изначальной колоды
    /// </summary>
    public const int DeckSize = 10;

    public const int StartHandSize = 4;

    public const int MaxFieldSize = 6;


    /// <summary>
    /// Колода противника
    /// </summary>
    public List<Card> EnemyDeck;

    /// <summary>
    /// Колода игрока
    /// </summary>
    public List<Card> PlayerDeck;

    /// <summary>
    /// Рука противника
    /// </summary>
    public List<CardShowSrc> EnemyHand;

    /// <summary>
    /// Рука игрока
    /// </summary>
    public List<CardShowSrc> PlayerHand;

    /// <summary>
    /// Поле противника
    /// </summary>
    public List<CardShowSrc> EnemyField;

    /// <summary>
    /// Поле игрока
    /// </summary>
    public List<CardShowSrc> PlayerField;

    /// <summary>
    /// Сброс противника
    /// </summary>
    public List<CardShowSrc> EnemyFold;

    /// <summary>
    /// Сброс игрока
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
    /// выдает  стартовую колоду карт
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
        CurrentGame = new Game();

        StartCoroutine(TurnFunc());


        GiveHandCards(CurrentGame.EnemyDeck, EnemyHand);
        GiveHandCards(CurrentGame.PlayerDeck, PlayerHand);
    }

    /// <summary>
    /// Выдает стартовый набор кард
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
    /// Добавляет карту в руку
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
            CardGo.GetComponent<AttackedCard>().enabled = false;

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

        foreach (var card in CurrentGame.PlayerField)
        {
            card.SelfCard.ChangeAttackState(false);
            card.DeHighlightCard();
        }
        foreach (var card in CurrentGame.EnemyField)
        {
            card.SelfCard.ChangeAttackState(false);
            card.SelfCard.ChangeAttackState(false);

        }



        if (IsPlayerTurn)
        {
            foreach(var card in CurrentGame.PlayerField)
            {
                card.SelfCard.ChangeAttackState(true);
                card.HighlightCard();
            }

            while (TurnTime-- > 0)
            {
                TurnTimeText.text = TurnTime.ToString();
                yield return new WaitForSeconds(1);
            }
            
            ChangeTurn();

        }
        else
        {
            foreach (var card in CurrentGame.EnemyField)
            {
                card.SelfCard.ChangeAttackState(true);
            }

            while (TurnTime-- > 27)
            {
                TurnTimeText.text = "Ход противника";
                yield return new WaitForSeconds(1);
            }
            EnemyTurn();
            ChangeTurn();
        }
    }

    // todo реализуй
    void EnemyTurn()
    {
        if (CurrentGame.EnemyHand.Count > 0)
        {
            //количество, карт, которое нужно отправить на поле
            int count = Random.Range(0, CurrentGame.EnemyHand.Count + 1);
            for (int i = 0, counter = 0; counter < count && CurrentGame.EnemyField.Count < Game.MaxFieldSize; counter++)
            {
                CurrentGame.EnemyHand[i].ShowCardInfo(CurrentGame.EnemyHand[i].SelfCard);
                CurrentGame.EnemyHand[i].transform.SetParent(GetNextPosition(Random.Range(0, Game.MaxFieldSize - CurrentGame.EnemyField.Count)));

                CurrentGame.EnemyField.Add(CurrentGame.EnemyHand[i]);
                CurrentGame.EnemyHand.Remove(CurrentGame.EnemyHand[i]);
            }
        }
    }

    //почему то она иногда кидает out of range
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


    public void CardsFidht(CardShowSrc card1, CardShowSrc card2)
    {
        card1.SelfCard.GetDamage(card2.SelfCard.Attack);
        card2.SelfCard.GetDamage(card1.SelfCard.Attack);

        card1.DeHighlightCard();
        card1.RafreshData();
        card2.RafreshData();
        if(!card1.SelfCard.IsAlive)
        {
            DestroyCard(card1);
        }
        if (!card2.SelfCard.IsAlive)
        { 
            DestroyCard(card2);
        }
    }

    /// <summary>
    /// Уничтожает карту
    /// </summary>
    /// <param name="card"></param>
    public void DestroyCard(CardShowSrc card)
    {
        card.GetComponent<CardMovementSrc>().OnEndDrag(null);

        if (CurrentGame.PlayerField.Contains(card))
        {
            CurrentGame.PlayerField.Remove(card);
            CurrentGame.PlayerFold.Add(card);
        }
        if (CurrentGame.PlayerHand.Contains(card))
        {
            CurrentGame.PlayerHand.Remove(card);
            CurrentGame.PlayerFold.Add(card);

        }
        if (CurrentGame.EnemyField.Contains(card))
        {
            CurrentGame.EnemyField.Remove(card);
            CurrentGame.EnemyFold.Add(card);

        }
        if (CurrentGame.EnemyHand.Contains(card))
        {
            CurrentGame.EnemyHand.Remove(card);
            CurrentGame.EnemyFold.Add(card);

        }
        Destroy(card.gameObject);
    }

}
