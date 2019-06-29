using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    [SerializeField] private bool electricallyCharge = false;

    private bool isOn = false;

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.CompareTag(Global.tagBox)) {

            switch(electricallyCharge) {

                case true:
                    if (electricallyCharge && !col.GetComponent<Box>().getIsElectricallyCharged()) {
                        isOn = true;
                    }
                    break;

                case false:
                    isOn = true;
                    break;

            }

        }

    }

    public bool getIsOn() {

        return isOn;

    }

}
