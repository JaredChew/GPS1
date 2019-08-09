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

        if(startTime >= 0.3f)
        {
            gameObject.SetActive(false);
        }
    }
}
