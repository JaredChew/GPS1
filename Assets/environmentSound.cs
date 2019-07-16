using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class environmentSound : MonoBehaviour
{
    public string soundName;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, Vector2.one);
    }
    public void Start()
    {
        Global.audiomanager.getSFX(soundName).play();

    }


}
