using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class DebugUI : MonoBehaviour {
    public GameManager gameManager;

    public void StartGame() {
        gameManager.StartGame(TurnActor.Player);
    }

    public void Advance() {
        gameManager.AdvanceTurn();
    }
}
