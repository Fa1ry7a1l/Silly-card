using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PlayerBase
{

    void OnTurnStarted()
    {
            foreach (var card in game.EnemyField)
            {
                card.SelfCard.ChangeAttackState(true);
            }
            mana.FillManaBar();
        
    }



    void OnTurnEnded()
    {
        
            foreach (var card in game.EnemyField)
            {
                card.SelfCard.ChangeAttackState(false);
            }
    }
    private void Awake()
    {
        turn.EnemyTurnStarted += OnTurnStarted;
        turn.EnemyTurnEnded += OnTurnEnded;
    }
}
