using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerBase
{

    void OnTurnStarted()
    {
        
            foreach (var card in game.PlayerField)
            {
                card.SelfCard.ChangeAttackState(true);
                card.HighlightCard();
            }
            mana.FillManaBar();
    }



    void OnTurnEnded()
    {
            foreach (var card in game.PlayerField)
            {
                card.SelfCard.ChangeAttackState(false);
                card.DeHighlightCard();
            }
    }
    private void Awake()
    {
        turn.PlayerTurnStarted += OnTurnStarted;
        turn.PlayerTurnEnded += OnTurnEnded;
    }
}
