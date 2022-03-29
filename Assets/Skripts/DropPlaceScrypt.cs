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


        if (Type != FieldType.SELF_FIELD || eventData.pointerDrag.GetComponent<CardMovementSrc>().DefaultParent.GetComponent<DropPlaceScrypt>().Type == Type)
            return;

        if (transform.childCount != 0)
        {
            CardShowSrc card1 = transform.GetChild(0).GetComponent<CardShowSrc>();
            FindObjectOfType<GameManagerSrc>().DestroyCard(card1);
        }


        CardMovementSrc card = eventData.pointerDrag.GetComponent<CardMovementSrc>();

        if (card)
        {
            Card curCard = eventData.pointerDrag.GetComponent<CardShowSrc>().SelfCard;
            if (curCard.TryPlay())
            {


                card.GameManager.CurrentGame.PlayerHand.Remove(card.GetComponent<CardShowSrc>());
                card.GameManager.CurrentGame.PlayerField.Add(card.GetComponent<CardShowSrc>());
                card.DefaultParent = transform;
            }

        }
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null || Type == FieldType.ENEMY_FIELD || Type == FieldType.ENEMY_HAND || Type == FieldType.SELF_HAND)
            return;

    }
}
