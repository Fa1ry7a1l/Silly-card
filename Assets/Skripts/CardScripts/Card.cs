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
    [HideInInspector]public PlayerBase PlayerBase;

    public void Init(CardModelBase cm,CardOwner owner)
    {
        CardModel = cm;
        CardShow.ShowCardInfo(cm);
        Owner = owner;
    }


    public bool TryPlay()
    {
        return PlayerBase.TryPlay(CardModel);
    }

}
