using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BackgroundDrop : DropPlaceBase
{
    [SerializeField] private Game Game;

    public override void MyOnDrop(CardBase cardBase)
    {
        if(cardBase.TryPlay())
        {
            (cardBase.CardModel as MassiveTargetSpell).Spell(Game, cardBase.Owner);
        }
    }
}
