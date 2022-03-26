using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar
{
    public const int MaxMana = 10;
    private int _curMana;
    private int _manaPullSize;
    public int CurrentMana
    {
        get { return _curMana; }
        private set
        {
            if (value > MaxMana)
                _curMana = MaxMana;
            else
            {
                if (value < 0)
                    _curMana = 0;
                else
                    _curMana = value;
            }
        }
    }
    private Transform Transform;
    List<Image> _gems;
    public ManaBar(Transform t)
    {
        Transform = t;
        CurrentMana = 0;
        _manaPullSize = 0;
        _gems = new List<Image>();
        for (int i = 0; i < Transform.childCount; i++)
        {
            _gems.Add(Transform.GetChild(i).GetComponent<Image>());
        }
    }

    public int AddMana(int addValue)
    {
        CurrentMana += addValue;
        this.show();
        return CurrentMana;
    }

    public int ReduceMana(int reduceValue)
    {
        CurrentMana -= reduceValue;
        this.show();
        return CurrentMana;
    }

    public void show()
    {
        int counter = 0;
        for (; counter < CurrentMana; counter++)
        {
            _gems[counter].sprite = Resources.Load<Sprite>("Mana and HP/full");
        }
        for (; counter < _gems.Count; counter++)
        {
            _gems[counter].sprite = Resources.Load<Sprite>("Mana and HP/used");
        }
    }

    public void FillManaBar()
    {
        _manaPullSize += 1;
        CurrentMana = _manaPullSize;
        Debug.Log($"{_manaPullSize} {CurrentMana}");
        this.show();
    }

}
