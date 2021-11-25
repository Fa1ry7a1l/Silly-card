using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropPlaceScrypt : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        CardScript card = eventData.pointerDrag.GetComponent<CardScript>();

        if(card)
        {
            card.DefaultParent = transform;
        }
    }

}
