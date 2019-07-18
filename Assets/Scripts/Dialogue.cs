using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour {

    [SerializeField] [TextArea(3, 11)] private string[] sentences;

    public void TriggerDialogue() {
        transform.parent.GetComponent<DialogueManager>().StartDialogue(sentences);
    }

    public void endTriggerDialogue() {
        transform.parent.GetComponent<DialogueManager>().EndDialogue();
    }

    void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag(Global.tagPlayer)) {

            TriggerDialogue();

        }

    }

    private void OnTriggerExit2D(Collider2D collision) {

        if (collision.gameObject.CompareTag(Global.tagPlayer)) {

            endTriggerDialogue();

        }

    }

}
