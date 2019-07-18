using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    [SerializeField] private Canvas dialogueCanvas;

    [SerializeField] private Animator dialogueBoxAnimator;

    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start() {
        sentences = new Queue<string>();
    }

    public void StartDialogue(string[] dialogue) {
        dialogueCanvas.gameObject.SetActive(true);

        dialogueBoxAnimator.SetBool("isOn", true);

        //Start dialogue

        //nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueCanvas.GetComponentInChildren<Text>().text = sentence;
    }

    public void EndDialogue() {

        dialogueBoxAnimator.SetBool("isOn", false);
        dialogueCanvas.gameObject.SetActive(false);
    }

}
