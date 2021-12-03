using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackedCard : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        CardShowSrc card = eventData.pointerDrag.GetComponent<CardShowSrc>();

        if(card != null && 
            card.SelfCard.CanAttack&&
            transform.parent.GetComponent<DropPlaceScrypt>().Type == FieldType.ENEMY_FIELD)
        {
            card.SelfCard.ChangeAttackState(false);
            FindObjectOfType<GameManagerSrc>().CardsFidht(card, GetComponent<CardShowSrc>());
        }
    }
}
