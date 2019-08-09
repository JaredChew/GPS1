using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroEvent : MonoBehaviour
{
    private Animator introAnim;
    private Dialogue dialogue;
    private DialogueManager dialogueManager;
    private bool isDialogueActive;
    [SerializeField]private GameObject DialogueManager;

    private void Awake()
    {
        dialogue = DialogueManager.transform.Find("IntroDialogue").gameObject.GetComponent<Dialogue>();
        dialogueManager = DialogueManager.GetComponent<DialogueManager>();
        introAnim = this.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isDialogueActive)
        {
            dialogueManager.DisplayNextSentence();
        }
        if (dialogueManager.introDetect)
        {
            introAnim.SetBool("nextPic", true);
        }
        if(dialogueManager.detectSentenceEnd)
        {
            introAnim.SetBool("changeScene", true);
        }
    }
    private void DialogueActivate()
    {
        isDialogueActive = true;
        dialogue.TriggerDialogue();
    }
   
    private void SceneChanging()
    {
        //remove// to change scene to new game
        //SceneManager.LoadScene((int)sceneToLoad);
        //Global.audiomanager.getBGM("main_menu").stop();
        //Global.audiomanager.getBGM("main_BGM").play();
    }
}
