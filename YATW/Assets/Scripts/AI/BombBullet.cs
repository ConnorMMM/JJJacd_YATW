using System;

using UnityEngine;

namespace BladeWaltz.AI
{
	public class BombBullet : BaseBullet
	{
		public Vector3 m_parent;
		private Vector3 m_dirToParent;

		private void Start()
		{
			m_dirToParent = (transform.position - m_parent).normalized;
			m_dirToParent = new Vector3(m_dirToParent.x, 0, m_dirToParent.z);
		}

		protected override void Behaviour()
		{
			transform.position += m_dirToParent * Time.deltaTime * m_speed;
		}
	}
}