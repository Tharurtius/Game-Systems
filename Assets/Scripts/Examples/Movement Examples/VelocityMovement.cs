using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityMovement : MonoBehaviour
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
        //directly changes speed/sets speed not acceleration
        //has gravity
        //does collide and register physics
        //doesnt register the ground
        rb.velocity = Vector3.forward * speed;
    }
}
