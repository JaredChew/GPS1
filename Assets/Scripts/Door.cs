using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    [SerializeField] int doorIndex;

    [SerializeField] Button button;

    private BoxCollider2D doorCollider;
    private Animator doorAnimator;

    private void Start()
    {
        doorAnimator = this.transform.Find("door_").GetComponent<Animator>();
        doorCollider = GetComponent<BoxCollider2D>();

        doorCollider.isTrigger = Global.gameManager.getDoorStaus(doorIndex);
        //animation
        if (doorCollider.isTrigger)
        {
            doorAnimator.SetBool("doorOpened", true);
        }
    }

    void Update()
    {

        if (button.getIsOn() && !doorCollider.isTrigger)
        {
            //animation
            doorAnimator.SetBool("doorOpening", true);

            doorCollider.isTrigger = true;
            Global.gameManager.saveDoorStatus(doorIndex, true);
            button.resetButton();
        }


    }

}
