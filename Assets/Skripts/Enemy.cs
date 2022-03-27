using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Game game;
    [SerializeField] private Turn turn;
    [SerializeField] private ManaBar mana;
    void OnTurnStarted(bool isPlayerTurn)
    {
        if (!isPlayerTurn)
        {
            foreach (var card in game.EnemyField)
            {
                card.SelfCard.ChangeAttackState(true);
            }
            mana.FillManaBar();
        }
        
    }


    private void Start()
    {

    }

    void OnTurnEnded(bool isPlayerTurn)
    {
        if (!isPlayerTurn)
        {
            foreach (var card in game.EnemyField)
            {
                card.SelfCard.ChangeAttackState(false);
            }
        }
    }
    private void Awake()
    {
        turn.TurnStarted += OnTurnStarted;
        turn.TurnEnded += OnTurnEnded;
    }
}
