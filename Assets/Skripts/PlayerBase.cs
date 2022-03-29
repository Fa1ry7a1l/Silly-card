using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{

    [SerializeField] protected Game game;
    [SerializeField] protected Turn turn;
    [SerializeField] protected ManaBar mana;

    public bool TryPlay(Card card)
    {
        if(mana.CurrentMana >= card.Manacost)
        {
            mana.ReduceMana(card.Manacost);
            return true;
        }
        return false;   
    }
}
