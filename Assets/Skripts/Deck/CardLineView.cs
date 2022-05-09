using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardLineView:MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image Logo;
    [SerializeField] private TextMeshProUGUI Text;
    [SerializeField] public string Name { get; private set; }
    [HideInInspector] private Transform parent;

    public void Init(DeckCard card)
    {
        Logo.sprite = card.CardLineSprite;
        Name = card.CardName;
        Text.text = card.CardName;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //if (Selected.IsActive())
        //{
        //    return;
        //}

        //offset = transform.position - Camera.main.ScreenToWorldPoint(eventData.position);
        parent = transform.parent;
        transform.SetParent(transform.parent.parent);
    }

    public void PlaceBack()
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        //if (Selected.IsActive())
        //{
        //    return;
        //}
        //Vector3 newPos = Camera.main.ScreenToWorldPoint(eventData.position);
        //transform.position = newPos + offset;
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        RaycastHit hit;

        int layerMask = 1 << 7;
        Vector3 pos = transform.position;
        pos.z = pos.z + 1000;
        transform.position = pos;
        if (Physics.Raycast(pos, transform.TransformDirection(Vector3.back), out hit, Mathf.Infinity, layerMask))
        {
            hit.transform.GetComponent<AllCardsDrop>()?.TryDrop(this);
        }
        else
        {
            PlaceBack();
        }
        pos.z = pos.z - 1000;
        transform.position = pos;

    }
}
