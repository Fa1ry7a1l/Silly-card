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
        DeckCards.Add(new DeckCard("Адский кот", "cat"));
        DeckCards.Add(new DeckCard("Адский культист", "occultist"));
        DeckCards.Add(new DeckCard("Блейзикен", "fighter"));
        DeckCards.Add(new DeckCard("Вууф", "vuuf"));
        DeckCards.Add(new DeckCard("Друид", "druid"));
        DeckCards.Add(new DeckCard("Ведьма", "witch"));
        DeckCards.Add(new DeckCard("Совух", "owl"));
        DeckCards.Add(new DeckCard("Тен Бен", "tenben"));
        DeckCards.Add(new DeckCard("Охотник за головами", "bountyhunter"));
        DeckCards.Add(new DeckCard("Палач", "executor"));
        DeckCards.Add(new DeckCard("Меления", "knight"));
        DeckCards.Add(new DeckCard("Роланд", "viking"));
        DeckCards.Add(new DeckCard("Часы", "clock"));
        DeckCards.Add(new DeckCard("Монах", "monk"));
        DeckCards.Add(new DeckCard("Молтрес", "phoenix"));
        DeckCards.Add(new DeckCard("МелМетал", "watchmacker"));
        DeckCards.Add(new DeckCard("X", "x"));
        DeckCards.Add(new DeckCard("Виридис", "scolopendra"));
        DeckCards.Add(new DeckCard("Жучара", "bug"));
        DeckCards.Add(new DeckCard("Восьмой", "dog"));

        DeckCards.Add(new DeckCard("Лечение", "shield"));
        DeckCards.Add(new DeckCard("Взрыв", "explotion"));

        DeckCards.Add(new DeckCard("Странное зелье", "poution"));

        DeckCards.Add(new DeckCard("Хлыст", "whip"));
    }
}
