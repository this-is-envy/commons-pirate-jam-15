using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class DebugUI : MonoBehaviour {
    public GameManager gameManager;
    [SerializeField] public TMP_Text playerTMP;
    [SerializeField] public TMP_Text phaseTMP;

    public void Start() {
        // TODO: We need to rehome this out of the DebugUI
        gameManager = new GameManager(FindObjectOfType<CardController>());
    }

    public void StartGame() {
        gameManager.StartGame(TurnActor.Player);
        playerTMP.text = $"Turn: {gameManager.actor}";
        phaseTMP.text = $"Phase: {gameManager.curPhase}";
    }

    public void Advance() {
        gameManager.AdvanceTurn();
        playerTMP.text = $"Turn: {gameManager.actor}";
        phaseTMP.text = $"Phase: {gameManager.curPhase}";
    }
}
