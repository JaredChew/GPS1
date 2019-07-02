using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    private bool isFlipping;
    public GameObject visionLeft;
    public GameObject visionRight;

    private void Start()
    {
        isFlipping = false;
        StartCoroutine("Flip");
    }

    private void Update()
    {
        StartCoroutine("Vision");
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
    private IEnumerator Vision()
    {
        //Wait for 2 secs.
        yield return new WaitForSeconds(3.0f);

        //Turn My game object that is set to on to off.
        visionLeft.SetActive(false);
        visionRight.SetActive(false);

        //Turn the Game Oject back on after 1 sec.
        yield return new WaitForSeconds(4.5f);

        //Game object will turn on
        visionLeft.SetActive(true);
        visionRight.SetActive(true);
    }
}
