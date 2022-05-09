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
        //опять большой вопрос
        (cardBase.CardModel as MassiveTargetSpell).Spell(Game, cardBase);

        GameManager.DestroyCard(cardBase);
    }
}
