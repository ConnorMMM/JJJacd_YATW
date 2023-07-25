using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BladeWaltz.DesignPatterns;

using System;

namespace BladeWaltz.Managers
{
	public class GameManager : Singleton<GameManager>
	{
		[Header("Timer settings")]
		[Tooltip("Starting game time in minutes.")]
		public float m_startTime;
		[Tooltip("Time until game ends.")]
		public float m_timer;

		[Header("AI Settings")]
		[SerializeField] private GameObject m_basicRanged;
		[SerializeField] private GameObject m_basicMelee;
		[SerializeField] private GameObject m_spawnPoint; // We add more later

		[Header("Player Info")] 
		public int m_score;

		public GameObject m_player;

		protected override void Awake()
		{
			m_timer = m_startTime * 60;
		}

		// Update is called once per frame
		private void FixedUpdate()
		{
			m_timer -= Time.deltaTime;
		}

		private void SpawnEnemy()
		{
        
		}
    
		public void AddScore(int _score)
		{
			m_score += _score;
		}

		public void AddTime(float _time)
		{
			m_timer += _time;
		}
	}
}