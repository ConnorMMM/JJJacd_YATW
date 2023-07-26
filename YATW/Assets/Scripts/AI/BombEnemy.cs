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
				double theta = (Math.PI * 2 / m_numOfBullets);
				double degree = theta * i;
				//float degree = (float)(theta * 180 / Math.PI);

				double x = m_radius * Math.Cos(degree) + transform.position.x;
				double y = m_radius * Math.Sin(degree) + transform.position.y;
				
				GameObject bullet = Instantiate(m_projectilePrefab, 
				                                new Vector3(
				                                            (float)x,
				                                            0.5f,
				                                            (float)y), 
				                                Quaternion.identity);

				bullet.transform.parent = null;
				bullet.GetComponent<BombBullet>().m_parent = transform;
				bullet.GetComponent<BombBullet>().m_damage = m_damage;
			}
		}
	}
}