using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class CardController : MonoBehaviour {
    [SerializeField] private GameManager gameManager;
    private System.Random rnd;
    public int maxCards = 5;

    private HandMode handMode;

    public List<CardBase> DrawDeck;
    public List<CardBase> Hand;
    public List<CardBase> DiscardPile;
    public GameObject cardPrefab;

    public List<CardBase> cardStack;
    public CardBase currentCard {
        get {
            if (cardStack.Count == 0) {
                return null;
            }
            return cardStack.Last();
        }
    }
    public bool hasCardSelected {
        get {
            return cardStack.Count > 0;
        }
    }
    public int stackCost {
        get {
            return cardStack.Select(c => c.cardSO.cost).Sum();
        }
    }

    public void Awake() {
        cardStack = new List<CardBase>();
        handMode = HandMode.Display;
        rnd = new System.Random((int)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
        DrawDeck = new List<CardBase>();
        Hand = new List<CardBase>();
        DiscardPile = new List<CardBase>();
    }

    private void Update() {
        if (hasCardSelected) {
            if (handMode.IsPlayable() && Input.GetMouseButtonDown(0)) {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                PlayStack(mousePosition);
            }

            if (Input.GetKeyDown(KeyCode.Escape)) {
                // clear the working stack
                cardStack.Clear();
                handMode = HandMode.Display;
            }
        }
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

    public void PlayStack(Vector3 worldPosition) {
        gameManager.playerResourcePool -= stackCost;

        // have to mark the cards as discarded before resolving
        // because that unwinds the card stack
        foreach (var c in cardStack) {
            Hand.Remove(c);
            DiscardPile.Add(c);
            c.gameObject.SetActive(false);
        }

        var start = cardStack.First();
        cardStack.RemoveAt(0);
        start.PlayCard(worldPosition, cardStack);
        cardStack.Clear();
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

    private void OnCardEnter(CardBase card) { }

    private void OnCardExit(CardBase card) { }

    private void OnCardClick(CardBase card) {
        Debug.Log("Clicked: " + card.cardSO);
        if (!(gameManager.actor == TurnActor.Player && gameManager.curPhase == TurnPhase.Play)) {
            return;
        }

        var reqCost = stackCost + card.cardSO.cost;
        if (gameManager.playerResourcePool < reqCost) {
            Debug.Log($"Player doesn't have required resource pool to play card, needs {reqCost}");
            return;
        }


        //Lets do it with card hovering like in hearthstone
        cardStack.Add(card);
        handMode = card.cardSO.usageMode;
    }

}


