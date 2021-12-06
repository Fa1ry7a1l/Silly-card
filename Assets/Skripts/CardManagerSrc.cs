using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Card
{
    //public int id;
    public string Name;
    public Sprite Logo;
    public int Attack, Defense;

    public bool CanAttack;

    public bool IsAlive { get { return Defense > 0; } }

    public Card(string Name, string logoPath, int attack, int defense, bool canAttack = false)
    {
        this.Name = Name;
        Logo = Resources.Load<Sprite>(logoPath);
        this.Attack = attack;
        this.Defense = defense;
        this.CanAttack = canAttack;
    }

    public void ChangeAttackState(bool can)
    {
        CanAttack = can;
    }

    public void GetDamage(int dmg)
    {
        Defense -= dmg;
    }
}

public class CardManagerSrc : MonoBehaviour
{
    public static List<Card> AllCards = new List<Card>();

    public void Awake()
    {
        CardManagerSrc.AllCards.Add(new Card("������ ���", "�haracters/inferno/inferno_cat", 1, 1));
        CardManagerSrc.AllCards.Add(new Card("������ ��������", "�haracters/inferno/inferno_occultist", 2, 1));
        CardManagerSrc.AllCards.Add(new Card("�����", "�haracters/metter/matter_druid", 3, 2));
        CardManagerSrc.AllCards.Add(new Card("������", "�haracters/metter/matter_witch", 4, 2));
        CardManagerSrc.AllCards.Add(new Card("������� �� ��������", "�haracters/neutral/neutral_bounty_hunter", 2, 5));
        CardManagerSrc.AllCards.Add(new Card("�����", "�haracters/neutral/neutral_executioner", 3, 4));
        CardManagerSrc.AllCards.Add(new Card("����", "�haracters/time/time_clock", 0, 6));
        CardManagerSrc.AllCards.Add(new Card("�����", "�haracters/time/time_monk", 3, 3));
        CardManagerSrc.AllCards.Add(new Card("X", "�haracters/void/void_reaver", 5, 3));
        CardManagerSrc.AllCards.Add(new Card("�������", "�haracters/void/void_scolopendra2_0", 2, 6));
    }
}
