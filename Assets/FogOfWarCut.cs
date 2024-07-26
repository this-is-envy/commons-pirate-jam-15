using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWarCut : MonoBehaviour
{
    public FogOfWar fogOfWar;
    public Transform secondaryFogOfWar;
    [Range(0, 50)]
    public float sightDistance;
    public float checkInterval;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckFogOfWar(checkInterval));
        secondaryFogOfWar.localScale = new Vector2(sightDistance, sightDistance) * 10f;
    }

    private void Update() {
        // This is just for testing, needs to be removed later
        if (Input.GetKey(KeyCode.W))
            transform.position += transform.up   * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            transform.position -= transform.up  * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
            transform.position -= transform.right  * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            transform.position += transform.right  * Time.deltaTime;
    }

    private IEnumerator CheckFogOfWar(float checkInterval) {
        while (true) {
            fogOfWar.MakeHole(transform.position, sightDistance);
            yield return new WaitForSeconds(checkInterval);
        }
    }
}
