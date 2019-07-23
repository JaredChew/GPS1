using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTransition : MonoBehaviour {

    [SerializeField] private Global.Areas areaConnect1;
    [SerializeField] private Global.Areas areaConnect2;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.CompareTag(Global.tagPlayer)) {

            if (Global.gameManager.getCurrentArea() == areaConnect1) {
                Global.gameManager.transitionToNewArea(areaConnect2);
            }
            else if (Global.gameManager.getCurrentArea() == areaConnect2) {
                Global.gameManager.transitionToNewArea(areaConnect1);
            }

        }

    }

}
