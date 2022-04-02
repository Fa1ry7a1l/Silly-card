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

    public override void MyOnDrop(Card cardBase)
    {
        CardMovementSrc card = cardBase.transform.GetComponent<CardMovementSrc>();


        if (Type != FieldType.SELF_FIELD || card.DropPlace.Type == Type)
            return;

        if (transform.childCount != 0)
        {
            var card1 = transform.GetChild(0).GetComponent<Card>();
            FindObjectOfType<GameManagerSrc>().DestroyCard(card1);
        }



        if (card)
        {

            card.GameManager.CurrentGame.PlayerHand.Remove(cardBase);
            card.GameManager.CurrentGame.PlayerField.Add(cardBase);
            card.DropPlace = this;
            cardBase.gameObject.layer = 8;
            cardBase.transform.SetParent(transform, false);


        }
    }
}
