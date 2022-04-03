using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class ResultObj:MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Text;
    [SerializeField] private PlayerBase Player;
    [SerializeField] private PlayerBase Enemy;

    private void Awake()
    {
        Player.OnDeath += EnemyWon;
        Enemy.OnDeath += UserWon;
        print("Сработал Awake");
        gameObject.SetActive(false);

    }

    public void UserWon()
    {
        gameObject.SetActive(true);
        Text.text = "Победа!";
    }

    public void EnemyWon()
    {
        gameObject.SetActive(true);
        Text.text = "Поражение.";
    }
}

