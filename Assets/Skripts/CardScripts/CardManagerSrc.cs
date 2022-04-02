using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CardManagerSrc : MonoBehaviour
{
    public static List<CardModelBase> AllCards = new List<CardModelBase>();


    public static void GenerateCards()
    {
        //����� �������
        CardManagerSrc.AllCards.Add(new UnitCard("������ ���", Element.Inferno, "�haracters/inferno/inferno_cat", 1, 1, 1));
        CardManagerSrc.AllCards.Add(new UnitCard("������ ��������", Element.Inferno, "�haracters/inferno/inferno_occultist", 2, 1, 2));
        CardManagerSrc.AllCards.Add(new UnitCard("�����", Element.Metter, "�haracters/metter/matter_druid", 3, 2, 2));
        CardManagerSrc.AllCards.Add(new UnitCard("������", Element.Metter, "�haracters/metter/matter_witch", 4, 2, 3));
        CardManagerSrc.AllCards.Add(new UnitCard("������� �� ��������", Element.Neutral, "�haracters/neutral/neutral_bounty_hunter", 2, 5, 2));
        CardManagerSrc.AllCards.Add(new UnitCard("�����", Element.Neutral, "�haracters/neutral/neutral_executioner", 3, 4, 3));
        CardManagerSrc.AllCards.Add(new UnitCard("����", Element.Time, "�haracters/time/time_clock", 0, 6, 2));
        CardManagerSrc.AllCards.Add(new UnitCard("�����", Element.Time, "�haracters/time/time_monk", 3, 3, 3));
        CardManagerSrc.AllCards.Add(new UnitCard("X", Element.Void, "�haracters/void/void_reaver", 5, 3, 4));
        CardManagerSrc.AllCards.Add(new UnitCard("�������", Element.Void, "�haracters/void/void_scolopendra2_0", 2, 6, 3));

        CardManagerSrc.AllCards.Add(new MassiveTargetSpell("�������", "�haracters/void/texture_void_bug", 2, (Game Game, CardBase.CardOwner Owner) =>
        {
            if (Owner == CardBase.CardOwner.Player)
            {
                Game.Player.Heal(3);
            }
            else
            {
                Game.Enemy.Heal(3);
            }
        }));
    }

}
