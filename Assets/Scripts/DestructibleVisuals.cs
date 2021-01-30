﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using UnityEngine.InputSystem;
public class DestructibleVisuals : MonoBehaviour
{
    [Header("Objects")]
    public GameObject notDestroyedMesh;
    public GameObject destroyedMesh;
    [Header("Physics")]
    public float explosionForce;
    public float explosionRadius;
    public float explosionUpForce;
    [Header("Feedbacks")]
    public MMFeedbacks feedbacks;

    protected List<Collider> _colliders = new List<Collider>();

    private void Awake()
    {
        foreach(Transform tr in destroyedMesh.transform)
        {
            _colliders.Add(tr.GetComponent<Collider>());
        }
    }

    private void Update()
    {
        if (Keyboard.current[Key.Space].wasPressedThisFrame)
            Destroy();
    }
    public void Destroy()
    {
        notDestroyedMesh.SetActive(false);
        destroyedMesh.SetActive(true);
        feedbacks.PlayFeedbacks();
        foreach (Collider col in _colliders)
        {
            col.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, destroyedMesh.transform.position, explosionRadius, explosionUpForce);
        }
    }
}