using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public  class Card : MonoBehaviour
{

    public enum CardOwner {Player,Enemy };
    public CardOwner Owner;
    public CardShowSrc CardShow;
    public CardModelBase CardModel;
    [HideInInspector] public Card Clone;

    [HideInInspector] public PlayerBase PlayerBase;
    [SerializeField] private Image _raycaster;

    public Image RayCaster => _raycaster;

    public void Init(CardModelBase cm,CardOwner owner)
    {
        CardModel = cm.Clone();
        CardShow.ShowCardInfo(cm);
        Owner = owner;
    }


    public bool TryPlay()
    {
        return PlayerBase.TryPlay(CardModel);
    }

    public override bool Equals(object other)
    {
        if (other is Card card)
            return this.CardModel.Equals(card.CardModel);
        return false;
    }

    public void UpdateVisualData()
    {
        if (CardModel is UnitCard card)
        {
            CardShow.RefreshData(card);
            Clone?.ChangeUnitCard(card.Clone());
            Clone?.UpdateVisualData();
        }
    }

    private void ChangeUnitCard(CardModelBase cmb)
    {
        CardModel = cmb;
        
    }


}
