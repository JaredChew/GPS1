using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour {
    /*
    [SerializeField] private Global.SwitchType switchType;

    private bool manipulatable = false;

    // Update is called once per frame
    private void Update() {

        if(manipulatable) { switchOnOff(); }

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == Global.tagPlayer) {
            manipulatable = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision) {

        if (collision.gameObject.tag == Global.tagPlayer) {
            manipulatable = false;
        }

    }

    private void switchOnOff() {

        if(Input.GetButtonDown(Global.controlsInteract)) {

            switch (switchType) {

                case Global.SwitchType.Lights:
                    Global.gameMngr.setLights(!Global.gameMngr.getIsNight());
                    break;

            }

        }

    }
    */
}
