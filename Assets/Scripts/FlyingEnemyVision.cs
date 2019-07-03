using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlyingEnemyVision : MonoBehaviour {

    void OnTriggerStay2D(Collider2D other) {

        if (other.CompareTag(Global.tagPlayer)) {
            other.GetComponent<Player>().killPlayer();
        }

    }

}
