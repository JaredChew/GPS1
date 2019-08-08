using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    private float startTime = 0.0f;

    // Update is called once per frame
    void Update()
    {
        startTime += 0.1f * Time.deltaTime;

        if(startTime >= 1.0f)
        {
            gameObject.SetActive(false);
        }
    }
}
