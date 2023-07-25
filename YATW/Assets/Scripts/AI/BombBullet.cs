using System;

using UnityEngine;

namespace BladeWaltz.AI
{
	public class BombBullet : BaseBullet
	{
		public Transform m_parent;
		private Vector3 m_dirToParent;

		private void Start()
		{
			m_dirToParent = (transform.position - m_parent.position).normalized;
		}

		protected override void Behaviour()
		{
			transform.position += m_dirToParent * Time.deltaTime * m_speed;
		}
	}
}