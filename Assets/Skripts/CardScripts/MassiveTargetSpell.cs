using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MassiveTargetSpell : CardModelBase
{
    public delegate void CardSpell(Game Game, Card Card);

    public CardSpell Spell;

    public MassiveTargetSpell(string Name, string logoPath, int manaCost, string description, CardSpell spell) : base(Name, logoPath, manaCost, description)
    {
        this.Spell = spell;
    }

    public override CardModelBase Clone()
    {
        return new MassiveTargetSpell(Name, logoPath, Manacost, Description, Spell);
    }
}