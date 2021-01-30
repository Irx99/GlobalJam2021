using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickableObject : MonoBehaviour
{
    public bool notCentered = false;

    private static float repulsionForce = 1f;

    private void Start()
    {
        this.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
    }

    public (GameObject, bool) Pick()
    {
        return (this.gameObject, notCentered);
    }

    public void SetAsUncentered()
    {
        notCentered = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "PlayerPush")
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.ProjectOnPlane((this.transform.position - other.transform.position).normalized, Vector3.up) * repulsionForce, ForceMode.Impulse);
        }
    }
}
