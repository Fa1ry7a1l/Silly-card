using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Turn : MonoBehaviour
{
    public event Action PlayerTurnEnded;
    public event Action PlayerTurnStarted;

    public event Action EnemyTurnEnded;
    public event Action EnemyTurnStarted;

    public bool IsPlayerTurn { get; private set; } = false;
    public static Turn instance;
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
        if (IsPlayerTurn)
            return time.ToString();
        //Pyzyaka
        return "Ход противника";
    }

    public void ChangeTurn()
    {
        StopAllCoroutines();
        if(IsPlayerTurn)
        {
            PlayerTurnEnded?.Invoke();
        }
        else
        {
            EnemyTurnEnded?.Invoke();
        }
        IsPlayerTurn = !IsPlayerTurn;
        if (IsPlayerTurn)
        {
            PlayerTurnStarted?.Invoke();
        }
        else
        {
            EnemyTurnStarted?.Invoke();
        }
        StartCoroutine(StartTimer());
    }

    private void Start()
    {
        ChangeTurn();
    }

    private void Awake()
    {
        instance = this;
    }
}
