using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurnButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Turn turn;
    public void OnClick()
    {
        turn.ChangeTurn();
    }
    private void Awake()
    {
        turn.TurnStarted += SetActiveState;
    }

    void SetActiveState(bool isPlayerTurn)
    {
        button.enabled = isPlayerTurn;
    }

}
