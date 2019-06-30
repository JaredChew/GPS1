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

            switch (electricallyCharge) {

                case true:
                    if (col.GetComponent<Box>().getIsElectricallyCharged()) {
                        isOn = true;
                        Debug.Log("Pressed");
                    }
                    break;

                case false:
                    isOn = true;
                    Debug.Log("Pressed");
                    break;

            }

        }

    }

    public void resetButton() {
        isOn = false;
    }

    public bool getIsOn() {

        return isOn;

    }

}
