using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultObj:MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Text;
    [SerializeField] private Image Background;
    [SerializeField] private PlayerBase Player;
    [SerializeField] private PlayerBase Enemy;

    private void Awake()
    {
        Player.OnDeath += EnemyWon;
        Enemy.OnDeath += UserWon;
        print("Сработал Awake");
        Background.gameObject.SetActive(false);

    }

    public void UserWon()
    {
        Background.gameObject.SetActive(true);
        Text.text = "Победа!";
    }

    public void EnemyWon()
    {
        Background.gameObject.SetActive(true);
        Text.text = "Поражение.";
    }
}

