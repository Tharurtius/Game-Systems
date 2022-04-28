using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceMovement : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //accelerates the object
        //can spin and bounce
        //has gravity
        //doesnt register ground by default
        //does collide and register physics
        //good for ice type games or driving as it seems slidy
        rb.AddForce(Vector3.forward * speed);
    }
}
