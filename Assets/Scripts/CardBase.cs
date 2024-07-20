using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBase : MonoBehaviour
{

    public CardSO cardSO;
    public List<EffectBase> effects;


    public void PlayCard()
    {
        foreach (EffectBase effect in effects)
        {
           // do the effect
        }
    }
}
