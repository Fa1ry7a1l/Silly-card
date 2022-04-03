using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManagerSrc : MonoBehaviour
{
    public Game CurrentGame;
    public Transform EnemyHand, PlayerHand;
    //public Transform EnemyField, PlayerField;
    public GameObject CardPref;


    public GameObject ResultGO;
    public TextMeshProUGUI ResultText;

    [SerializeField] private PlayerBase Player, Enemy;
    [SerializeField] private Turn Turn;

    // Start is called before the first frame update
    void Start()
    {


        GiveHandCards(CurrentGame.EnemyDeck, EnemyHand);
        GiveHandCards(CurrentGame.PlayerDeck, PlayerHand);

        Turn.PlayerTurnStarted += GiveCardToPlayer;
        Turn.EnemyTurnStarted += GiveCardToEnemy;
    }

    /// <summary>
    /// ������ ��������� ����� ����
    /// </summary>
    /// <param name="deck"></param>
    /// <param name="hand"></param>
    void GiveHandCards(List<CardModelBase> deck, Transform handTransform)
    {
        for (int i = 0; i < Game.StartHandSize; i++)
        {
            GiveCardToHand(deck, handTransform);
        }
    }

    void GiveCardToPlayer()
    {
        GiveCardToHand(CurrentGame.PlayerDeck, PlayerHand);
    }

    void GiveCardToEnemy()
    {
        GiveCardToHand(CurrentGame.EnemyDeck, EnemyHand);
    }
    /// <summary>
    /// ��������� ����� � ����
    /// </summary>
    /// <param name="deck"></param>
    /// <param name="hand"></param>
    void GiveCardToHand(List<CardModelBase> deck, Transform handTransform)
    {

        if (deck.Count == 0 || handTransform.childCount == Game.MaxFieldSize)
            return;
        CardModelBase card = deck[0];
        deck.Remove(card);

        GameObject CardGo = Instantiate(CardPref, handTransform, false);
        Card cb = CardGo.GetComponent<Card>();
        cb.Init(card, handTransform == PlayerHand ? Card.CardOwner.Player : Card.CardOwner.Enemy);
        CardGo.layer = 2;
        if (handTransform == EnemyHand)
        {
            cb.PlayerBase = Enemy;
            CardGo.GetComponent<CardShowSrc>().HideCardInfo();
            CurrentGame.EnemyHand.Add(cb);
        }
        else
        {
            cb.PlayerBase = Player;

            CurrentGame.PlayerHand.Add(cb);
            CardGo.GetComponent<AttackedCard>().enabled = false;

        }

    }




    // todo ��������
    //void EnemyTurn()
    //{
    //    //��������� ��� �������� ���� �� ����� // todo
    //    if (CurrentGame.EnemyHand.Count > 0)
    //    {
    //        //����������, ����, ������� ����� ��������� �� ����
    //        int count = Random.Range(0, CurrentGame.EnemyHand.Count + 1);
    //        for (int i = 0, counter = 0; counter < count && CurrentGame.EnemyField.Count < Game.MaxFieldSize; counter++)
    //        {
    //            CurrentGame.EnemyHand[i].ShowCardInfo(CurrentGame.EnemyHand[i].SelfCard);
    //            CurrentGame.EnemyHand[i].transform.SetParent(GetNextPosition(Random.Range(0, Game.MaxFieldSize - CurrentGame.EnemyField.Count)));

    //            CurrentGame.EnemyField.Add(CurrentGame.EnemyHand[i]);
    //            CurrentGame.EnemyHand.Remove(CurrentGame.EnemyHand[i]);
    //        }
    //    }
    //}

    //Transform GetNextPosition(int pos)
    //{
    //    if (pos < 0 || pos > Game.MaxFieldSize - CurrentGame.EnemyField.Count)
    //    {
    //        throw new ArgumentException($"cant get position for {pos} in range [0,{Game.MaxFieldSize - CurrentGame.EnemyField.Count})");
    //    }
    //    pos++;
    //    int counter = 0;
    //    for (int i = 0; i < EnemyField.childCount; i++)
    //    {
    //        if (counter == pos && EnemyField.GetChild(i).childCount == 0)
    //            return EnemyField.GetChild(i);

    //        if (EnemyField.GetChild(i).childCount == 0)
    //            counter++;
    //        if (counter == pos && EnemyField.GetChild(i).childCount == 0)
    //            return EnemyField.GetChild(i);

    //    }
    //    throw new ArgumentException($"cant get position for {pos} in range [0,{Game.MaxFieldSize - CurrentGame.EnemyField.Count}) in for loop");

    //}



    /// <summary>
    /// ������ ������� ������ �� �����
    /// </summary>
    public void GiveNewCards()
    {
        GiveCardToHand(CurrentGame.EnemyDeck, EnemyHand);
        GiveCardToHand(CurrentGame.PlayerDeck, PlayerHand);
    }


    public void CardsFidht(Card card1, Card card2)
    {
        if (card1.CardModel is UnitCard uc1 && card2.CardModel is UnitCard uc2)
        {
            uc1.GetDamage(uc2.Attack);
            uc2.GetDamage(uc1.Attack);

            card1.CardShow.DeHighlightCard();
            card1.CardShow.RefreshData(uc1);
            card2.CardShow.RefreshData(uc2);
            if (!uc1.IsAlive)
            {
                DestroyCard(card1);
            }
            if (!uc2.IsAlive)
            {
                DestroyCard(card2);
            }
        }


    }


    public void DestroyIfDead(Card card)
    {
        if (card.CardModel is UnitCard uc && !uc.IsAlive)
        {
            DestroyCard(card);
        }
    }

    /// <summary>
    /// ���������� �����
    /// </summary>
    /// <param name="card"></param>
    public void DestroyCard(Card card)
    {
        //card.GetComponent<CardMovementSrc>().OnEndDrag(null);

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

    /*
    void CheckForResult()
    {
        if (PlayerHPBar.CurrentHP == 0 || EnemyHPBar.CurrentHP == 0)
        {
            ResultGO.SetActive(true);
            StopAllCoroutines();

            if (PlayerHPBar.CurrentHP == 0)
                ResultText.text = "������� � ������ ���";
            else
                ResultText.text = "������!";

        }
    }*/

}
