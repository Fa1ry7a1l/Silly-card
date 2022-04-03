using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackedCard : DropPlaceBase, ITarget
{
    [SerializeField] Card card;

    //сюда могут попадать юниты или заклинания
    public override void MyOnDrop(Card cardBase)
    {

        if (cardBase.CardModel is UnitCard uc && uc.CanAttack /*&&
                transform.parent.GetComponent<DropPlaceScrypt>().Type == FieldType.ENEMY_FIELD*/)
        {
            uc.ChangeAttackState(false);
            FindObjectOfType<GameManagerSrc>().CardsFidht(cardBase, GetComponent<Card>());
        }
        else if (cardBase.CardModel is SingleTargetSpellCard stsc)
        {
            stsc.Spell(this);
            FindObjectOfType<GameManagerSrc>().DestroyCard(cardBase);
        }
    }

    public override void MyOnDropEnemy(Card cardBase)
    {
        if (cardBase.CardModel is UnitCard uc && uc.CanAttack /*&&
                transform.parent.GetComponent<DropPlaceScrypt>().Type == FieldType.ENEMY_FIELD*/)
        {
            uc.ChangeAttackState(false);
            FindObjectOfType<GameManagerSrc>().CardsFidht(cardBase, GetComponent<Card>());
        }
        else if (cardBase.CardModel is SingleTargetSpellCard stsc)
        {
            stsc.Spell(this);
            FindObjectOfType<GameManagerSrc>().DestroyCard(cardBase);
        }
    }

    public void TakeDamage(int damage)
    {
        UnitCard uc = (card.CardModel as UnitCard);
        uc.GetDamage(damage);

        FindObjectOfType<GameManagerSrc>().DestroyIfDead(card);
    }

    public void TakeHeal(int heal)
    {
        UnitCard uc = (card.CardModel as UnitCard);
        uc.GetHeal(heal);
    }
}
