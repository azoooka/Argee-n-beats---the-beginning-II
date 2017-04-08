﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackBase : MonoBehaviour {
    public GameObject objectToAttack;
    public float damage;
    [Tooltip("Attacks per second")]
    public float attackSpeed;
    public float attackDistance;

    protected float attackTimer;
	// Use this for initialization
	protected void BaseStart () {
        attackTimer = 1/attackSpeed;

        GameObject navigation = GetComponent<FollowNavigationAgent>().navigation;
        if (navigation != null)
        {
            navigation.GetComponent<NavMeshAgent>().stoppingDistance = attackDistance;
        }
    }
	
	// Update is called once per frame
	protected void BaseUpdate() {
        attackTimer -= Time.deltaTime;
    }

    protected bool CanIAttackTarget()
    {
        return attackTimer <= 0 && objectToAttack != null && (transform.position - objectToAttack.transform.position).magnitude <= attackDistance;
    }
}
