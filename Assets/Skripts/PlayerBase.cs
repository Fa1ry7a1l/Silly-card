using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerBase : MonoBehaviour, IDropHandler
{

    [SerializeField] protected Game game;
    [SerializeField] protected Turn turn;
    [SerializeField] protected ManaBar mana;
    [SerializeField] protected HPBar hp;

    public event Action<int,int> OnDamage; //сколько нанесли и новое значение hp
    public event Action<int,int> OnHeal; //сколько восстановили и новое значение hp


    public bool TryPlay(Card card)
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
    }

    public void Heal(int heal)
    {
        hp.AddHP(heal);
        OnHeal?.Invoke(heal, hp.CurrentHP);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!turn.IsPlayerTurn)
        {
            return;
        }

        CardShowSrc cardShowSrc = eventData.pointerDrag.GetComponent<CardShowSrc>();


        if (cardShowSrc != null && cardShowSrc.SelfCard.CanAttack)
        {
            cardShowSrc.SelfCard.CanAttack = false;
            cardShowSrc.DeHighlightCard();
            this.Damage(cardShowSrc.SelfCard.Attack);
        }
    }
}
