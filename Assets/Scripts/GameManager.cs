using System;
using System.Collections;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour {
    [SerializeField] public CardController cardController;

    public int playerResourcePool = 0;

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

        switch (actor) {
            case TurnActor.Player:
                return PlayerTurn(curPhase);
            case TurnActor.Neutral:
                break;
            case TurnActor.Opponent:
                break;
        }

        return true;
    }

    private bool PlayerTurn(TurnPhase phase) {
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

        if (curPhase == TurnPhase.WorldTick) {
            // TODO: this is *only* some dumb first pass shit to start
            // sketching things out
            // at the beginning of the player's turn their resource pool is at zero
            playerResourcePool = 0;
            // grab all the placeables
            var objs = GameObject.FindGameObjectsWithTag("Placeables");
            foreach (var obj in objs) {
                // modifiy the player's resource pool by whatever the placeable tells us
                playerResourcePool += obj.GetComponent<Placeable>().WorldTick(TurnActor.Player);
            }
        }

        // nothing else to do, let the game auto-advance in some cases
        switch (phase) {
            case TurnPhase.Play:
                return false;
            default:
                return true;
        }

    }
}