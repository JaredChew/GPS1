using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    [SerializeField] private Transform teleportDestination;

    private Player player;

    private bool isInteractable;

    private void Awake() {

        isInteractable = false;

    }

    private void Update() {

        if (isInteractable) {
            teleportToDestination();
        }

    }

    public void OnTriggerStay2D(Collider2D collision) {

        if (collision.gameObject.CompareTag(Global.tagPlayer)) {

            player = collision.GetComponent<Player>();

            isInteractable = true;

        }

    }

    public void OnTriggerExit2D(Collider2D collision) {

        player = null;

        isInteractable = false;

    }

    private void teleportToDestination() {

        if (Input.GetButtonDown(Global.controlsInteract)) {
            player.setPlayerPosition(teleportDestination.position);
        }

    }

}
