using UnityEngine;

namespace BladeWaltz.AI
{
	public class BasicRanged : BaseEnemy
	{
		[SerializeField] private float m_fleeRange;
		[SerializeField] private float m_timerReset;

		[SerializeField] private GameObject m_gun;
		
		public float m_attackTimer;
		public bool m_attack;
		
		protected override void Behaviour()
		{
			m_attackTimer -= Time.deltaTime;
			if(m_attackTimer <= 0)
			{
				m_attack = true;
			}

			if(Vector3.Distance(transform.position, m_player.transform.position) < m_fleeRange && m_attack == false) // Flee
			{
				FaceTarget();
				FleeTarget();
			}
			else if (Vector3.Distance(transform.position, m_player.transform.position) > m_fleeRange && m_attack == true) // Shoot
			{
				FaceTarget();
				AttackTarget();
				Instantiate(m_projectilePrefab, m_gun.transform);
				m_attack = false;
				m_attackTimer = m_timerReset;
			}
			else if(Vector3.Distance(transform.position, m_player.transform.position) > m_fleeRange && m_attack == false) // Seek
			{
				FaceTarget();
				ChaseTarget();
			}
		}


	}
}