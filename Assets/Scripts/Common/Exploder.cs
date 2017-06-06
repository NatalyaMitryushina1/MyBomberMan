﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Common
{
	public static class Exploder
	{
		private static float explosionDuration = 0.5f;

		public static IEnumerator Explode(GameObject explodedObject, float explosionDelay, float explosionDistance,
			GameObject particleSystemPrefab, Action<RaycastHit[]> explodeCallBack = null)
		{
			yield return new WaitForSeconds(explosionDelay);
			ShowExplosionEffects(explodedObject, explosionDistance, particleSystemPrefab);		

			yield return new WaitForSeconds(explosionDuration);
			RaycastHit[] hittedObjects = GetHittedObjects(explodedObject, explosionDistance);

			if (explodeCallBack != null)
			{
				explodeCallBack(hittedObjects);
			}

			UnityEngine.Object.DestroyObject(explodedObject);
		}

		private static void ShowExplosionEffects(GameObject explodedObject, float explosionDistance, GameObject particleSystemPrefab)
		{
			Vector3 explosionPosition = explodedObject.transform.position;
			GameObject particleSystemObject = GameObject.Instantiate(particleSystemPrefab, explosionPosition, Quaternion.identity);
			ParticleSystem[] particles = particleSystemObject.GetComponentsInChildren<ParticleSystem>();
			foreach(ParticleSystem particle in particles)
			{
				var mainSettings = particle.main;
				mainSettings.startSpeed = explosionDistance;
				particle.Play();
				
			}
		}

		private static RaycastHit[] GetHittedObjects(GameObject explodedObject, float explosionDistance)
		{
			List<RaycastHit> hittedObjects = new List<RaycastHit>();
			Vector3 start = explodedObject.transform.position;
			foreach(Vector3 bombDirection in PhysicsHelper.bombDirections)
			{
				RaycastHit[] hittedObjectsInOneDirection = Physics.RaycastAll(explodedObject.transform.position, bombDirection, explosionDistance);
				if(hittedObjectsInOneDirection != null)
				{
					hittedObjects.AddRange(hittedObjectsInOneDirection);
				}
			}
			return hittedObjects.ToArray();
		}
	}
}