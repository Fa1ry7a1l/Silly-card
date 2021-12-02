using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum FieldType
{
    SELF_FIELD,
    SELF_HAND,
    ENEMY_FIELD,
    ENEMY_HAND
}

public class DropPlaceScrypt : MonoBehaviour, IDropHandler, IPointerEnterHandler
{
    public FieldType Type;
    public void OnDrop(PointerEventData eventData)
    {
        if (Type != FieldType.SELF_FIELD)
            return;

        if (transform.childCount != 0)
            return; //потом заменится скидыванием предыдущей в отбой


        CardMovementSrc card = eventData.pointerDrag.GetComponent<CardMovementSrc>();
        
        if(card)
        {
            card.GameManager.CurrentGame.PlayerHand.Remove(card.GetComponent<CardShowSrc>());
            card.GameManager.CurrentGame.PlayerField.AddFirst(card.GetComponent<CardShowSrc>());
            card.DefaultParent = transform;
        }
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag==null || Type != FieldType.SELF_FIELD)
            return;
    }
}
