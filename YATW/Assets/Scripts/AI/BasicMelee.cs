using UnityEngine;
using UnityEngine.AI;

namespace BladeWaltz.AI
{
	public class BasicMelee : BaseEnemy
	{
		protected override void Behaviour()
		{
			ChaseTarget(); // Seek
			FaceTarget();
		}

		protected override void DeathBehaviour()
		{
			Debug.Log("UwU");
		}
	}
}