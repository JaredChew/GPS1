using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene2 : MonoBehaviour
{
    private float startTime = 0.0f;

    // Update is called once per frame
    void Update()
    {
        startTime += 0.1f * Time.deltaTime;

        if (startTime >= 0.5f)
        {
            SceneManager.LoadScene("MainMenu");
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
