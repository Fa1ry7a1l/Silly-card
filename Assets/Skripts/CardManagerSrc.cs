using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element
{ Inferno, Metter, Neutral, Time, Void }

public abstract class CardModelBase
{
    public string Name;
    public Sprite Logo;
    public int Manacost;
    protected string logoPath; 
    public CardModelBase(string Name, string logoPath, int manaCost)
    {
        this.Name = Name;
        Logo = Resources.Load<Sprite>(logoPath);
        this.Manacost = manaCost;
        this.logoPath = logoPath;

    }

    public abstract CardModelBase Clone();

}


public class UnitCard : CardModelBase
{
    //public int id;
    
    public int Attack, Defense;
    public Element Element;

    public bool CanAttack;

    public bool IsAlive { get { return Defense > 0; } }

    public UnitCard(string Name, Element element, string logoPath, int attack, int defense, int manaCost, bool canAttack = false):base(Name,logoPath,manaCost)
    {
        Element = element;
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

    public override CardModelBase Clone()
    {
        return new UnitCard(Name, Element, logoPath, Attack, Defense, Manacost, CanAttack);
    }
}

public class CardManagerSrc : MonoBehaviour
{
    public static List<UnitCard> AllCards = new List<UnitCard>();

    public void Awake()
    {
        CardManagerSrc.AllCards.Add(new UnitCard("Àäñêèé êîò", Element.Inferno, "Ñharacters/inferno/inferno_cat", 1, 1, 1));
        CardManagerSrc.AllCards.Add(new UnitCard("Àäñêèé êóëüòèñò", Element.Inferno, "Ñharacters/inferno/inferno_occultist", 2, 1, 2));
        CardManagerSrc.AllCards.Add(new UnitCard("Äğóèä", Element.Metter, "Ñharacters/metter/matter_druid", 3, 2, 2));
        CardManagerSrc.AllCards.Add(new UnitCard("Âåäüìà", Element.Metter, "Ñharacters/metter/matter_witch", 4, 2, 3));
        CardManagerSrc.AllCards.Add(new UnitCard("Îõîòíèê çà ãîëîâàìè", Element.Neutral, "Ñharacters/neutral/neutral_bounty_hunter", 2, 5, 2));
        CardManagerSrc.AllCards.Add(new UnitCard("Ïàëà÷", Element.Neutral, "Ñharacters/neutral/neutral_executioner", 3, 4, 3));
        CardManagerSrc.AllCards.Add(new UnitCard("×àñû", Element.Time, "Ñharacters/time/time_clock", 0, 6, 2));
        CardManagerSrc.AllCards.Add(new UnitCard("Ìîíàõ", Element.Time, "Ñharacters/time/time_monk", 3, 3, 3));
        CardManagerSrc.AllCards.Add(new UnitCard("X", Element.Void, "Ñharacters/void/void_reaver", 5, 3, 4));
        CardManagerSrc.AllCards.Add(new UnitCard("Âèğèäèñ", Element.Void, "Ñharacters/void/void_scolopendra2_0", 2, 6, 3));
    }
}
