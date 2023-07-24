using UnityEngine;
using UnityEngine.AI;

namespace BladeWaltz.AI
{
	public class BaseMelee : BaseEnemy
	{
		protected override void Behaviour()
		{
			m_agent.SetDestination(m_player.transform.position);
		}
	}
}