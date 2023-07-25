using BladeWaltz.Character;
using BladeWaltz.Managers;

using System;

using UnityEngine;

namespace BladeWaltz.AI
{
	public abstract class BaseBullet : MonoBehaviour
	{
		public float m_damage = 100;
		public float m_speed;
		public float m_timer = 5;

		private void Awake()
		{
			Destroy(gameObject, m_timer);
		}

		private void FixedUpdate()
		{
			//transform.position = new Vector3(transform.position.x, transform.position.y, 0.5f);

			Behaviour();
		}

		private void OnTriggerEnter(Collider _col)
		{
			if(_col.CompareTag("Player"))
			{
				GameManager.Instance.m_player.GetComponent<CharacterManager>().TakeDamage(m_damage);
				
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