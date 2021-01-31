using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickableObject : MonoBehaviour
{
    public bool notCentered = false, isPhone = false;

    private static float repulsionForce = 0.75f;

    private void Start()
    {
        this.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
    }

    public (GameObject, bool, bool) Pick()
    {
        return (this.gameObject, notCentered, isPhone);
    }

    public void SetAsUncentered()
    {
        notCentered = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isPhone && other.tag == "PlayerPush")
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.ProjectOnPlane(((this.transform.position - (this.GetComponent<Renderer>() != null ? this.GetComponent<Renderer>().bounds.center : this.transform.position) - other.transform.position)).normalized, Vector3.up) * repulsionForce, ForceMode.Impulse);
        }
    }
}
