using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerBase
{

    void OnTurnStarted()
    {

        foreach (var card in game.PlayerField)
        {
            (card.CardModel as UnitCard).ChangeAttackState(true);
            card.CardShow.HighlightCard();
        }
        mana.FillManaBar();
    }



    void OnTurnEnded()
    {
        foreach (var card in game.PlayerField)
        {
            (card.CardModel as UnitCard).ChangeAttackState(false);
            card.CardShow.DeHighlightCard();
        }
    }
    private void Awake()
    {
        turn.PlayerTurnStarted += OnTurnStarted;
        turn.PlayerTurnEnded += OnTurnEnded;
    }
}
