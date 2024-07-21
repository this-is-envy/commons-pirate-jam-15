using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "ScriptableObjects/Card")]
public class CardSO : ScriptableObject {
    public string cardName;
    [TextArea(15, 20)]
    public string description;
    public int cost;
    public Sprite sprite;
    //todo: Add effects
    public EffectType effectType;

    public override string ToString() {
        return $"{cardName} ({cost}): {description}";
    }

    public static CardSO GetCard(Card card) {
        var cardSO = AssetDatabase.LoadAssetAtPath<CardSO>(card.AssetPath());
        if (card == null ) {
            Debug.Log("Requested unknown card: " + card);
        }
        return cardSO;
    }
}

public enum Card {
    Fireball,
    GustOfWind,
    RockSlide,
    SummonFog,
    AreaOfEffect,
    BlazeOfGlory,
    Melee1,
    Melee2,
    Melee3,
    Range1,
    Range2,
    Range3,
    Scout1,
    Scout2,
    Scout3,
}