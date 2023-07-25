using UnityEngine;

namespace BladeWaltz.AI
{
	public class BasicRanged : BaseEnemy
	{
		[Header("Timers")]
		[Tooltip("Range that the enemy will stay at.")]
		[SerializeField] private float m_fleeRange;
		[Tooltip("Time between shots.")]
		[SerializeField] private float m_timerReset;
		[Tooltip("Starting timer till the enemy shoots.")]
		public float m_attackTimer;
		[SerializeField] private GameObject m_gun;
		
		public bool m_attack;
		
		protected override void Behaviour()
		{
			m_attackTimer -= Time.deltaTime;
			if(m_attackTimer <= 0)
			{
				m_attack = true;
				m_attackTimer = m_timerReset;
			}

			if(Vector3.Distance(transform.position, m_player.transform.position) < m_fleeRange) // Flee
			{
				FaceTarget();
				FleeTarget();
			}
			else if (Vector3.Distance(transform.position, m_player.transform.position) > m_fleeRange && m_attack == true) // Shoot
			{
				FaceTarget();
				AttackTarget();
				GameObject bullet = Instantiate(m_projectilePrefab, transform.position, Quaternion.identity, this.transform);
				bullet.transform.parent = null;
				bullet.GetComponent<BasicBullet>().m_speed = m_projectileSpeed;
				
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