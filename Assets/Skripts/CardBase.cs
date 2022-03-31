using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public  class CardBase : MonoBehaviour
{
    public CardShowSrc CardShow;
    public CardModelBase CardModel;
    [HideInInspector]public PlayerBase PlayerBase;

    public void Init(CardModelBase cm)
    {
        CardModel = cm;
        CardShow.ShowCardInfo(cm);
    }


    public bool TryPlay()
    {
        return PlayerBase.TryPlay(CardModel);
    }

}
