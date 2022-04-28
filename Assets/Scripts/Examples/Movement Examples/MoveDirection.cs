using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDirection : MonoBehaviour
{
    public float speed = 5f;
    // Update is called once per frame
    void Update()
    {
        //teleports forward every update frame
        //ignores other objects/colliders
        //adjusts position/speed based of delta time
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
