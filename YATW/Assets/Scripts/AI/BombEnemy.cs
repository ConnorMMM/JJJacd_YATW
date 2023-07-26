using System;
using System.Collections;

using UnityEngine;
using UnityEngine.AI;

namespace BladeWaltz.AI
{
	public class BombEnemy : BaseEnemy
	{
		[SerializeField] private int m_numOfBullets;
		[SerializeField] private float m_radius;
		
		protected override void Behaviour()
		{
			ChaseTarget(); // Seek
			FaceTarget();
		}

		protected override void DeathBehaviour()
		{
			Destroy(gameObject.GetComponent<MeshRenderer>());
			m_agent.acceleration = 0;
			m_agent.velocity = new Vector3();
			m_agent.speed = 0;
			m_agent.angularSpeed = 0;

			StartCoroutine(ExplodeAfter(2));
		}

		private void Explode()
		{
			for(int i = 1; i <= m_numOfBullets; i++)
			{
				double x = transform.position.x + m_radius * Math.Cos(2 * Math.PI * i / m_numOfBullets);
				double z = transform.position.z + m_radius * Math.Sin(2 * Math.PI * i / m_numOfBullets);
				
				
				GameObject bullet = Instantiate(m_projectilePrefab, 
				                                new Vector3(
				                                            (float)x,
				                                            0.5f,
				                                            (float)z), 
				                                Quaternion.identity, 
				                                this.transform);
				
				bullet.transform.parent = null;
				bullet.GetComponent<BombBullet>().m_parent = transform.position;
				bullet.GetComponent<BombBullet>().m_damage = m_damage;
			}
		}

		IEnumerator ExplodeAfter(int _seconds)
		{
			yield return new WaitForSeconds(_seconds);
			Explode();
			Destroy(gameObject);
		}
	}
}