using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class CardMovementSrc : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    Camera MainCamera;
    Vector3 offset;
    float card_height;
    [HideInInspector] public GameManagerSrc GameManager;
    [HideInInspector] public DropPlaceScrypt DropPlace;
    [SerializeField] private Card CardShow;
    private bool IsDraggable;

    void Awake()
    {
        MainCamera = Camera.allCameras[0];
        GameManager = FindObjectOfType<GameManagerSrc>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        cloneDisappear();
        offset = transform.position - MainCamera.ScreenToWorldPoint(eventData.position);
        offset.z = 500;

        DropPlace = transform.parent.GetComponent<DropPlaceScrypt>();



        IsDraggable = Turn.instance.IsPlayerTurn && (DropPlace!=null) && (DropPlace.Type == FieldType.SELF_HAND ||
            DropPlace.Type == FieldType.SELF_FIELD
            && (CardShow.CardModel as UnitCard).CanAttack);


        if (!IsDraggable)
            return;

        transform.SetParent(transform.parent.parent);
        //GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!IsDraggable || !Turn.instance.IsPlayerTurn)
            return;


        Vector3 newPos = MainCamera.ScreenToWorldPoint(eventData.position);
        transform.position = newPos + offset;


    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(!IsDraggable)
            return ;

        if ( DropPlace == null || !Turn.instance.IsPlayerTurn)
        {
            Vector3 nPos = MainCamera.ScreenToWorldPoint(DropPlace.transform.position);
            nPos.z = 0;
            transform.position = nPos;

            //if(DropPlace != null)
                transform.parent = DropPlace.transform;
            return;
        }

        var a = CardShow;



        //eventData.pointerDrag.GetComponent<>().
       
        transform.SetParent(DropPlace.transform);
        GetComponent<CanvasGroup>().blocksRaycasts = true;


        int layerMask = 1;
        RaycastHit hit;



        switch (a.CardModel)
        {
            case UnitCard b:

                // арта юнит может выкладыватьс€ с руки, или игратьс€ с пол€
                if (DropPlace.Type != FieldType.SELF_FIELD)
                {
                    //если выкладываетс€ с руки
                    layerMask = 1 << 9;
                    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, Mathf.Infinity, layerMask))
                    {
                        hit.transform.GetComponent<DropPlaceBase>()?.OnDrop(a);
                    }
                }
                else
                {
                    //если играетс€ с пол€
                    layerMask = 1 << 8;
                    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, Mathf.Infinity, layerMask))
                    {
                        hit.transform.GetComponent<DropPlaceBase>()?.MyOnDrop(a);
                        transform.SetParent(DropPlace.transform);
                    }
                }

                

                break;
            case MassiveTargetSpell mts:
                layerMask = 1 << 10;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, Mathf.Infinity, layerMask))
                {
                    hit.transform.GetComponent<DropPlaceBase>()?.OnDrop(a);
                }
                break;

            case SingleTargetSpellCard stsc:
                layerMask = 1 << 8;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, Mathf.Infinity, layerMask))
                {

                    hit.transform.GetComponent<DropPlaceBase>()?.OnDrop(a);

                }
                break;

            default:
                throw new NotImplementedException("ќшибка типа бросаемой карты");

        }

        if (eventData != null)
        {
            Vector3 newPos = MainCamera.ScreenToWorldPoint(DropPlace.transform.position);
            newPos.z = 0;
            transform.position = newPos;
            transform.parent = DropPlace.transform;

        }
    }

    //увеличение при нажатии
    public void OnPointerEnter(PointerEventData eventData)
    {
        DropPlaceScrypt LocalDropPlace = transform.parent.GetComponent<DropPlaceScrypt>();
        
        if (LocalDropPlace != null && (LocalDropPlace.Type == FieldType.SELF_HAND ||
              LocalDropPlace.Type == FieldType.SELF_FIELD))
        {
                
                var clone = CardShow.Clone;
                card_height = clone.transform.GetComponent<RectTransform>().rect.height;
                clone.gameObject.SetActive(true);
                clone.transform.DOScale(1.4f, 0.3f);
                clone.transform.DOLocalMoveY(card_height+100, 0);
        }
      
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        cloneDisappear();
    }

    private void cloneDisappear()
    {
        var clone = CardShow.Clone;
        if (clone != null && clone.gameObject.activeSelf)
        {
                clone.transform.DOScale(0.9f, 0.01f).OnComplete(() =>
                {
                    clone.transform.DOLocalMoveY(-card_height-100, 0).OnComplete(() =>
                    {
                        clone.gameObject.SetActive(false);
                    });
                });
        }
        
    }
}
