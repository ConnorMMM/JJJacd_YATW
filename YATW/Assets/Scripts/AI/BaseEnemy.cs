using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BladeWaltz.Managers;

namespace BladeWaltz.AI
{
	[RequireComponent(typeof(NavMeshAgent))]
	public abstract class BaseEnemy : MonoBehaviour
	{
    
		[Header("AI Stats")] 
		[SerializeField] private float m_health;
		[SerializeField] private float m_moveSpeed;

		[Header("Weapon Stats")]
		[SerializeField] private GameObject m_projectilePrefab;
		[SerializeField] private float m_damage;
		[SerializeField] private float m_projectileSpeed;
    
		protected NavMeshAgent m_agent;
		protected GameManager m_gameManager;
		protected GameObject m_player;
		protected float m_distance;

		// Start is called before the first frame update
		private void Awake()
		{
			m_agent = GetComponent<NavMeshAgent>();
			m_gameManager = FindObjectOfType<GameManager>();
			
			// 1 in 20 chance for enemy to give speed on death
			int num = Random.Range(1, 20);
			if(num == 1)
			{
				IsSpecial();
			}
		}

		// Update is called once per frame
		private void FixedUpdate()
		{
			m_player = m_gameManager.m_player;
			m_distance = Vector3.Distance(transform.position, m_player.transform.position);
			
			// Custom behaviours for sub classes
			Behaviour();
		}

		private void IsSpecial()
		{
			// Change enemy colour or something
		}
    
		protected abstract void Behaviour();
	}
}