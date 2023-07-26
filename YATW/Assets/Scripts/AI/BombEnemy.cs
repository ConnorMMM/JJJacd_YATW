using System;

using UnityEngine;

namespace BladeWaltz.AI
{
	public class BombEnemy : BaseEnemy
	{
		[SerializeField] private int m_numOfBullets;
		[SerializeField] private float m_radius;
		
		protected override void Behaviour()
		{
			ChaseTarget(); // Seek
		}

		protected override void DeathBehaviour()
		{
			Explode();
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
	}
}