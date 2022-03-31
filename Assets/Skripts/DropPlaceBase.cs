using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DropPlaceBase : MonoBehaviour
{
    abstract public void MyOnDrop(PointerEventData eventData);


}
