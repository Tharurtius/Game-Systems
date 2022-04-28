using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Stats
{
    public string UserName;
    public int damage = 10;
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Attributes>())
        {
            collision.gameObject.GetComponent<Attributes>().attributes[0].curValue -= damage;
            Debug.Log(collision.gameObject.name);
        }
    }
}
