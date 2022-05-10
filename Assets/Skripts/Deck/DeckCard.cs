using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckCard
{
    public string CardName;
    public Sprite CardSprite { get; private set; }
    public Sprite CardLineSprite { get; private set; }

    public DeckCard(string name, string subname)
    {
        this.CardName = name;
        this.CardSprite = Resources.Load<Sprite>("cards characters/card_" + subname);
        this.CardLineSprite = Resources.Load<Sprite>("lines characters/line_" + subname);
    }
}

public class DeckCardManager
{
    public static List<DeckCard> DeckCards = new List<DeckCard>();
    public static void GenerateDeck()
    {
        DeckCards.Add(new DeckCard("������ ���", "cat"));
        DeckCards.Add(new DeckCard("������ ��������", "occultist"));
        DeckCards.Add(new DeckCard("���������", "fighter"));
        DeckCards.Add(new DeckCard("����", "vuuf"));
        DeckCards.Add(new DeckCard("�����", "druid"));
        DeckCards.Add(new DeckCard("������", "witch"));
        DeckCards.Add(new DeckCard("�����", "owl"));
        DeckCards.Add(new DeckCard("��� ���", "tenben"));
        DeckCards.Add(new DeckCard("������� �� ��������", "bountyhunter"));
        DeckCards.Add(new DeckCard("�����", "executor"));
        DeckCards.Add(new DeckCard("�������", "knight"));
        DeckCards.Add(new DeckCard("������", "viking"));
        DeckCards.Add(new DeckCard("����", "clock"));
        DeckCards.Add(new DeckCard("�����", "monk"));
        DeckCards.Add(new DeckCard("�������", "phoenix"));
        DeckCards.Add(new DeckCard("��������", "watchmacker"));
        DeckCards.Add(new DeckCard("X", "x"));
        DeckCards.Add(new DeckCard("�������", "scolopendra"));
        DeckCards.Add(new DeckCard("������", "bug"));
        DeckCards.Add(new DeckCard("�������", "dog"));

        DeckCards.Add(new DeckCard("�������", "shield"));
        DeckCards.Add(new DeckCard("�����", "explotion"));

        DeckCards.Add(new DeckCard("�������� �����", "poution"));

        DeckCards.Add(new DeckCard("�����", "whip"));
    }
}
