using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : PlayerBase
{

    void OnTurnStarted()
    {
        foreach (var card in game.EnemyField)
        {
            (card.CardModel as UnitCard).ChangeAttackState(true);
        }
        mana.FillManaBar();

    }



    void OnTurnEnded()
    {

        foreach (var card in game.EnemyField)
        {
            (card.CardModel as UnitCard).ChangeAttackState(false);
        }
    }

    private void Awake()
    {
        turn.EnemyTurnStarted += OnTurnStarted;
        turn.EnemyTurnEnded += OnTurnEnded;

    }
}
