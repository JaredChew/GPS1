using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    [SerializeField] private Global.CheckpointLocation checkpointLocation;

    private bool indicatorActive = false;
    private Animator checkpointAnim;


    private void Start()
    {

        checkpointAnim = GetComponent<Animator>();

        if (Global.gameManager.getLastCheckpointAt() == checkpointLocation)
        {

            indicatorActive = true;

            //play animation (need to fix dunno)
            checkpointAnim.SetBool("activeTrigger", true);

        }

    }

    private void Update()
    {

        if (Global.gameManager.getLastCheckpointAt() != checkpointLocation && indicatorActive)
        {
            indicatorActive = false;

            //play animation      
            checkpointAnim.SetBool("checkpointTrigger", false);
            checkpointAnim.SetBool("activeTrigger", false);
            checkpointAnim.SetBool("isActive", false);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag(Global.tagPlayer))
        {


            if (Global.gameManager.getLastCheckpointAt() != checkpointLocation)
            {
                Global.gameManager.setCurrentCheckpoint(checkpointLocation);
                indicatorActive = true;

                //sound
                Global.audiomanager.getSFX("checkpoint_sound").play();
                //play animation               
                checkpointAnim.SetBool("checkpointTrigger", true);
                checkpointAnim.SetBool("isActive", true);
            }

            Global.gameManager.saveGame();

        }

    }

    public bool getIsIndicatorActive()
    {
        return indicatorActive;
    }

    public Global.CheckpointLocation getCheckpointLocation()
    {
        return checkpointLocation;
    }

}