using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MassiveTargetSpell : CardModelBase
{
    public delegate void CardSpell(Game Game, CardBase.CardOwner Owner);

    public CardSpell Spell;

    public MassiveTargetSpell(string Name, string logoPath, int manaCost, CardSpell spell) : base(Name, logoPath, manaCost)
    {
        this.Spell = spell;
    }

    public override CardModelBase Clone()
    {
        return new MassiveTargetSpell(Name, logoPath, Manacost, Spell);
    }
}