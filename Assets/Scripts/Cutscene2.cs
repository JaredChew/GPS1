using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene2 : MonoBehaviour
{
    private float startTime = 0.0f;

    // Update is called once per frame
    void Update()
    {
        startTime += 0.1f * Time.deltaTime;

        if (startTime >= 2.0f)
        {
            gameObject.SetActive(false);
        }
    }
}
