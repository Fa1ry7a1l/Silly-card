using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Turn : MonoBehaviour
{
    public event Action<bool> TurnEnded;
    public event Action<bool> TurnStarted;
    private bool isPlayerTurn = false;
    [SerializeField] private int turnTime;
    [SerializeField] private TMPro.TextMeshProUGUI turnTimeText;
    IEnumerator StartTimer()
    {
        int time = turnTime;
        turnTimeText.text = turnTime.ToString();

        while (time-- > 0)
        {
            turnTimeText.text = GetTimerText(time);
            yield return new WaitForSeconds(1);
        }
        ChangeTurn();
    }

    private string GetTimerText(int time)
    {
        if (isPlayerTurn)
            return time.ToString();
        //Pyzyaka
        return "Ход противника";
    }

    public void ChangeTurn()
    {
        StopAllCoroutines();
        TurnEnded?.Invoke(isPlayerTurn);
        isPlayerTurn = !isPlayerTurn;
        TurnStarted?.Invoke(isPlayerTurn);
        StartCoroutine(StartTimer());
    }

    private void Start()
    {
        ChangeTurn();
    }
}
