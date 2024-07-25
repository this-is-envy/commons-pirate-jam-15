using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CardController : MonoBehaviour {
    [SerializeField] private GameManager gameManager;
    private System.Random rnd;
    public int maxCards = 5;

    public List<CardBase> DrawDeck;
    public List<CardBase> Hand;
    public List<CardBase> DiscardPile;
    public GameObject cardPrefab;

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
                if (DiscardPile.Count == 0) {
                    // TODO: how do we want to handle this?
                    return;
                }
                Shuffle();
            }

            CardBase card = DrawDeck[0];
            DrawDeck.RemoveAt(0);
            Debug.Log("drew: " + card);

            if (Hand.Count >= maxCards) {
                card.gameObject.SetActive(false);
                DiscardPile.Add(card);
            } else {
                card.gameObject.SetActive(true);
                Hand.Add(card);
            }
        }
    }

    public void UseCard(CardBase card) {
        card.PlayCard();
        Hand.Remove(card);
        card.gameObject.SetActive(false);
        DiscardPile.Add(card);
    }

    public void DiscardAll() {
        foreach (var c in Hand) {
            DiscardPile.Add(c);
            c.gameObject.SetActive(false);
        }
        Hand.Clear();
    }

    public CardBase HydrateCard(CardSO so) {
        var card = Instantiate(cardPrefab, transform);
        // cards start off deactivated until they need to be displayed
        card.SetActive(false);
        card.transform.parent = transform;
        var cardBase = card.AddComponent<CardBase>();
        cardBase.cardSO = so;
        var cardUI = card.GetComponent<CardUI>();
        cardUI.titleTMP.text = so.name;
        cardUI.descriptionTMP.text = so.description;
        cardUI.costTMP.text = $"{so.cost}";
        cardUI.cardArt.sprite = so.sprite;

        cardUI.MouseEntered.AddListener(() => OnCardEnter(cardBase));
        cardUI.MouseExited.AddListener(() => OnCardExit(cardBase));
        cardUI.OnClick.AddListener(() => OnCardClick(cardBase));
        return cardBase;
    }

    private void OnCardEnter(CardBase card) {
        Debug.Log("MouseOver: " + card.cardSO);
    }
    private void OnCardExit(CardBase card) {
        Debug.Log("MouseExit: " + card.cardSO);
    }

    private void OnCardClick(CardBase card) {
        Debug.Log("Clicked: " + card.cardSO);
        if (!(gameManager.actor == TurnActor.Player && gameManager.curPhase == TurnPhase.Play)) {
            return;
        }

        if (gameManager.playerResourcePool <= card.cardSO.cost) {
            Debug.Log("Palyer doesn't have required resource pool to play card");
            return;
        }

        // TODO: modifier cards will be a multi-step play process
        gameManager.playerResourcePool -= card.cardSO.cost;
        // TODO: play card
    }
}


