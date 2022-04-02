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
    public CardModelBase(string Name, string logoPath, int manaCost)
    {
        this.Name = Name;
        Logo = Resources.Load<Sprite>(logoPath);
        this.Manacost = manaCost;
        this.logoPath = logoPath;

    }

    public abstract CardModelBase Clone();

}

