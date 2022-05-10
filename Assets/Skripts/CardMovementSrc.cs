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
    private float card_height;
    private bool CardZoomCompleted;
    private Transform oldParent;
    private int oldSibling;
    private float MoveY;
    private Vector3 oldPos;
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

            transform.SetParent(DropPlace.transform);
            transform.SetAsLastSibling();
            if (DropPlace.Type == FieldType.SELF_FIELD)
                transform.DOMove(newPos, 0.5f);
            else
                transform.position = newPos;

        }
    }

    //увеличение при нажатии
    public void OnPointerEnter(PointerEventData eventData)
    {
        print(eventData.pointerCurrentRaycast.gameObject);
        DropPlaceScrypt LocalDropPlace = transform.parent.GetComponent<DropPlaceScrypt>();

        //CardZoomBeginning
        if (LocalDropPlace != null && (LocalDropPlace.Type == FieldType.SELF_HAND ||
              LocalDropPlace.Type == FieldType.SELF_FIELD ||
              LocalDropPlace.Type == FieldType.ENEMY_FIELD))
        {
            //CardShow.Clone.UpdateVisualData();
            var clone = CardShow.Clone;
            clone.GetComponent<BoxCollider>().enabled = false;
            card_height = clone.transform.GetComponent<RectTransform>().rect.height;
            print(card_height);
            var scale = clone.transform.localScale;
            float scalePar = 1.2f;
            oldPos = transform.position;

            

            MoveY = card_height + 100;
            var oldY = oldPos.y;
            if (oldY + MoveY + card_height*scalePar > Screen.height)
                MoveY = Screen.height - 300 - oldY;

            oldParent = clone.transform.parent;
            oldSibling = clone.transform.GetSiblingIndex();
            clone.transform.SetParent(GameObject.Find("Canvas").transform);
            clone.transform.SetAsLastSibling();

            clone.transform.DOMoveY(oldY+MoveY, 0).OnComplete(() =>
            {
                clone.gameObject.SetActive(true);
                clone.transform.DOScale(scalePar, 0.3f);
                
                CardZoomCompleted = true;
                print("cardzoom true");
            });
            
        }
      
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        cloneDisappear();
    }

    //CardZoomEnding
    private void cloneDisappear()
    {
        var clone = CardShow.Clone;
        if (clone != null && clone.gameObject.activeSelf)//&& CardZoomCompleted)
        {
            clone.transform.DOScale(1f/1.2f, 0.1f).OnComplete(() =>
                {
                    
                    clone.transform.DOMove(oldPos, 0).OnComplete(() =>
                    {
                        clone.transform.SetParent(oldParent);
                        clone.transform.SetSiblingIndex(oldSibling);
                        clone.gameObject.SetActive(false);
                        
                    });
                });
            CardZoomCompleted = false;
            print("cardzoom false");
        }
        
        
    }

    public void MoveToField(Transform field)
    {
        //Transform parent = transform.parent;
        transform.SetParent(field.transform);
        transform.SetAsLastSibling();
        transform.DOMove(field.position, .5f);
    }

    public void MoveToTarget(Transform target, TweenCallback OnCompletedActions, bool WithFrom = true)
    {
        var oldPosition = transform.position;
        var oldParent = transform.parent;
        var oldSibling = transform.GetSiblingIndex();
        if (target.GetComponent<DropPlaceBase>() is PlayerBase)
            transform.SetParent(target.transform.parent);
        else
            transform.SetParent(target.transform);
        transform.SetAsLastSibling();


        if (WithFrom)
        {
            transform.DOMove(target.position, .5f).OnStepComplete(() => {
                System.Threading.Thread.Sleep(100);
                MoveFromTarget(oldPosition, oldParent, oldSibling);
            });

            OnCompletedActions();
        }
        else
        {
            transform.DOMove(target.position, .5f).OnStepComplete(OnCompletedActions);
        }
    }

    
    public void MoveFromTarget(Vector3 oldPosition, Transform oldParent, int oldSibling)
    {
        transform.DOMove(oldPosition, 1f).OnComplete(() =>
        {
            transform.SetParent(oldParent);
            transform.SetSiblingIndex(oldSibling);
        });
    }
}
