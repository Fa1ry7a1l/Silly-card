using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BackgroundDrop : DropPlaceBase
{
    [SerializeField] private Game Game;
    [SerializeField] private GameManagerSrc GameManager;

    public override void MyOnDrop(Card cardBase)
    {
        (cardBase.CardModel as MassiveTargetSpell).Spell(Game, cardBase);

        GameManager.DestroyCard(cardBase);
    }

    public override void MyOnDropEnemy(Card cardBase)
    {
        //особенный MoveToTarget
        var card = cardBase.transform.GetComponent<CardMovementSrc>();
        var canvas = GameObject.Find("Canvas").transform;
        transform.SetParent(canvas);
        transform.SetAsLastSibling();

        card.transform.DOMove(new Vector3(Screen.width / 2, Screen.height / 2, 0), .5f).OnComplete(()=>
        {
            (cardBase.CardModel as MassiveTargetSpell).Spell(Game, cardBase);

            GameManager.DestroyCard(cardBase);
        }
        );
       
    }
}
