using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardView: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image Logo;
    [SerializeField] public Image Selected;
    [SerializeField] public string Name { get; private set; }
    [HideInInspector] public Transform MainParent;
    private Vector3 offset;
    public int position { get; private set; }

    public void Init(DeckCard card, bool selected,int position)
    {
        if (Selected.IsActive())
        {
            return;
        }
        Name = card.CardName;
        Logo.sprite = card.CardSprite;
        Selected.gameObject.SetActive(selected);
        Selected.sprite = card.CardSprite;
        this.position = position;
    }

    public void PlaceBack()
    {
        this.transform.SetParent(this.MainParent);
        this.transform.SetSiblingIndex(this.position);
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Selected.IsActive())
        {
            return;
        }

        MainParent = transform.parent;
        offset = transform.position - Camera.main.ScreenToWorldPoint(eventData.position);
        transform.SetParent(transform.parent.parent.parent.parent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Selected.IsActive())
        {
            return;
        }
        Vector3 newPos = Camera.main.ScreenToWorldPoint(eventData.position);
        transform.position = newPos + offset;
        //transform.position = Camera.main.ScreenToWorldPoint(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Selected.IsActive())
        {
            return;
        }

        
        RaycastHit hit;

        int layerMask = 1 << 6;
        Vector3 pos = transform.position;
        pos.z = pos.z + 1000;
        transform.position = pos;
        if (Physics.Raycast(pos, transform.TransformDirection(Vector3.back), out hit, Mathf.Infinity, layerMask))
        {
            hit.transform.GetComponent<MyDeckDrop>()?.TryDrop(this);
        }
        else
        {
            PlaceBack();
        }
        pos.z = pos.z - 1000;
        transform.position = pos;


        //throw new NotImplementedException();
    }
}
