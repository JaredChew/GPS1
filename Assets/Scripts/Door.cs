using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    [SerializeField] Button button;

    private BoxCollider2D doorCollider;

    void Update() {

        if (button.GetComponent<Button>().getIsOn()) {
            //transform.Translate(Vector3.up * Time.deltaTime);
            doorCollider.isTrigger = true;
        }

    }

}
