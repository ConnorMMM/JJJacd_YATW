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

			if(m_health <= 0)
			{
				Explode();
			}
		}

		private void Explode()
		{
			float circle = 360 / m_numOfBullets;
			for(int i = 0; i < m_numOfBullets; i++)
			{
				GameObject bullet = Instantiate(m_projectilePrefab, 
				                                new Vector3((float)(Math.Cos(i * circle) * m_radius + transform.position.x), 
				                                            0.5f, 
				                                            (float)(Math.Sin(i * circle) * m_radius + transform.position.y)), 
				                                Quaternion.identity, 
				                                this.transform);

				bullet.transform.parent = null;
				bullet.GetComponent<BombBullet>().m_parent = transform;
			}
			
			Destroy(gameObject);
		}
	}
}