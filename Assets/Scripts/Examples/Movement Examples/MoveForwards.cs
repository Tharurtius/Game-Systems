using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwards : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 goal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //moves towards a specific point in 3D space
        //teleports forward every update frame
        //ignores other objects/colliders
        //adjusts position/speed based of delta time
        transform.position = Vector3.MoveTowards(transform.position, goal, speed * Time.deltaTime);
    }
}
