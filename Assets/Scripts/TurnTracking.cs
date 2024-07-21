using System;

using UnityEditor.UI;

public enum TurnActor {
    Player,
    Neutral,
    Opponent,
}

public enum TurnPhase {
    StartTurn,
    ResolveMovement,
    Draw,
    Play,
    Discard,
    EndTurn,
}