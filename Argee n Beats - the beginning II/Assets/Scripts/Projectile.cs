﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [HideInInspector]
    public float impulse;
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public float radius;
    [HideInInspector]
    public float damageRadius;
    [HideInInspector]
    public GameObject launcher;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == launcher)
        {
            return; // We should not be able to hit ourself
        }
        Collider[] allInRadius = Physics.OverlapSphere(transform.position, radius);
        foreach (var item in allInRadius)
        {
            if (item.gameObject == gameObject)
            {
                continue;
            }
            Vector3 forceDirection = item.transform.position - transform.position;
            float distance = forceDirection.magnitude;
            forceDirection.Normalize();

            MovementManager movementManager = item.gameObject.GetComponent<MovementManager>();
            if (movementManager != null)
            {
                // We assume it is an enemy here
                movementManager.AddImpulse(forceDirection * impulse * (1-(distance/radius)));
            }
            else
            {
                Rigidbody rb = item.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(forceDirection * impulse * (1 - (distance / radius)), ForceMode.Impulse);
                }
            }
        }

        Collider[] allInDamageRadius = Physics.OverlapSphere(transform.position, damageRadius);
        foreach (var item in allInRadius)
        {
            Health hp = item.gameObject.GetComponent<Health>();
            if (hp != null)
            {
                // We assume it is an enemy here
                Vector3 forceDirection = item.transform.position - transform.position;
                float distance = forceDirection.magnitude;
                hp.TakeDamage(Mathf.Max(1 - (distance / damageRadius), 0) * damage);
            }
        }

        Destroy(gameObject);
    }
}