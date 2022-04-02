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

public class DropPlaceScrypt : DropPlaceBase
{
    public FieldType Type;

    public override void MyOnDrop(CardBase cardBase)
    {
        CardMovementSrc card = cardBase.transform.GetComponent<CardMovementSrc>();


        if (Type != FieldType.SELF_FIELD || card.DropPlace.Type == Type)
            return;

        if (transform.childCount != 0)
        {
            var card1 = transform.GetChild(0).GetComponent<CardBase>();
            FindObjectOfType<GameManagerSrc>().DestroyCard(card1);
        }



        if (card)
        {
            if (cardBase.TryPlay())
            {
                card.GameManager.CurrentGame.PlayerHand.Remove(cardBase);
                card.GameManager.CurrentGame.PlayerField.Add(cardBase);
                card.DropPlace = this;
                cardBase.gameObject.layer = 8;
                cardBase.transform.SetParent(transform, false);
            }

        }
    }

    /*public void OnDrop(PointerEventData eventData)
    {
        //eventData.pointerDrag.

        if (Type != FieldType.SELF_FIELD || eventData.pointerDrag.GetComponent<CardMovementSrc>().DropPlace.Type == Type)
            return;

        if (transform.childCount != 0)
        {
           var card1 = transform.GetChild(0).GetComponent<CardBase>();
            FindObjectOfType<GameManagerSrc>().DestroyCard(card1);
        }


        CardMovementSrc card = eventData.pointerDrag.GetComponent<CardMovementSrc>();

        if (card)
        {
            CardBase curCard = eventData.pointerDrag.GetComponent<CardBase>();
            if (curCard.TryPlay())
            {
                card.GameManager.CurrentGame.PlayerHand.Remove(curCard);
                card.GameManager.CurrentGame.PlayerField.Add(curCard);
                card.DropPlace = this;
            }

        }
    }*/

    //void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    //{
    //    if (eventData.pointerDrag == null || Type == FieldType.ENEMY_FIELD || Type == FieldType.ENEMY_HAND || Type == FieldType.SELF_HAND)
    //        return;

    //}
}
