using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pump : MonoBehaviour {

    bool pumping;
    public bool isDown;
    public bool isUp;

    bool hasSetUp;
    bool hasSetDown;

    private void Start() {
        isUp = true;
    }

    private void FixedUpdate() {

        if (transform.position.y >= 0) {
            isUp = true;
            isDown = false;
            hasSetDown = false;
        }

        if (transform.position.y <= -2) {
            isDown = true;
            isUp = false;
            hasSetUp = false;
        }


        if (pumping) {
            Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (isUp) {
                if(!hasSetUp)
                    SetUp();
                if (mouseScreenPosition.y < transform.position.y) {
                    transform.position = new Vector3(0, mouseScreenPosition.y, 0);
                }
            }
            if (isDown) {
                if(!hasSetDown)
                    SetDown();
                if (mouseScreenPosition.y > transform.position.y) {
                    transform.position = new Vector3(0, mouseScreenPosition.y, 0);
                }
            }            
        }
    }

    private void OnMouseDown() {
        if (!GameManager.Instance.grabbingSomething) {
            GameManager.Instance.grabbingSomething = true;
            pumping = true;
        }
    }
    private void OnMouseUp() {
        if (isUp) {
            SetUp();
        } else if (isDown) {
            SetDown();
        }
        if (GameManager.Instance.grabbingSomething) {
            GameManager.Instance.grabbingSomething = false;
            pumping = false;
        }
    }

    void SetDown() {
        transform.position = new Vector3(0, -2, 0);
        hasSetDown = true;
    }
    void SetUp() {
        transform.position = new Vector3(0, 0, 0);
        hasSetUp = true;
    }
}
