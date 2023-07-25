using System;

using UnityEngine;

namespace BladeWaltz.AI
{
	public abstract class BaseBullet : MonoBehaviour
	{
		public float m_speed;
		public float m_timer = 5;
		
		private void FixedUpdate()
		{
			m_timer -= Time.deltaTime;
			if(m_timer <= 0)
			{
				Destroy(gameObject);
			}

			//transform.position = new Vector3(transform.position.x, transform.position.y, 0.5f);

			Behaviour();
		}

		private void OnTriggerEnter(Collider _col)
		{
			if(_col.CompareTag("Player"))
			{
				// take dmg + destroy bullet
				Debug.Log("Bullet hit player");
				Destroy(gameObject);
			}
			else
			{
				Destroy(gameObject);
			}
		}
		
		protected abstract void Behaviour();
	}
}