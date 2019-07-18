using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSound : MonoBehaviour
{

    public string soundName;

    public void Start()
    {
        Global.audiomanager.getSFX(soundName).play();

    }


}
