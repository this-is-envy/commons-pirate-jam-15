using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSummon : EffectBase
{
    public GameObject summonPrefab;
    private Vector3 summonPosition;

    public void SetTransformPosition(Vector3 position)
    {
        summonPosition = position;
        summonPosition.z = 0;
    }

    public override void ActivateEffect()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        SetTransformPosition(mousePosition);
        SummonEntity(summonPrefab, summonPosition);
    }
}
