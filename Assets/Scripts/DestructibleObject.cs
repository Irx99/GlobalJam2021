using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.relativeVelocity.magnitude > 15f)
        {
            Destroy();
        }
    }
}
