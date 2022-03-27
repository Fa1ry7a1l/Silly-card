using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar:MonoBehaviour
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
    [SerializeField] private List<Image> _gems = new List<Image>();
    private void Start()
    {
        CurrentMana = 0;
        _manaPullSize = 0;
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
        show();
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
        show();
    }

}
