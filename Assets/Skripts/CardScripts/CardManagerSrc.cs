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
        //����� �������
        CardManagerSrc.AllUnitCards.Add(new UnitCard("������ ���", Element.Inferno, "�haracters/inferno/inferno_cat", 1, 1, 1));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("������ ��������", Element.Inferno, "�haracters/inferno/inferno_occultist", 2, 1, 2));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("�����", Element.Metter, "�haracters/metter/matter_druid", 3, 2, 2));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("������", Element.Metter, "�haracters/metter/matter_witch", 4, 2, 3));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("������� �� ��������", Element.Neutral, "�haracters/neutral/neutral_bounty_hunter", 2, 5, 2));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("�����", Element.Neutral, "�haracters/neutral/neutral_executioner", 3, 4, 3));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("����", Element.Time, "�haracters/time/time_clock", 0, 6, 2));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("�����", Element.Time, "�haracters/time/time_monk", 3, 3, 3));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("X", Element.Void, "�haracters/void/void_reaver", 5, 3, 4));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("�������", Element.Void, "�haracters/void/void_scolopendra2_0", 2, 6, 3));

        CardManagerSrc.AllMassiveTargetSpellCards.Add(new MassiveTargetSpell("�������", "�haracters/void/texture_void_bug", 2, (Game Game, Card Card) =>
        {
            Card.PlayerBase.Heal(3);
        }));
        CardManagerSrc.AllSingleTargetCards.Add(new SingleTargetSpellCard("3 �����", "�haracters/void/texture_void_dog", 2, (ITarget it) =>
        {
            it.TakeDamage(3);
            it.TakeHeal(2);
        }));

        CardManagerSrc.AllCards.InsertRange(0, CardManagerSrc.AllUnitCards);
        CardManagerSrc.AllCards.InsertRange(0, CardManagerSrc.AllSingleTargetCards);
        CardManagerSrc.AllCards.InsertRange(0, CardManagerSrc.AllMassiveTargetSpellCards);


    }

}
