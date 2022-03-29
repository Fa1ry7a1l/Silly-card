using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class AttackHero : MonoBehaviour, IDropHandler
{
    public enum HeroType
    {
        ENEMY, PLAYER
    }

    public HeroType Type;
    public GameManagerSrc GameManager;
    public Turn turn;

    public void OnDrop(PointerEventData eventData)
    {
        if(!turn.IsPlayerTurn)
        {
            return;
        }

        CardShowSrc cardShowSrc = eventData.pointerDrag.GetComponent<CardShowSrc>();


        if(cardShowSrc != null && cardShowSrc.SelfCard.CanAttack)
        {
            cardShowSrc.SelfCard.CanAttack = false;
            GameManager.DamageHero(cardShowSrc, Type == HeroType.ENEMY);
        }
    }
}
