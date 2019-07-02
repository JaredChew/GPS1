using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlyingEnemyVision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // if enter lose?
        if (other.gameObject.name == "Player")
        {
            Debug.Log("Spotted");
            SceneManager.LoadScene("Throw");
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // this jus for checking working or not
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Searching");
        }
    }
}
