using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CardManagerSrc : MonoBehaviour
{
    public static List<CardModelBase> AllCards = new List<CardModelBase>();
    public static List<CardModelBase> AllUnitCards = new List<CardModelBase>();
    public static List<CardModelBase> AllSingleTargetCards = new List<CardModelBase>();
    public static List<CardModelBase> AllMassiveTargetSpellCards = new List<CardModelBase>();


    public static void GenerateCards()
    {
        //êàðòû ñóùåñòâ
        CardManagerSrc.AllUnitCards.Add(new UnitCard("Àäñêèé êîò", Element.Inferno, "Ñharacters/inferno/inferno_cat", 1, 1, 1));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("Àäñêèé êóëüòèñò", Element.Inferno, "Ñharacters/inferno/inferno_occultist", 2, 1, 2));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("Áëåéçèêåí", Element.Inferno, "Ñharacters/inferno/texture_inferno_fighter", 5, 5, 6));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("Âóóô", Element.Inferno, "Ñharacters/inferno/texture_inferno_robot", 4, 2, 3));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("Äðóèä", Element.Metter, "Ñharacters/metter/matter_druid", 3, 2, 2));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("Âåäüìà", Element.Metter, "Ñharacters/metter/matter_witch", 4, 2, 3));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("Ñîâóõ", Element.Metter, "Ñharacters/metter/texture_matter_owl", 3, 5, 3));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("Òåí Áåí", Element.Metter, "Ñharacters/metter/texture_matter_tree", 6, 3, 4));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("Îõîòíèê çà ãîëîâàìè", Element.Neutral, "Ñharacters/neutral/neutral_bounty_hunter", 2, 5, 2));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("Ïàëà÷", Element.Neutral, "Ñharacters/neutral/neutral_executioner", 3, 4, 3));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("Ìåëåíèÿ", Element.Neutral, "Ñharacters/neutral/texture_neutral_knight", 4, 7, 6));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("Ðîëàíä", Element.Neutral, "Ñharacters/neutral/texture_neutral_viking", 4, 2, 2));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("×àñû", Element.Time, "Ñharacters/time/time_clock", 1, 6, 2));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("Ìîíàõ", Element.Time, "Ñharacters/time/time_monk", 3, 3, 3));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("Ìîëòðåñ", Element.Time, "Ñharacters/time/texture_time_crow", 2, 5, 3));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("ÌåëÌåòàë", Element.Time, "Ñharacters/time/texture_time_watchmacker", 6, 10, 8));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("X", Element.Void, "Ñharacters/void/void_reaver", 5, 3, 4));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("Âèðèäèñ", Element.Void, "Ñharacters/void/void_scolopendra2_0", 2, 6, 3));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("Æó÷àðà", Element.Void, "Ñharacters/void/texture_void_bug", 2, 1, 1));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("Âîñüìîé", Element.Void, "Ñharacters/void/texture_void_dog", 8, 7, 7));

        CardManagerSrc.AllMassiveTargetSpellCards.Add(new MassiveTargetSpell("Ëå÷åíèå", "Spells/Shield", 3,"Âîñòòàíàâëèâàåò 3 çäîðîâüÿ èãðîêó", (Game Game, Card Card) =>
        {
            Card.PlayerBase.Heal(3);
        }));
        CardManagerSrc.AllMassiveTargetSpellCards.Add(new MassiveTargetSpell("Âçðûâ", "Spells/Explotion", 2, "Íàíîñèò 3 óðîíà ïðîòèâíèêó", (Game Game, Card Card) =>
        {
            if(Card.Owner == Card.CardOwner.Player)
            {
                Game.Enemy.Damage(3);
            }
            else
            {
                Game.Player.Damage(3);
            }
        }));

        CardManagerSrc.AllSingleTargetCards.Add(new SingleTargetSpellCard("Ñòðàííîå çåëüå", "Spells/Potion", 2,"Íàíîñèò 3 óðîíà, âîññòàíàâëèâàåò 2", (ITarget it) =>
        {
            it.TakeDamage(3);
            it.TakeHeal(2);
        }));

        CardManagerSrc.AllSingleTargetCards.Add(new SingleTargetSpellCard("Õëûñò", "Spells/Whip", 2, "Íàíîñèò 2 óðîíà, óðîíà", (ITarget it) =>
        {
            it.TakeDamage(2);

        }));


        CardManagerSrc.AllCards.InsertRange(0, CardManagerSrc.AllUnitCards);
        CardManagerSrc.AllCards.InsertRange(0, CardManagerSrc.AllSingleTargetCards);
        CardManagerSrc.AllCards.InsertRange(0, CardManagerSrc.AllMassiveTargetSpellCards);


    }

}
