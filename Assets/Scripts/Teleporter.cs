using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    [SerializeField] private Transform teleportDestination;

    private Player player;

    private bool isInteractable;

    //animation
    private Animator teleportAnim;
    private bool isTeleporting;



    private void Awake()
    {

        //animation    
        teleportAnim = this.transform.GetComponent<Animator>();


        isInteractable = false;

    }

    private void Update()
    {

        if (isInteractable)
        {
            teleportToDestination();
            //animation
            teleportAnim.SetTrigger("teleportCloseTrigger");

        }

    }

    public void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag(Global.tagPlayer))
        {

            player = collision.GetComponent<Player>();

            isInteractable = true;


        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {

        player = null;

        isInteractable = false;
        teleportAnim.ResetTrigger("teleportCloseTrigger");
    }

    private void teleportToDestination()
    {

        if (Input.GetButtonDown(Global.controlsInteract))
        {

            // animation
            teleportAnim.SetBool("teleportActivate", true);


            player.GetComponent<Player>().teleportAnim(isTeleporting);
            //player.setPlayerPosition(teleportDestination.position); //teleport to position
            Global.audiomanager.getSFX("princess_walking").stop();
        }

    }


    private void animationEnded()
    {

        teleportAnim.SetBool("teleportActivate", false);
        player.setPlayerPosition(teleportDestination.position);
        player.GetComponent<Player>().teleportAnim(!isTeleporting);

    }

}
