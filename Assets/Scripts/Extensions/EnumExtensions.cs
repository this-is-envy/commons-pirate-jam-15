using System;

using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;

using UnityEngine;

static class EnumExtensions {

    public static TurnPhase Next(this TurnPhase self) {
        switch (self) {
            case TurnPhase.StartTurn: return TurnPhase.ResolveMovement;
            case TurnPhase.ResolveMovement: return TurnPhase.Draw;
            case TurnPhase.Draw: return TurnPhase.Play;
            case TurnPhase.Play: return TurnPhase.Discard;
            case TurnPhase.Discard: return TurnPhase.EndTurn;
            case TurnPhase.EndTurn: return TurnPhase.StartTurn;
            default: throw new ArgumentOutOfRangeException("Unhandled turn phase: " + self);
        }
    }

    public static TurnActor Next(this TurnActor self) {
        switch (self) {
            case TurnActor.Player: return TurnActor.Neutral;
            case TurnActor.Neutral: return TurnActor.Opponent;
            case TurnActor.Opponent: return TurnActor.Player;
            default: throw new ArgumentOutOfRangeException("Unhandled turn actor: " + self);
        }
    }

    private static string effectAssetPath(string name) {
        return $"Assets/ScriptableObjects/Cards/Effects/{name}.asset";
    }

    private static string modifierAssetPath(string name) {
        return $"Assets/ScriptableObjects/Cards/Modifiers/{name}.asset";
    }

    private static string summonsAssetPath(string name) {
        return $"Assets/ScriptableObjects/Cards/Summons/{name}.asset";
    }

    public static string AssetPath(this Card card) {
        switch (card) {
            case Card.Fireball: return effectAssetPath("Fireball");
            case Card.GustOfWind: return effectAssetPath("Gust of Wind");
            case Card.RockSlide: return effectAssetPath("Rock Slide");
            case Card.SummonFog: return effectAssetPath("Summon Fog");
            case Card.AreaOfEffect: return modifierAssetPath("Area of Effect");
            case Card.BlazeOfGlory: return modifierAssetPath("Blaze of Glory");
            case Card.Melee1: return summonsAssetPath("Melee 1");
            case Card.Melee2: return summonsAssetPath("Melee 2");
            case Card.Melee3: return summonsAssetPath("Melee 3");
            case Card.Range1: return summonsAssetPath("Range 1");
            case Card.Range2: return summonsAssetPath("Range 2");
            case Card.Range3: return summonsAssetPath("Range 3");
            case Card.Scout1: return summonsAssetPath("Scout 1");
            case Card.Scout2: return summonsAssetPath("Scout 2");
            case Card.Scout3: return summonsAssetPath("Scout 3");
            default:
                throw new ArgumentOutOfRangeException("Unknown card type requested: " + card);
        }
    }

    public static CardBase AsCardBase(this Card self, CardController parent) {
        Debug.Log("convert " + self + " to card base");
        // TODO: I initially was going to treat this as pure data and new CardBase
        // but because new fails I need to construct them via AddComponent. If it's
        // A component it needs to exist hanging off some GameObject. Presumably
        // this will be something that manages hand rendering or something idk.
        // for now I'm just attaching them to the card controller but generally
        // this probably won't be the correct path forward. Hold off on
        // structure input from Snechar.


        //there is no need to attach the script to the parent, this needs to be attached to the card object, and should be created using and Instantiate method
        //which requires MonoBehavior, so we do it in CardController instead

        var cb = parent.AddComponent<CardBase>();
        var so = CardSO.GetCard(self);
        Debug.Log("got so: " + so);
        cb.cardSO = so;
        Debug.Log("constructed " + cb);

        return cb;
    }
}