using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    [SerializeField] private Global.BoxAbilities type;

    bool isInteractable;

    private void Awake() {

        isInteractable = false;

    }

    private void Update() {
        
        if(isInteractable) {
            itemPickUp();
        }

    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.name.Equals("Player")) {
            isInteractable = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision) {

        if (collision.gameObject.name.Equals("Player")) {
            isInteractable = false;
        }

    }

    void itemPickUp() {

        if(Input.GetButtonDown(Global.controlsInteract)) {
            //Global.powerUpBox(type);
            Destroy(gameObject);
        }

    }

}
