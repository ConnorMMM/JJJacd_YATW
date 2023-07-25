using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BladeWaltz.Managers;

using System;
using System.Security.Cryptography;

namespace BladeWaltz.AI
{
	public class BasicBullet : BaseBullet
	{
		private Transform m_player;
		private GameManager m_gameManager;

		private Vector3 m_dirToPlayer;
		
		// Start is called before the first frame update
		private void Start()
		{
			m_gameManager = GameManager.Instance;
			m_player = m_gameManager.m_player.transform;
			m_dirToPlayer = (transform.position - m_player.position).normalized;
			m_dirToPlayer = new Vector3(m_dirToPlayer.x, 0, m_dirToPlayer.z);
		}

		protected override void Behaviour()
		{
			transform.position -= m_dirToPlayer * Time.deltaTime * m_speed;
		}
	}

}