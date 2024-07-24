using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEditor;

using UnityEngine;

public class DebugUI : MonoBehaviour {
    public GameManager gameManager;
    [SerializeField] public TMP_Text playerTMP;
    [SerializeField] public TMP_Text phaseTMP;

    public void Start() {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void StartGame() {
        gameManager.StartGame(TurnActor.Player);
    }

    public void Advance() {
        gameManager.AdvanceTurn();
    }

    public void Update() {
        playerTMP.text = $"Turn: {gameManager?.actor}";
        phaseTMP.text = $"Phase: {gameManager?.curPhase}";
    }
}
