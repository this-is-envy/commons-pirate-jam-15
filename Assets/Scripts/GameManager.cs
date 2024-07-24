using System;
using System.Collections;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour {
    [SerializeField] public CardController cardController;

    UnityEvent gameStart;
    UnityEvent gameOver;

    public TurnActor actor { get; private set; }
    public TurnPhase curPhase { get; private set; }

    public bool gameStarted { get; private set; } = false;

    public void Start() {
        gameStarted = false;
    }

    public void StartGame(TurnActor player1) {
        if (gameStarted) {
            return;
        }

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

        gameStart?.Invoke();
    }

    public void AdvanceTurn() {
        if (!gameStarted) {
            throw new Exception("Attempting to advance game that has not started.");
        }

        var next = curPhase.Next();
        curPhase = next;

        if (curPhase == TurnPhase.StartTurn) {
            var nextActor = actor.Next();
            actor = nextActor;
        }
        if (HandlePhase()) {
            Invoke("AdvanceTurn", .5f);
        }
    }

    public bool HandlePhase() {
        Debug.Log($"Processing {actor}:{curPhase}");

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

        return actor != TurnActor.Player;
    }
}