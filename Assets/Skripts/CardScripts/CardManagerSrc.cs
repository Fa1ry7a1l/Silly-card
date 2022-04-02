using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CardManagerSrc : MonoBehaviour
{
    public static List<CardModelBase> AllCards = new List<CardModelBase>();


    public static void GenerateCards()
    {
        //êàðòû ñóùåñòâ
        CardManagerSrc.AllCards.Add(new UnitCard("Àäñêèé êîò", Element.Inferno, "Ñharacters/inferno/inferno_cat", 1, 1, 1));
       /* CardManagerSrc.AllCards.Add(new UnitCard("Àäñêèé êóëüòèñò", Element.Inferno, "Ñharacters/inferno/inferno_occultist", 2, 1, 2));*/
        CardManagerSrc.AllCards.Add(new UnitCard("Äðóèä", Element.Metter, "Ñharacters/metter/matter_druid", 3, 2, 2));
        /*CardManagerSrc.AllCards.Add(new UnitCard("Âåäüìà", Element.Metter, "Ñharacters/metter/matter_witch", 4, 2, 3));
        CardManagerSrc.AllCards.Add(new UnitCard("Îõîòíèê çà ãîëîâàìè", Element.Neutral, "Ñharacters/neutral/neutral_bounty_hunter", 2, 5, 2));
        CardManagerSrc.AllCards.Add(new UnitCard("Ïàëà÷", Element.Neutral, "Ñharacters/neutral/neutral_executioner", 3, 4, 3));
        CardManagerSrc.AllCards.Add(new UnitCard("×àñû", Element.Time, "Ñharacters/time/time_clock", 0, 6, 2));
        CardManagerSrc.AllCards.Add(new UnitCard("Ìîíàõ", Element.Time, "Ñharacters/time/time_monk", 3, 3, 3));
        CardManagerSrc.AllCards.Add(new UnitCard("X", Element.Void, "Ñharacters/void/void_reaver", 5, 3, 4));
        CardManagerSrc.AllCards.Add(new UnitCard("Âèðèäèñ", Element.Void, "Ñharacters/void/void_scolopendra2_0", 2, 6, 3));

        CardManagerSrc.AllCards.Add(new MassiveTargetSpell("Ëå÷åíèå", "Ñharacters/void/texture_void_bug", 2, (Game Game, Card Card) =>
        {
            Card.PlayerBase.Heal(3);
        }));*/
        CardManagerSrc.AllCards.Add(new SingleTargetSpellCard("3 óðîíà", "Ñharacters/void/texture_void_dog", 2, (ITarget it) =>
        {
            it.TakeDamage(3);
            it.TakeHeal(2);
        }));


    }

}
