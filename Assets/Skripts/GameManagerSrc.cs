using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Random = UnityEngine.Random;

public class GameManagerSrc : MonoBehaviour
{
    public Game CurrentGame;
    public Transform EnemyHand, PlayerHand;
    public Transform EnemyField, PlayerField;
    public GameObject CardPref;
    int Turn, TurnTime = 30;
    public TextMeshProUGUI TurnTimeText;
    public Button EndTurnButton;

    public Transform EnemyManaBarTransform;
    public Transform PlayerManaBarTransform;


    private ManaBar enemyManaBar;
    private ManaBar playerManaBar;


    public Transform EnemyHPBarTransform;
    public Transform PlayerHPBarTransform;


    private HPBar EnemyHPBar;
    private HPBar PlayerHPBar;

    public GameObject ResultGO;
    public TextMeshProUGUI ResultText;

    // Start is called before the first frame update
    void Start()
    {
        Turn = 0;
        CurrentGame = new Game();

        StartCoroutine(TurnFunc());
        PlayerHPBar = new HPBar(PlayerHPBarTransform);
        EnemyHPBar = new HPBar(EnemyHPBarTransform);
        playerManaBar = new ManaBar(PlayerManaBarTransform);
        enemyManaBar = new ManaBar(EnemyManaBarTransform);
        GiveHandCards(CurrentGame.EnemyDeck, EnemyHand);
        GiveHandCards(CurrentGame.PlayerDeck, PlayerHand);

        PlayerHPBar.show();
        EnemyHPBar.show();
        playerManaBar.FillManaBar();
        enemyManaBar.show();
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
            foreach (var card in CurrentGame.PlayerField)
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
        //поправить для отправки карт на битву // todo
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
        FillManaBar(!IsPlayerTurn);
        StartCoroutine(TurnFunc());
    }

    public void FillManaBar(bool isEnemyTurn)
    {
        if (isEnemyTurn)
            enemyManaBar.FillManaBar();
        else
            playerManaBar.FillManaBar();
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
        if (!card1.SelfCard.IsAlive)
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

    public void DamageHero(CardShowSrc card, bool isEnemyAttacked)
    {
        int res;
        if (isEnemyAttacked)
            res = EnemyHPBar.ReduceHP(card.SelfCard.Attack);
        else
            res = PlayerHPBar.ReduceHP(card.SelfCard.Attack);
        card.DeHighlightCard();

        if (res == 0)
            CheckForResult();
    }

    void CheckForResult()
    {
        if (PlayerHPBar.CurrentHP == 0 || EnemyHPBar.CurrentHP == 0)
        {
            ResultGO.SetActive(true);
            StopAllCoroutines();

            if (PlayerHPBar.CurrentHP == 0)
                ResultText.text = "Повезет в другой раз";
            else
                ResultText.text = "Победа!";

        }
    }

}
