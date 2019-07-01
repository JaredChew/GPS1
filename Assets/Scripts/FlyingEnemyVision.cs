using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyVision : MonoBehaviour
{
    private bool isFlipping;

    private void Start()
    {
        isFlipping = false;
        StartCoroutine("Flip");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // if enter lose?
        if (other.gameObject.name == "Player")
        {
            Debug.Log("Spotted");
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

    private IEnumerator Flip()
    {
        while (true)
        {
            // for now i only able to flip the sprite every 3 sec because dk how to implement the code for rotating the vision every 3 sec
            GetComponent<SpriteRenderer>().flipX = isFlipping;
            isFlipping = !isFlipping;
            yield return new WaitForSeconds(3f);
        }
    }
}
