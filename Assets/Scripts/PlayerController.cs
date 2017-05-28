﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Common;

public class PlayerController: MonoBehaviour
{
	private Vector3 direction;
	private float speed = 1f;
	private float distance = 1f;
	Movement mov;

	void Start()
	{
		mov = new Movement();
	}

	void Update()
	{
		direction = PhysicsHelper.GetDirectionByKey();
		Vector3 nextPoint = transform.position + direction * distance;
		if (!IsPossible(nextPoint)) mov.TryMove(this, direction, speed, distance);
	}

	private bool IsPossible(Vector3 nextPoint)
	{

		nextPoint = transform.position + direction * distance;
		var hitColliders = Physics.OverlapCapsule(nextPoint, nextPoint, 0.5f);
		foreach (var col in hitColliders)
		{
			if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Brick Wall") return true;
		}
		return false;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
			DestroyObject(this.gameObject);
		}
	}

}