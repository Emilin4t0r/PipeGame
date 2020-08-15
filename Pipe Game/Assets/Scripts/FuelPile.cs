using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelPile : MonoBehaviour {

    public GameObject coal;
    GameObject currentFuelPiece;

    public int fuelAmt;

    void Start() {
    }

    void FixedUpdate() {
        if (currentFuelPiece != null) {
            Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentFuelPiece.transform.position = mouseScreenPosition;
            float x = currentFuelPiece.transform.position.x;
            float y = currentFuelPiece.transform.position.y;
            currentFuelPiece.transform.position = new Vector3(x, y, 0);
        }
    }
    
    private void Update() {
        //Dropping fuel        
        if (Input.GetKeyUp(KeyCode.Mouse0) && GameManager.Instance.grabbingSomething) {                                    
            if (Stove.Instance.fuelHoveringOnStove) {
                Stove.Instance.AddFuel(50);
            }
            else {
                fuelAmt += 1;
            }
            Destroy(currentFuelPiece.gameObject);
            currentFuelPiece = null;
            GameManager.Instance.grabbingSomething = false;
        }
    }

    private void OnMouseDown() {
        if (!GameManager.Instance.grabbingSomething) {
            currentFuelPiece = Instantiate(coal, transform.position, transform.rotation);
            GameManager.Instance.grabbingSomething = true;
            fuelAmt -= 1;
        }
    }
}
