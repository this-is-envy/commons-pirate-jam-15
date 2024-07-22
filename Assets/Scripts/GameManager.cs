using System;

using UnityEngine;

public class GameManager {
    public TurnActor actor { get; private set; }
    public TurnPhase curPhase { get; private set; }

    public bool gameStarted { get; private set; } = false;

    private CardController cardController;

    public GameManager(CardController cc) {
        Debug.Log("GameManager(" + cc + ")");
        cardController = cc;
        gameStarted = false;
    }

    public void StartGame(TurnActor player1) {

        gameStarted = true;
        actor = player1;
        curPhase = TurnPhase.StartTurn;

        // yes, we'll eventually want this in the editor but for now I'm going
        // to just stub it out here to get things moving along.
        Card[] initialDeck = new Card[]{
            Card.Melee1,
            Card.Melee1,
            Card.Scout1,
            Card.Range1,
            Card.Range2,
            Card.Fireball,
            Card.SummonFog,
            Card.GustOfWind,
            Card.AreaOfEffect,
        };
        foreach (var c in initialDeck) {
            cardController.DiscardPile.Add(
                cardController.HydrateCard(CardSO.GetCard(c)));
        }
    }

    public void AdvanceTurn() {
        if (!gameStarted) {
            throw new Exception("Attempting to advance game that has not started.");
        }

        var next = curPhase.Next();
        Debug.Log($"{curPhase} -> {next}");
        curPhase = next;

        if (curPhase == TurnPhase.StartTurn) {
            var nextActor = actor.Next();
            Debug.Log($"{actor} -> {nextActor}");
            actor = nextActor;
        }

        if (actor == TurnActor.Player) {
            if (curPhase == TurnPhase.Draw) {
                cardController.DrawCards(5);
                Debug.Log("Hand:");
                foreach (var c in cardController.Hand) {
                    Debug.Log("  " + c);
                }
            }

            if (curPhase == TurnPhase.Discard) {
                cardController.DiscardAll();
            }
        }
    }
}