using System.Collections;
using System.Collections.Generic;

using UnityEngine;



// Make class extensions of this class to create effects
public class EffectBase : MonoBehaviour
{

    virtual public void ActivateEffect(CardBase context, Vector3 worldPos)
    {

    }

    virtual public void DealDamage(float damage, float area, Vector3 position)
    {
        position.z = 0; // Set the Z coordinate to 0
        Collider[] hitColliders = Physics.OverlapSphere(position, area);
    }

    virtual public GameObject SummonEntity(GameObject entity, Vector3 position)
    {
        position.z = 0; // Set the Z coordinate to 0
        return Object.Instantiate(entity, position, Quaternion.identity);
    }

    virtual public GameObject CreateTerrain(GameObject terrain, float area, Vector3 position)
    {
        position.z = 0; // Set the Z coordinate to 0
        return Object.Instantiate(terrain, position, Quaternion.identity);
    }
}
