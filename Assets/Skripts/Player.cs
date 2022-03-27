using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Game game;
    [SerializeField] private Turn turn;
    [SerializeField] private ManaBar mana;
    void OnTurnStarted(bool isPlayerTurn)
    {
        if (isPlayerTurn)
        {
            foreach (var card in game.PlayerField)
            {
                card.SelfCard.ChangeAttackState(true);
                card.HighlightCard();
            }
            mana.FillManaBar();
        }
        
    }
    private void Start()
    {
    }

    void OnTurnEnded(bool isPlayerTurn)
    {
        if (isPlayerTurn)
        {
            foreach (var card in game.PlayerField)
            {
                card.SelfCard.ChangeAttackState(false);
                card.DeHighlightCard();
            }
        }
    }
    private void Awake()
    {
        turn.TurnStarted += OnTurnStarted;
        turn.TurnEnded += OnTurnEnded;
    }
}
