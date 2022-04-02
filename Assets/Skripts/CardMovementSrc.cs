using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovementSrc : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Camera MainCamera;
    Vector3 offset;
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
        offset = transform.position - MainCamera.ScreenToWorldPoint(eventData.position);
        offset.z = 500;

        DropPlace = transform.parent.GetComponent<DropPlaceScrypt>();



        IsDraggable = Turn.instance.IsPlayerTurn && (DropPlace.Type == FieldType.SELF_HAND ||
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



        if (!IsDraggable)
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

                //����� ���� ����� ������������� � ����, ��� �������� � ����
                if (DropPlace.Type != FieldType.SELF_FIELD)
                {
                    //���� ������������� � ����
                    layerMask = 1 << 9;
                }
                else
                {
                    //���� �������� � ����
                    layerMask = 1 << 8;
                }

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, Mathf.Infinity, layerMask))
                {
                    hit.transform.GetComponent<DropPlaceBase>()?.MyOnDrop(a);
                }

                break;
            case MassiveTargetSpell mts:
                layerMask = 1 << 10;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, Mathf.Infinity, layerMask))
                {
                    print("����� �����+++++++");

                    hit.transform.GetComponent<DropPlaceBase>()?.MyOnDrop(a);
                    print("����� �����--------------");
                    GameManager.DestroyCard(a);
                }
                break;

            default:
                throw new NotImplementedException("������ ���� ��������� �����");

        }

        if (eventData != null)
        {
            Vector3 newPos = MainCamera.ScreenToWorldPoint(eventData.position);
            newPos.z = 0;
            transform.position = newPos;
        }
    }

}
