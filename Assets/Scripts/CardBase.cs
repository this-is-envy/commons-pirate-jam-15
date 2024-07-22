using System.Collections.Generic;

using UnityEngine;

public class CardBase : MonoBehaviour {

    public CardSO cardSO;
    public List<EffectBase> effects;

    public void Awake() {
        effects = new List<EffectBase>();
    }

    public void PlayCard() {
        foreach (EffectBase effect in effects) {
            // do the effect
        }
    }

    public override string ToString() {
        return $"{cardSO.name} / {cardSO.cost} with {effects?.Count} effects";
    }
}
