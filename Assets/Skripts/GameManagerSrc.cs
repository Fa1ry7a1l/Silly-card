using DG.Tweening;
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

    public EnemyStrat EnemyLogic;

    private int PlayerStepsWithoutCards = 0;
    private int EnemyStepsWithoutCards = 0;

    [SerializeField] private PlayerBase Player, Enemy;
    [SerializeField] private Turn Turn;
    private Action<int, Transform, bool> _added;


    // Start is called before the first frame update
    void Start()
    {
        GiveHandCards(CurrentGame.EnemyDeck, EnemyHand);
        GiveHandCards(CurrentGame.PlayerDeck, PlayerHand);

        Turn.PlayerTurnStarted += GiveCardToPlayer;
        Turn.EnemyTurnStarted += GiveCardToEnemy;
    }

    public void Init(Action<int, Transform, bool> added)
    {
        _added = added;
    }

    /// <summary>
    /// Выдает стартовый набор кард
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
        if (CurrentGame.PlayerDeck.Count > 0)
        {
            GiveCardToHand(CurrentGame.PlayerDeck, PlayerHand);
        }
        else
        {
            PlayerStepsWithoutCards++;
            Player.Damage(PlayerStepsWithoutCards);
        }
    }

    void GiveCardToEnemy()
    {
        if (CurrentGame.EnemyDeck.Count > 0)
        {
            GiveCardToHand(CurrentGame.EnemyDeck, EnemyHand);
        }
        else
        {
            EnemyStepsWithoutCards++;
            Enemy.Damage(EnemyStepsWithoutCards);
        }

        EnemyLogic.TryStep();
    }
    /// <summary>
    /// Добавляет карту в руку
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

        var clone = Instantiate(CardPref, cb.transform);
   
        clone.GetComponent<CanvasGroup>().interactable = false;
        clone.GetComponent<CanvasGroup>().blocksRaycasts = false;
        var cloneCard = clone.GetComponent<Card>();
        cloneCard.RayCaster.raycastTarget = false;
        cloneCard.gameObject.SetActive(false);
        cloneCard.Init(card, handTransform == PlayerHand ? Card.CardOwner.Player : Card.CardOwner.Enemy);
        cb.Clone = cloneCard;
        cb.Init(card, handTransform == PlayerHand ? Card.CardOwner.Player : Card.CardOwner.Enemy);
        CardGo.layer = 2;
        if (handTransform == EnemyHand)
        {
            cb.PlayerBase = Enemy;
            CardGo.GetComponent<CardShowSrc>().HideCardInfo();
            CurrentGame.EnemyHand.Add(cb);
            _added?.Invoke(CurrentGame.EnemyHand.Count, EnemyHand, true);

        }
        else
        {
            cb.PlayerBase = Player;

            CurrentGame.PlayerHand.Add(cb);
            CardGo.GetComponent<AttackedCard>().enabled = false;
            _added?.Invoke(CurrentGame.PlayerHand.Count, PlayerHand, false);

        }


    }




    // todo реализуй
    //void EnemyTurn()
    //{
    //    //поправить для отправки карт на битву // todo
    //    if (CurrentGame.EnemyHand.Count > 0)
    //    {
    //        //количество, карт, которое нужно отправить на поле
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
    /// Выдает каждому игроку по карте
    /// </summary>
    public void GiveNewCards()
    {
        GiveCardToHand(CurrentGame.EnemyDeck, EnemyHand);
        GiveCardToHand(CurrentGame.PlayerDeck, PlayerHand);
    }


    public void CardsFight(Card card1, Card card2)
    {
        if (card1.CardModel is UnitCard uc1 && card2.CardModel is UnitCard uc2)
        {
            //особенный MoveToTarget
            var oldLocalPosition = card1.transform.localPosition;
            var oldPosition = card1.transform.position;
            var oldParent = card1.transform.parent;
            var oldSibling = card1.transform.GetSiblingIndex();
            card1.transform.SetParent(oldParent.parent.parent);
            card1.transform.SetAsLastSibling();
            var duration = 1f;
            if (card1.Owner is Card.CardOwner.Player)
                duration = 0f;
            card1.transform.DOMove(card2.transform.position, duration).OnComplete(() =>
            {
                uc1.GetDamage(uc2.Attack);
                uc2.GetDamage(uc1.Attack);

                card1.CardShow.DeHighlightCard();

                card1.UpdateVisualData();
                card2.UpdateVisualData();

                if (!uc1.IsAlive)
                {
                    DestroyCard(card1);
                    EnemyStrat.EnemyFieldChildCounts[EnemyStrat.FightCardFieldInd] = false;
                }
                else //card1.transform.GetComponent<CardMovementSrc>().MoveFromTarget(oldPosition, oldParent, oldSibling);
                {
                    if (card1.Owner is Card.CardOwner.Enemy)
                        card1.transform.DOMove(oldPosition, 1f).OnComplete(() =>
                    {
                        card1.transform.SetParent(oldParent);
                        card1.transform.SetSiblingIndex(oldSibling);
                        //card1.transform.DOMove(oldLocalPosition, 0.5f);
                    });
                }
                /*    card1.transform.DOMove(oldPosition, 1f).OnComplete(()=>
                {
                    card1.transform.SetParent(oldParent);
                    card1.transform.SetSiblingIndex(oldSibling);
                });*/
                if (!uc2.IsAlive)
                {
                    DestroyCard(card2);
                }

            });

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
    /// Уничтожает карту
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
                ResultText.text = "Повезет в другой раз";
            else
                ResultText.text = "Победа!";

        }
    }*/

}
