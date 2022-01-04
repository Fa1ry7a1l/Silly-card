using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element
{ Inferno, Metter, Neutral, Time, Void }


public struct Card
{
    //public int id;
    public string Name;
    public Sprite Logo;
    public int Attack, Defense, Manacost;
    public Element Element;

    public bool CanAttack;

    public bool IsAlive { get { return Defense > 0; } }

    public Card(string Name, Element element, string logoPath, int attack, int defense, int manaCost, bool canAttack = false)
    {
        this.Name = Name;
        Element = element;
        Logo = Resources.Load<Sprite>(logoPath);
        this.Attack = attack;
        this.Defense = defense;
        this.CanAttack = canAttack;
        this.Manacost = manaCost;
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
        CardManagerSrc.AllCards.Add(new Card("������ ���", Element.Inferno, "�haracters/inferno/inferno_cat", 1, 1, 1));
        CardManagerSrc.AllCards.Add(new Card("������ ��������", Element.Inferno, "�haracters/inferno/inferno_occultist", 2, 1, 2));
        CardManagerSrc.AllCards.Add(new Card("�����", Element.Metter, "�haracters/metter/matter_druid", 3, 2, 2));
        CardManagerSrc.AllCards.Add(new Card("������", Element.Metter, "�haracters/metter/matter_witch", 4, 2, 3));
        CardManagerSrc.AllCards.Add(new Card("������� �� ��������", Element.Neutral, "�haracters/neutral/neutral_bounty_hunter", 2, 5, 2));
        CardManagerSrc.AllCards.Add(new Card("�����", Element.Neutral, "�haracters/neutral/neutral_executioner", 3, 4, 3));
        CardManagerSrc.AllCards.Add(new Card("����", Element.Time, "�haracters/time/time_clock", 0, 6, 2));
        CardManagerSrc.AllCards.Add(new Card("�����", Element.Time, "�haracters/time/time_monk", 3, 3, 3));
        CardManagerSrc.AllCards.Add(new Card("X", Element.Void, "�haracters/void/void_reaver", 5, 3, 4));
        CardManagerSrc.AllCards.Add(new Card("�������", Element.Void, "�haracters/void/void_scolopendra2_0", 2, 6, 3));
    }
}
