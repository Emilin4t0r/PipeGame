using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    GameObject toggler;
    public enum ArrowType { Up, Down }
    public ArrowType arrowType;

    void Start() {

        toggler = transform.GetChild(0).gameObject;

    }

    private void OnMouseEnter() {
        if (!GameManager.Instance.grabbingSomething)
            toggler.SetActive(true);
    }
    private void OnMouseExit() {
        if (!GameManager.Instance.grabbingSomething)
        toggler.SetActive(false);
    }
    private void OnMouseDown() {
        if (arrowType == ArrowType.Up) {
            if (!CameraMover.Instance.movingDown)
                CameraMover.Instance.GoToRoom("up");
        }
        else if (arrowType == ArrowType.Down) {
            if (!CameraMover.Instance.movingUp)
                CameraMover.Instance.GoToRoom("down");
        }
    }
}
