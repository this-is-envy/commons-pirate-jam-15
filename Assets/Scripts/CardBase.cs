using System.Collections.Generic;

using UnityEngine;

public class CardBase : MonoBehaviour {

    public CardSO cardSO;
    public List<EffectBase> effects;

    public void Awake() {
        effects = new List<EffectBase>();
    }

    public void PlayCard(Vector3 position, List<CardBase> remainingStack) {
        Debug.Log("  - " + cardSO.name);
        effects = cardSO.effects;
        foreach (EffectBase effect in effects) {
            effect.ActivateEffect(this, position);
        }

        if (remainingStack.Count == 0) {
            return;
        }

        var next = remainingStack[0];
        remainingStack.RemoveAt(0);
        next.PlayCard(position, remainingStack);
    }

    public override string ToString() {
        return $"{cardSO.name} / {cardSO.cost}";
    }
}
