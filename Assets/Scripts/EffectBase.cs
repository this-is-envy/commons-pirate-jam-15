using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// Make class extensions of this class to create effects
public class EffectBase : MonoBehaviour
{

    public void DealDamage(float damage, float area, Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, area);

    }
    public void SummonEntity(GameObject entity, Vector3 position)
    {
        Instantiate(entity, position, Quaternion.identity);
    }
    public void CreateTerrain(GameObject terrain,float area,  Vector3 position)
    {
        Instantiate(terrain, position, Quaternion.identity);
    }

   

}
