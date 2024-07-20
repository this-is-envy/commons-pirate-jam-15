using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour {
    public int maxCards = 10;

    public List<CardBase> DrawDeck;
    public List<CardBase> Hand;
    public List<CardBase> DiscardPile;


    public void DrawCards(int noCards) {
        for (int i = 0; i < noCards; i++) {
            CardBase card = DrawDeck[0];
            DrawDeck.RemoveAt(0);
            if(Hand.Count >= maxCards) {            
              DiscardPile.Add(card);
            }
            else
            {
                Hand.Add(card);
            }

        }
    }

    public void UseCard(CardBase card) {
       card.PlayCard();
        Hand.Remove(card);
        DiscardPile.Add(card);
    }



}


