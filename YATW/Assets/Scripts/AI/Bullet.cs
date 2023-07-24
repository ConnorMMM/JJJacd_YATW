using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BladeWaltz.Managers;

using System.Security.Cryptography;

namespace BladeWaltz.AI
{
	public class Bullet : MonoBehaviour
	{
		private Transform m_player;
		private GameManager m_gameManager;
		[SerializeField] private float m_speed;

		private float m_timer = 5;
		private Vector3 m_dirToPlayer;
		
		// Start is called before the first frame update
		private void Start()
		{
			m_gameManager = FindObjectOfType<GameManager>();
			m_player = m_gameManager.m_player.transform;
			m_dirToPlayer = (GetComponentInParent<Transform>().position - m_player.position).normalized;
		}

		// Update is called once per frame
		private void FixedUpdate()
		{
			m_timer -= Time.deltaTime;

			transform.position -= m_dirToPlayer * Time.deltaTime;
			if(m_timer <= 0)
			{
				Destroy(this);
			}
		}
	}

}