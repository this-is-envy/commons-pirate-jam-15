using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

public class BasicSummon : EffectBase {
    public override void ActivateEffect(CardBase context, Vector3 worldPos) {
        var summonPosition = worldPos;
        summonPosition.z = 0;

        var data = context.cardSO.summonData;

        var prefab = data.unitPrefab;
        var summoned = SummonEntity(prefab, summonPosition);
        var unit = summoned.GetComponent<Unit>();
        unit.InitFromSO(data);

        var renderer = summoned.GetComponent<SpriteRenderer>();
        renderer.sprite = data.token;
        unit.transform.localScale = new Vector3(data.tokenScale, data.tokenScale, 1);
    }
}
