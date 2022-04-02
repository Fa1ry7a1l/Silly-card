using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum Element
{ Inferno, Metter, Neutral, Time, Void }




public class UnitCard : CardModelBase
{
    //public int id;

    public int Attack, Defense;
    public Element Element;

    public bool CanAttack;

    public bool IsAlive { get { return Defense > 0; } }

    public UnitCard(string Name, Element element, string logoPath, int attack, int defense, int manaCost, bool canAttack = false) : base(Name, logoPath, manaCost)
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
