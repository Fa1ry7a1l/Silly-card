using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackedCard : DropPlaceBase
{
    //сюда могут попадать юниты или заклинания
    public override void MyOnDrop(CardBase cardBase)
    {

        if (cardBase.CardModel is UnitCard uc && uc.CanAttack /*&&
                transform.parent.GetComponent<DropPlaceScrypt>().Type == FieldType.ENEMY_FIELD*/)
        {
            uc.ChangeAttackState(false);
            FindObjectOfType<GameManagerSrc>().CardsFidht(cardBase, GetComponent<CardBase>());
        }
    }


    /*CardShowSrc card = eventData.pointerDrag.GetComponent<CardShowSrc>();

    if(card != null && 
        card.SelfCard.CanAttack&&
        transform.parent.GetComponent<DropPlaceScrypt>().Type == FieldType.ENEMY_FIELD)
    {
        card.SelfCard.ChangeAttackState(false);
        FindObjectOfType<GameManagerSrc>().CardsFidht(card, GetComponent<CardShowSrc>());
    }*/
}
