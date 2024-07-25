﻿using System;

using UnityEditor.UI;

public enum TurnActor {
    Player,
    Neutral,
    Opponent,
}

public enum TurnPhase {
    StartTurn,
    WorldTick,
    Draw,
    Play,
    Discard,
    EndTurn,
}