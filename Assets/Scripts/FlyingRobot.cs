using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingRobot : MonoBehaviour {

    public bool isIdle = true;

    // Vision Code
    private RaycastHit2D rayCastHit;
    private const float RAYCASTDIST = 3f;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    // when patrolling
    // for the debug jus want to test the ray detection to make sure it works
    public virtual IEnumerator Idle() {

        Vector2 dir = this.transform.TransformDirection(Vector2.down) * RAYCASTDIST;

        while (isIdle) {

            rayCastHit = Physics2D.Raycast(this.transform.position, Vector2.down, RAYCASTDIST);
            Debug.DrawRay(this.transform.position, dir, Color.green);

            yield return null;

        }

        yield return null;

    }

}
