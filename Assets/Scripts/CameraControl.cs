using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CameraControl : MonoBehaviour {
    public Func<Vector3> GetDirection;
    [SerializeField] public Camera camera;
    [SerializeField] public float MoveSpeed = 1f;
    [SerializeField] public float BorderWidthPct = .05f;
    [SerializeField] SpriteRenderer ClampingObject;

    // Start is called before the first frame update
    void Start() {
        if (GetDirection == null) {
            GetDirection = DefaultGetDirection;
        }
    }

    public void ResetGetDirection() {
        GetDirection = DefaultGetDirection;
    }

    private Vector3 DefaultGetDirection() {
        var screenWidth = Screen.width;
        var horizEdge = screenWidth * BorderWidthPct;
        var screenHeight = Screen.height;
        var vertEdge = screenHeight * BorderWidthPct;

        var mousePos = Input.mousePosition;

        var dir = new Vector3(0, 0);

        if (mousePos.x < horizEdge) {
            dir.x = -1;
        }
        if (mousePos.x > screenWidth - horizEdge) {
            dir.x = 1;
        }
        if (mousePos.y < vertEdge) {
            dir.y = -1;
        }
        if (mousePos.y > screenHeight - vertEdge) {
            dir.y = 1;
        }

        

        return dir.normalized;
    }

    // Update is called once per frame
    void Update() {
        var direction = GetDirection();
        if (direction == Vector3.zero) {
            return;
        }

        var newPos = camera.transform.position + direction * MoveSpeed * Time.deltaTime;
        // have to set the z value to our bounding box for this check or it'll always fail
        newPos.z = ClampingObject.transform.position.z;
        if (ClampingObject.bounds.Contains(newPos)) {
            newPos.z = camera.transform.position.z;
            camera.transform.position = newPos;
        } else {
            Debug.Log(ClampingObject.bounds + " does not contain " + newPos);
        }
    }
}
