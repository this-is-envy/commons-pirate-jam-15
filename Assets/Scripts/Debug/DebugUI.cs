using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class DebugUI : MonoBehaviour {
    public GameManager gameManager;

    public void Awake() {
        gameManager = new GameManager(FindFirstObjectByType<CardController>());
    }

    public void StartGame() {
        gameManager.StartGame(TurnActor.Player);
    }

    public void Advance() {
        gameManager.AdvanceTurn();
    }
}
