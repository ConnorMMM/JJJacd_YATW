using System.Collections;

using UnityEditor.Search;

using UnityEngine;
using UnityEngine.PlayerLoop;

namespace BladeWaltz.AI
{
	public class BasicRanged : BaseEnemy
	{
		[SerializeField] private float m_fleeRange;

		private float m_attackTimer;
		
		protected override void Behaviour()
		{
			m_fleeRange -= Time.deltaTime;
			
			// Behaviour:
			// 1. Check distance, if distance within range, flee for set time
			// 2. Shoot
			// 3. Repeat
			if(Vector3.Distance(transform.position, m_player.transform.position) < m_fleeRange && m_attackTimer > 0)
			{
				Vector3 dirToPlayer = transform.position - m_player.transform.position;
				Vector3 newPos = transform.position + dirToPlayer;
				m_agent.SetDestination(newPos);
			}
			else if (Vector3.Distance(transform.position, m_player.transform.position) > m_fleeRange && m_attackTimer <= 0)
			{
				
			}
		}
	}
}