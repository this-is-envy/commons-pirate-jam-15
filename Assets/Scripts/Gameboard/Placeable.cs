using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    public PlaceableSO details;
    public TurnActor owner = TurnActor.Neutral;

    public int WorldTick(TurnActor forActor) {
        if (owner == forActor) {
            return details.value;
        }
        return 0;
    }
}
