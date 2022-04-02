using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public class SingleTargetSpellCard : CardModelBase
{
    public delegate void CardSpell();

    protected CardSpell Spell;

    public SingleTargetSpellCard(string Name, string logoPath, int manaCost, CardSpell spell) : base(Name, logoPath, manaCost)
    {
        this.Spell = spell;
    }



    public override CardModelBase Clone()
    {
        return new SingleTargetSpellCard(Name, logoPath, Manacost, Spell);
    }
}
