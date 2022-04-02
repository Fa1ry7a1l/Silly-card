using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DropPlaceBase : MonoBehaviour
{
    public void OnDrop(Card cardBase)
    {
        if (cardBase.TryPlay())
            MyOnDrop(cardBase);
    }

    abstract public void MyOnDrop(Card cardBase);


}
