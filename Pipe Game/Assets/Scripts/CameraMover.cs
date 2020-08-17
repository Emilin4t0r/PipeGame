using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {
    public static CameraMover Instance;

    public Vector3 boilerPos;
    public Vector3 spaPos;

    public bool movingUp;
    public bool movingDown;

    public float moveSpeed;

    private void Awake() {
        Instance = this;
    }

    private void FixedUpdate() {

        if (movingUp) {
            moveSpeed = Vector3.Distance(transform.position, spaPos) / 10;
            if (transform.position.y < 10) {
                transform.position = new Vector3(0, transform.position.y + moveSpeed, -10);
            }
            else {
                movingUp = false;
                transform.position = spaPos;
            }
        }

        if (movingDown) {
            moveSpeed = Vector3.Distance(transform.position, boilerPos) / 10;
            if (transform.position.y > 0.08f) {
                transform.position = new Vector3(0, transform.position.y - moveSpeed, -10);
            }
            else {
                movingDown = false;
                transform.position = boilerPos;
            }
        }        
    }

    public void GoToRoom(string direction) {

        if (direction == "up") {
            movingUp = true;
        }
        if (direction == "down") {
            movingDown = true;
        }
    }
}
