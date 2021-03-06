using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public const int MaxHP = 30;
    [SerializeField] private int _curHP = 4;
    public int CurrentHP
    {
        get { return _curHP; }
        private set
        {
            if (value > MaxHP)
                _curHP = MaxHP;
            else
                    if (value < 0)
                _curHP = 0;
            else
                _curHP = value;
        }
    }
    [SerializeField] private List<Image> _gems = new List<Image>();


    public int AddHP(int addValue)
    {
        CurrentHP += addValue;
        this.Show();
        return CurrentHP;
    }

    public int ReduceHP(int reduceValue)
    {
        CurrentHP -= reduceValue;
        this.Show();
        return CurrentHP;
    }

    public void Show()
    {
        int counter = 0;
        for (; counter < CurrentHP / 3; counter++)
        {
            _gems[counter].sprite = Resources.Load<Sprite>("Mana and HP/FullHp");
        }
        if (CurrentHP % 3 != 0)
        {
            _gems[counter++].sprite = CurrentHP % 3 == 2 ? Resources.Load<Sprite>("Mana and HP/1lvl") : Resources.Load<Sprite>("Mana and HP/2lvl");
        }
        for (; counter < _gems.Count; counter++)
        {
            _gems[counter].sprite = Resources.Load<Sprite>("Mana and HP/0");
        }
    }

}
