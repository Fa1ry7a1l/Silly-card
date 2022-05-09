using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyStrat : MonoBehaviour
{
    [SerializeField] private List<GameObject> EnemyFieldList;
    [SerializeField] private List<GameObject> PlayerFieldList;
    [SerializeField] private DropPlaceBase Background;
    [SerializeField] private GameObject EnemyHand;
    [SerializeField] private PlayerBase Player;
    [SerializeField] private PlayerBase Enemy;
    [SerializeField] private GameManagerSrc GameManager;
    private List<Card> GetPlayerCards()
    {
        List<Card> PlayerField = new List<Card>();
        for (int i = 0; i < PlayerFieldList.Count; i++)
            if (PlayerFieldList[i].transform.childCount != 0)
                PlayerField.Add(PlayerFieldList[i].transform.GetChild(0).GetComponent<Card>());
        return PlayerField;
    }



    private List<Card> GetEnemyCards()
    {
        List<Card> EnemyField = new List<Card>();
        for (int i = 0; i < EnemyFieldList.Count; i++)
            if (EnemyFieldList[i].transform.childCount != 0)
                EnemyField.Add(EnemyFieldList[i].transform.GetChild(0).GetComponent<Card>());
        return EnemyField;
    }

    private int GetMaxFieldDamage(List<Card> cards)
    {
        int max = 0;
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].CardModel is UnitCard uc)
                max += uc.Attack;
        }
        return max;
    }

    private List<Card> GetHandCards()
    {
        List<Card> cards = new List<Card>();
        for (int i = 0; i < EnemyHand.transform.childCount; i++)
        {
            cards.Add(EnemyHand.transform.GetChild(i).GetComponent<Card>());
        }
        return cards;
    }

    private int GetMaxDamageFromHand(List<Card> cards)
    {
        int max = 0;
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].CardModel is MassiveTargetSpell mts)
            {
                if (mts.Equals(CardManagerSrc.AllMassiveTargetSpellCards[0]))
                    max += 3;
            }
            if (cards[i].CardModel is SingleTargetSpellCard stsc)
            {
                if (stsc.Equals(CardManagerSrc.AllSingleTargetCards[0]))
                    max += 1;
                if (stsc.Equals(CardManagerSrc.AllSingleTargetCards[1]))
                    max += 2;

            }
        }
        return max;
    }

    private List<Card> GetAttckSpellsFromHand()
    {
        List<Card> cards = new List<Card>();
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].CardModel is MassiveTargetSpell mts)
            {
                if (mts.Equals(CardManagerSrc.AllMassiveTargetSpellCards[0]))
                    cards.Add(cards[i]);
            }
            if (cards[i].CardModel is SingleTargetSpellCard stsc)
            {
                if (stsc.Equals(CardManagerSrc.AllSingleTargetCards[0]))
                    cards.Add(cards[i]);
                if (stsc.Equals(CardManagerSrc.AllSingleTargetCards[1]))
                    cards.Add(cards[i]);

            }
        }

        return cards;
    }

    private bool FindMoustPowerfullPlayerCard(List<Card> cards, out Card card)
    {
        card = null;
        int max = 0;
        for (int i = 0; i < cards.Count; i++)
            if (cards[i].CardModel is UnitCard uc && uc.Attack > max)
            {
                max = uc.Attack;
                card = cards[i];
            }
        return max > 0;
    }

    private AttackedCard FindAttackedCard(Card card)
    {
        AttackedCard attackedCard = null;

        for (int i = 0; i < PlayerFieldList.Count; i++)
        {
            if (PlayerFieldList[i].transform.GetChild(0).GetComponent<Card>().Equals(card))
                attackedCard = PlayerFieldList[i].transform.GetChild(0).GetComponent<AttackedCard>();
        }

        return attackedCard;
    }

    private int FindNextCard(List<Card> cards, int from)
    {
        for (int i = from + 1; i < cards.Count; i++)
            if (cards[i].CardModel is UnitCard)
                return i;
        return -1;
    }

    private GameObject FindCardInGand(Card card)
    {
        for (int i = 0; i < EnemyHand.transform.childCount; i++)
            if (EnemyHand.transform.GetChild(i).GetComponent<Card>().Equals(card))
                return EnemyHand.transform.GetChild(i).gameObject;
        return null;
    }

    public void TryStep()
    {
        List<Card> PlayerFieldCards = GetPlayerCards();
        List<Card> EnemyFieldCards = GetEnemyCards();
        List<Card> EnemyHandCards = GetHandCards();

        int maxEnemyDamage = GetMaxDamageFromHand(EnemyHandCards) + GetMaxFieldDamage(EnemyFieldCards);

        if (maxEnemyDamage >= Player.GetHP())
        {
            List<Card> AttackSpels = GetAttckSpellsFromHand();

            for (int i = 0; i < EnemyFieldCards.Count; i++)
            {
                Player.MyOnDropEnemy(EnemyFieldCards[i]);
                System.Threading.Thread.Sleep(300);
            }
            if (Player.GetHP() > 0)
            {
                for (int i = 0; i < AttackSpels.Count; i++)
                {
                    if (AttackSpels[i].CardModel is SingleTargetSpellCard)
                        Player.OnDropEnemy(AttackSpels[i]);
                    else
                        Background.MyOnDropEnemy(AttackSpels[i]);
                    System.Threading.Thread.Sleep(300);
                }
            }

        }
        else
        {
            //int damageFromPlayer =  GetMaxDamageFromHand(PlayerFieldCards);

            for (int i = 0; i < EnemyFieldCards.Count; i++)
            {
                if (PlayerFieldCards.Count > 0)
                {
                    Card card = null;
                    FindMoustPowerfullPlayerCard(PlayerFieldCards, out card);
                    //тут надо добавить подсветку карты
                    card.gameObject.GetComponent<AttackedCard>().MyOnDropEnemy(EnemyFieldCards[i]);
                    
                }
                else
                {
                    Player.MyOnDropEnemy(EnemyFieldCards[i]);
                }
                System.Threading.Thread.Sleep(300);
            }
        }
        System.Threading.Thread.Sleep(1200);
        List<Card> EnemyFieldCardsNew = GetEnemyCards();
        List<Card> EnemyHandCardsNew = GetHandCards();
        int curPos = 0;
        int next = -1;
        for (int j = 0; (EnemyFieldCardsNew.Count < 6 && EnemyHandCardsNew.Count > 0) && j < EnemyHandCardsNew.Count; j++)
        {
            for (; curPos < 6; curPos++)
                if (EnemyFieldList[curPos].transform.childCount == 0)
                    break;
            if (curPos != 6)
            {
                next = FindNextCard(EnemyHandCardsNew, next);
                if (next != -1)
                {
                    GameObject go = EnemyHandCardsNew[next].gameObject;
                    if (go == null)
                    {
                        print("a");
                    }
                    Card tempCard = go.GetComponent<Card>();
                    DropPlaceScrypt dropPlaceTemp = EnemyFieldList[curPos].GetComponent<DropPlaceScrypt>();

                    print($"go {go != null} tempCard {tempCard != null} dropPlaceTemp {dropPlaceTemp != null}");

                    var res = dropPlaceTemp.OnDropEnemy(tempCard);
                    System.Threading.Thread.Sleep(500);
                    if (res)
                        go.GetComponent<Card>().CardShow.UnHideCardInfo();
                }
            }
        }
        print("Конец хода ****************");
    }
}