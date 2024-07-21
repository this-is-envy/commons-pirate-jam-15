using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CardController : MonoBehaviour {
    public int maxCards = 5;

    public List<CardBase> DrawDeck;
    public List<CardBase> Hand;
    public List<CardBase> DiscardPile;
    private System.Random rnd;

    public void Awake() {
        rnd = new System.Random();
        DrawDeck = new List<CardBase>();
        Hand = new List<CardBase>();
        DiscardPile = new List<CardBase>();
    }

    public void Shuffle() {
        while (DiscardPile.Count > 0) {
            var idx = rnd.Next(DiscardPile.Count);
            DrawDeck.Add(DiscardPile[idx]);
            DiscardPile.RemoveAt(idx);
        }
    }

    public void DrawCards(int noCards) {
        for (int i = 0; i < noCards; i++) {
            if (DrawDeck.Count == 0) {
                Shuffle();
            }
            CardBase card = DrawDeck[0];
            DrawDeck.RemoveAt(0);
            Debug.Log("drew: " + card);

            if (Hand.Count >= maxCards) {
                DiscardPile.Add(card);
            } else {
                Hand.Add(card);
            }
        }
    }

    public void UseCard(CardBase card) {
        card.PlayCard();
        Hand.Remove(card);
        DiscardPile.Add(card);
    }

    public void DiscardAll() {
        foreach (var c in Hand) {
            DiscardPile.Add(c);
        }
        Hand.Clear();
    }

}


