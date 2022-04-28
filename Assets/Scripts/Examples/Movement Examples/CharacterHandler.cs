using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandler : MonoBehaviour
{
    public float speed = 5f;
    public CharacterController charCon;
    public Vector3 moveDir;

    private void Start()
    {
        charCon = GetComponent<CharacterController>();
    }
    void Update()
    {
        //collides and registers physics
        //does register ground by default
        //doesnt register gravity by default
        //moves forward by direction/speed/over time
        moveDir = Vector3.forward;
        charCon.Move(moveDir * speed * Time.deltaTime);
    }
}
