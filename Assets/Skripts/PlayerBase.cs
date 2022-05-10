using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerBase : DropPlaceBase, ITarget
{

    [SerializeField] public Game game;
    [SerializeField] protected Turn turn;
    [SerializeField] protected ManaBar mana;
    [SerializeField] protected HPBar hp;
    [SerializeField] protected GameManagerSrc GameManager;

    public event Action<int,int> OnDamage; //сколько нанесли и новое значение hp
    public event Action<int,int> OnHeal; //сколько восстановили и новое значение hp

    public event Action OnDeath;


    public bool TryPlay(CardModelBase card)
    {
        if(mana.CurrentMana >= card.Manacost)
        {
            mana.ReduceMana(card.Manacost);
            return true;
        }
        return false;   
    }

    public void Damage(int damage)
    {
        hp.ReduceHP(damage);
        OnDamage?.Invoke(damage,hp.CurrentHP);

        if (hp.CurrentHP <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    public void Heal(int heal)
    {
        hp.AddHP(heal);
        OnHeal?.Invoke(heal, hp.CurrentHP);
    }

    public int GetHP()
    {
        return hp.CurrentHP;    
    }



    public override void MyOnDrop(Card cardBase)
    {
        if (!turn.IsPlayerTurn)
        {
            return;
        }
        Debug.Log("Получили карту по лбу");
        if (cardBase.CardModel is UnitCard uc)
        {
            Debug.Log("Карта оказалась существом");
            if (uc.CanAttack)
            {
                uc.CanAttack = false;
                cardBase.CardShow.DeHighlightCard();
                this.Damage(uc.Attack);
            }
        }else if (cardBase.CardModel is SingleTargetSpellCard stsc)
        {
            Debug.Log("Карта оказалась заклинанием");

            stsc.Spell(this);
            GameManager.DestroyCard(cardBase);
        }
    }

    public void TakeDamage(int damage)
    {
        Damage(damage);
    }

    public void TakeHeal(int heal)
    {
        Heal(heal);
    }

    public override void MyOnDropEnemy(Card cardBase)
    {
        Debug.Log("Получили картой по лбу от врага");
        if(cardBase!= null)
        if (cardBase.CardModel is UnitCard uc)
        {
            Debug.Log("Карта врага оказалась существом");

                if (uc.CanAttack)
                {
                    Debug.Log("Карта врага оказалась существом, которое может атаковать");

                    var card = cardBase.transform.GetComponent<CardMovementSrc>();
                    card.MoveToTarget(this.transform, () =>
                    {
                        Debug.Log("Карта врага атаковала");
                        uc.CanAttack = false;
                         cardBase.CardShow.DeHighlightCard();
                         this.Damage(uc.Attack);
                    },true);
                }
        }
        else if (cardBase.CardModel is SingleTargetSpellCard stsc)
        {
                Debug.Log("Карта врага оказалась заклинанием");

                var card = cardBase.transform.GetComponent<CardMovementSrc>();
              card.MoveToTarget(this.transform,
                   () =>
                   { 
                       stsc.Spell(this);
                       GameManager.DestroyCard(cardBase);
                   },false);
                

            }
    }
}
