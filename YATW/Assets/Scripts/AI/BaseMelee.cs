using UnityEngine;
using UnityEngine.AI;

namespace BladeWaltz.AI
{
	public class BaseMelee : BaseEnemy
	{
		protected override void Behaviour()
		{
			ChaseTarget(); // Seek
		}
	}
}