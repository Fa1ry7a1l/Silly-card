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
    [SerializeField] private CardBase CardShow;
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
        if (!IsDraggable)
            return;


        Vector3 newPos = MainCamera.ScreenToWorldPoint(eventData.position);
        transform.position = newPos + offset;


    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (!IsDraggable || DropPlace == null)
        {
            Vector3 nPos = MainCamera.ScreenToWorldPoint(eventData.position);
            nPos.z = 0;
            transform.position = nPos;
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

                //Карта юнит может выкладываться с руки, или играться с поля
                if (DropPlace.Type != FieldType.SELF_FIELD)
                {
                    //если выкладывается с руки
                    layerMask = 1 << 9;
                }
                else
                {
                    //если играется с поля
                    layerMask = 1 << 8;
                    print("нашли маску для drop");
                }

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, Mathf.Infinity, layerMask))
                {
                    print("вызвали drop для карты");

                    hit.transform.GetComponent<DropPlaceBase>()?.MyOnDrop(a);
                }

                break;

            default:
                throw new NotImplementedException("Ошибка типа бросаемой карты");

        }

        if (eventData != null)
        {
            Vector3 newPos = MainCamera.ScreenToWorldPoint(eventData.position);
            newPos.z = 0;
            transform.position = newPos;
        }
    }

    //увеличение при нажатии
    public void OnPointerEnter(PointerEventData eventData)
    {
        DropPlace = transform.parent.GetComponent<DropPlaceScrypt>();
        
        if (DropPlace != null && (DropPlace.Type == FieldType.SELF_HAND ||
              DropPlace.Type == FieldType.SELF_FIELD))
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
