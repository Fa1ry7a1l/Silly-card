using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public abstract class CardModelBase
{
    public string Name;
    public Sprite Logo;
    public int Manacost;
    protected string logoPath;
    public string Description;
    public CardModelBase(string Name, string logoPath, int manaCost, string description = "")
    {
        this.Name = Name;
        Logo = Resources.Load<Sprite>(logoPath);
        this.Manacost = manaCost;
        this.logoPath = logoPath;
        Description = description;
    }

    public abstract CardModelBase Clone();

    public override  bool Equals(object obj)
    {
        if (obj is CardModelBase cmb)
        {
            return cmb.Name.Equals(this.Name);
        }
        return false;
    }

}

