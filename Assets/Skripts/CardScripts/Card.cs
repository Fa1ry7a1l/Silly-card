using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;



public  class Card : MonoBehaviour
{

    public enum CardOwner {Player,Enemy };
    public CardOwner Owner;
    public CardShowSrc CardShow;
    public CardModelBase CardModel;
    [HideInInspector] public Card Clone;

    [HideInInspector]public PlayerBase PlayerBase;

    public void Init(CardModelBase cm,CardOwner owner)
    {
        CardModel = cm.Clone();
        CardShow.ShowCardInfo(cm);
        Owner = owner;
    }


    public bool TryPlay()
    {
        return PlayerBase.TryPlay(CardModel);
    }

    public override bool Equals(object other)
    {
        if (other is Card card)
            return this.CardModel.Equals(card.CardModel);
        return false;
    }


}
