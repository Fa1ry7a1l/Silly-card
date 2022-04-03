using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public class SingleTargetSpellCard : CardModelBase
{
    public delegate void CardSpell(ITarget it);

    public CardSpell Spell;

    public SingleTargetSpellCard(string Name, string logoPath, int manaCost, string description, CardSpell spell) : base(Name, logoPath, manaCost, description)
    {
        this.Spell = spell;
    }



    public override CardModelBase Clone()
    {
        return new SingleTargetSpellCard(Name, logoPath, Manacost, Description, Spell);
    }
}
