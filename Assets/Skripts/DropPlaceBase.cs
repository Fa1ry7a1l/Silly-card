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
    public bool OnDropEnemy(Card cardBase)
    {
        if (cardBase.TryPlay())
        {
            MyOnDropEnemy(cardBase);
            return true;
        }
        return false;
    }

    abstract public void MyOnDrop(Card cardBase);
    abstract public void MyOnDropEnemy(Card cardBase);


}
