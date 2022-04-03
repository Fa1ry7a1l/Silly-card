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
        CardManagerSrc.AllUnitCards.Add(new UnitCard("���������", Element.Inferno, "�haracters/inferno/texture_inferno_fighter", 5, 5, 6));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("����", Element.Inferno, "�haracters/inferno/texture_inferno_robot", 4, 2, 3));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("�����", Element.Metter, "�haracters/metter/matter_druid", 3, 2, 2));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("������", Element.Metter, "�haracters/metter/matter_witch", 4, 2, 3));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("�����", Element.Metter, "�haracters/metter/texture_matter_owl", 3, 5, 3));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("��� ���", Element.Metter, "�haracters/metter/texture_matter_tree", 6, 3, 4));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("������� �� ��������", Element.Neutral, "�haracters/neutral/neutral_bounty_hunter", 2, 5, 2));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("�����", Element.Neutral, "�haracters/neutral/neutral_executioner", 3, 4, 3));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("�������", Element.Neutral, "�haracters/neutral/texture_neutral_knight", 4, 7, 6));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("������", Element.Neutral, "�haracters/neutral/texture_neutral_viking", 4, 2, 2));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("����", Element.Time, "�haracters/time/time_clock", 1, 6, 2));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("�����", Element.Time, "�haracters/time/time_monk", 3, 3, 3));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("�������", Element.Time, "�haracters/time/texture_time_crow", 2, 5, 3));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("��������", Element.Time, "�haracters/time/texture_time_watchmacker", 6, 10, 8));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("X", Element.Void, "�haracters/void/void_reaver", 5, 3, 4));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("�������", Element.Void, "�haracters/void/void_scolopendra2_0", 2, 6, 3));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("������", Element.Void, "�haracters/void/texture_void_bug", 2, 1, 1));
        CardManagerSrc.AllUnitCards.Add(new UnitCard("�������", Element.Void, "�haracters/void/texture_void_dog", 8, 7, 7));

        CardManagerSrc.AllMassiveTargetSpellCards.Add(new MassiveTargetSpell("�������", "Spells/Shield", 3,"��������������� 3 �������� ������", (Game Game, Card Card) =>
        {
            Card.PlayerBase.Heal(3);
        }));
        CardManagerSrc.AllMassiveTargetSpellCards.Add(new MassiveTargetSpell("�����", "Spells/Explotion", 2, "������� 3 ����� ����������", (Game Game, Card Card) =>
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

        CardManagerSrc.AllSingleTargetCards.Add(new SingleTargetSpellCard("�������� �����", "Spells/Potion", 2,"������� 3 �����, ��������������� 2", (ITarget it) =>
        {
            it.TakeDamage(3);
            it.TakeHeal(2);
        }));

        CardManagerSrc.AllSingleTargetCards.Add(new SingleTargetSpellCard("�����", "Spells/Whip", 2, "������� 2 �����, �����", (ITarget it) =>
        {
            it.TakeDamage(2);

        }));


        CardManagerSrc.AllCards.InsertRange(0, CardManagerSrc.AllUnitCards);
        CardManagerSrc.AllCards.InsertRange(0, CardManagerSrc.AllSingleTargetCards);
        CardManagerSrc.AllCards.InsertRange(0, CardManagerSrc.AllMassiveTargetSpellCards);


    }

}
