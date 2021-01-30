using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    private void Start()
    {
        if(this.GetComponent<Collider>() == null)
        {
            Debug.LogWarning("Hay un DestructibleObject sin collider");
        }
    }

    public void Destroy()
    {
        if(this.gameObject.GetComponent<DestructibleVisuals>() != null)
        {
            this.gameObject.GetComponent<DestructibleVisuals>().Destroy();
            Destroy(this.gameObject.GetComponent<Collider>());
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.relativeVelocity.magnitude > 15f)
        {
            Destroy();
        }
    }
}
