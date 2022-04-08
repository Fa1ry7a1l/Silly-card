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

        if (hp.CurrentHP == 0)
            OnDeath?.Invoke();
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

        if (cardBase.CardModel is UnitCard uc)
        {
            if (uc.CanAttack)
            {
                uc.CanAttack = false;
                cardBase.CardShow.DeHighlightCard();
                this.Damage(uc.Attack);
            }
        }else if (cardBase.CardModel is SingleTargetSpellCard stsc)
        {
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
        if(cardBase!= null)
        if (cardBase.CardModel is UnitCard uc)
        {
            if (uc.CanAttack)
            {
                uc.CanAttack = false;
                cardBase.CardShow.DeHighlightCard();
                this.Damage(uc.Attack);
            }
        }
        else if (cardBase.CardModel is SingleTargetSpellCard stsc)
        {
            stsc.Spell(this);
            GameManager.DestroyCard(cardBase);
        }
    }
}
