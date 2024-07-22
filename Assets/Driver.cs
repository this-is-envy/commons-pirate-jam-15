using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEditor.Build.Content;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Driver : MonoBehaviour {
    private GameManager gameManager;
    private CardController cardController;

    private Scene debugScene;
    private DebugUI debugUI;

    public void Awake() {
        cardController = GetComponent<CardController>();
        gameManager = new GameManager(cardController);

        debugScene = SceneManager.GetSceneByName("DebugUI");
        debugUI = debugScene.GetRootComponent<DebugUI>();
        debugUI.gameManager = gameManager;
        Debug.Log("debugUI: " + debugUI);
    }
}
