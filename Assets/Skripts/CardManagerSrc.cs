using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Card
{
    //public int id;
    public string Name;
    //public Sprite Logo;
    public int Attack, Defense;

    public Card(string Name /*, string logoPath*/, int attack, int defense)
    {
        this.Name = Name;
        //Logo = Resources.Load<Sprite>(logoPath);
        this.Attack = attack;
        this.Defense = defense;
    }
}

public class CardManagerSrc : MonoBehaviour
{
    public static List<Card> AllCards = new List<Card>();

    public void Awake()
    {
        CardManagerSrc.AllCards.Add(new Card("A", 1, 1));
        CardManagerSrc.AllCards.Add(new Card("B", 2, 1));
        CardManagerSrc.AllCards.Add(new Card("C", 3, 2));
        CardManagerSrc.AllCards.Add(new Card("D", 4, 2));
        CardManagerSrc.AllCards.Add(new Card("E", 2, 5));
        CardManagerSrc.AllCards.Add(new Card("F", 3, 4));
        CardManagerSrc.AllCards.Add(new Card("G", 0, 6));
        CardManagerSrc.AllCards.Add(new Card("H", 3, 3));
        CardManagerSrc.AllCards.Add(new Card("I", 5, 3));
        CardManagerSrc.AllCards.Add(new Card("J", 2, 6));
    }
}
