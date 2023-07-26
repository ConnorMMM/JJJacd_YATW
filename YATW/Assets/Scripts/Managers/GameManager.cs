using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BladeWaltz.DesignPatterns;

using System;

using TMPro;

using Random = UnityEngine.Random;

namespace BladeWaltz.Managers
{
	public class GameManager : Singleton<GameManager>
	{
		[Header("Timer settings")]
		[Tooltip("Starting game time in minutes.")]
		public float m_startTime;
		[Tooltip("Time until game ends.")]
		public int m_timer = 0;

		private float m_timeValue; 

		[Header("AI Settings")]
		[SerializeField] private GameObject m_basicRanged;
		[SerializeField, Range(0, 1)] private float m_rangedSpawnChance;
		[SerializeField] private GameObject m_basicMelee;
		[SerializeField, Range(0, 1)] private float m_meleeSpawnChance;
		[SerializeField] private GameObject m_basicBomber;
		[SerializeField, Range(0, 1)] private float m_bomberSpawnChance;

		[Header("Spawn Settings")] 
		[SerializeField, Tooltip("If left blank, will use (0,0)")]
		private Transform m_spawnPoint;
		[SerializeField, Tooltip("Radius of a circle around the spawn point that enemies with appear")]
		private float m_spawnRadius = 45f;
		[SerializeField, Tooltip("How many enemies spawn every second")]
		private float m_spawnRate = 2;
		[SerializeField, Tooltip("How mach spawn rate increases every second")]
		private float m_spawnRateIncrease = 0.2f;

		[Header("Player Info")]
		[SerializeField] public TMP_Text m_scoreText;
		public int m_score;

		public GameObject m_player;

		[SerializeField] private TMP_Text m_timerText;
		[SerializeField] private GameObject m_finalPrompt;

		protected override void Awake()
		{
			Time.timeScale = 1.0f;
			m_timer = (int)m_startTime * 60;
			StartCoroutine(SpawningEnemies());
			StartCoroutine(SpawnRateIncrease());
			m_timeValue = 90;
		}

		private void Update()
		{
			if(m_timeValue > 0)
			{
				m_timeValue -= Time.deltaTime;
			}
			else
			{
				m_timeValue = 0;
			}
			
			DisplayTime(m_timeValue);
		}

		// Update is called once per frame
		private void FixedUpdate()
		{
		}

		private IEnumerator SpawningEnemies()
		{
			while(true)
			{
				yield return new WaitForSeconds(1 / m_spawnRate);
				SpawnEnemy();
			}
		}
		
		private IEnumerator SpawnRateIncrease()
		{
			while(true)
			{
				yield return new WaitForSeconds(1);
				m_spawnRate += m_spawnRateIncrease;
			}
		}

		private void SpawnEnemy()
		{
			float totalChance = m_rangedSpawnChance + m_meleeSpawnChance + m_bomberSpawnChance;
			
			float rangedChance = m_rangedSpawnChance / totalChance;
			float meleeChance = m_meleeSpawnChance / totalChance;
			float bomberChance = m_bomberSpawnChance / totalChance;

			float rndNum = Random.value;
			if(rndNum <= rangedChance)
			{
				Instantiate(m_basicRanged, GetRandomSpawnPoint(), Quaternion.identity);
			}
			else if(rndNum <= rangedChance + meleeChance)
			{
				Instantiate(m_basicMelee, GetRandomSpawnPoint(), Quaternion.identity);
			}
			else if(rndNum <= rangedChance + meleeChance + bomberChance)
			{
				Instantiate(m_basicBomber, GetRandomSpawnPoint(), Quaternion.identity);
			}
		}

		private Vector3 GetRandomSpawnPoint()
		{
			float degree = Random.value * 360;

			float x = m_spawnRadius * Mathf.Sin(degree);
			float z = m_spawnRadius * Mathf.Cos(degree);

			if(m_spawnPoint != null)
			{
				x += m_spawnPoint.position.x;
				z += m_spawnPoint.position.z;
			}
				
			return new Vector3(x, 0.5f, z);
		}
    
		public void AddScore(int _score)
		{
			m_score += _score;
		}

		public void AddTime(float _time)
		{
			//m_timer += _time;
		}

		public void PlayerDeath()
		{
			Time.timeScale = 0.0f;
			m_finalPrompt.SetActive(true);
		}

		private void DisplayTime(float _timeToDisplay)
		{
			if(_timeToDisplay < 0)
			{
				_timeToDisplay = 0;
			}

			float minutes = Mathf.FloorToInt(_timeToDisplay / 60);
			float seconds = Mathf.FloorToInt(_timeToDisplay % 60);

			m_timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
		}
	}
}