using System.Collections.Generic;
using UnityEngine;

using BladeWaltz.JsonSaving;

using System;

namespace BladeWaltz.UI
{
	public class HighScoreHandler : MonoBehaviour
	{
		[SerializeField] private Transform m_canvas;
		[SerializeField] private GameObject m_scorePrefab;
		[SerializeField] private string m_fileName;
		
		private List<GameObject> m_listOfHighScores = new List<GameObject>();
		public List<InputEntry> m_entries = new List<InputEntry>();
		
		private void Start()
		{
			m_entries = JsonHandler.ReadFromJson<InputEntry>(m_fileName);

			if(m_entries.Count >= 5)
			{
				for(int i = 0; i <= 4; i++)
				{
					GameObject currentPrefab = Instantiate(m_scorePrefab, m_canvas);
					m_listOfHighScores.Add(currentPrefab);
					HighScore currentHighScore = currentPrefab.GetComponent<HighScore>();
					currentHighScore.m_placement = i + 1;
					currentHighScore.m_name = m_entries[i].m_playerName;
					currentHighScore.m_score = int.Parse(m_entries[i].m_playerScore);
					currentHighScore.m_time = m_entries[i].m_playerTime;
				}
			}
			else
			{
				for(int i = 0; i < m_entries.Count; i++)
				{
					GameObject currentPrefab = Instantiate(m_scorePrefab, m_canvas);
					m_listOfHighScores.Add(currentPrefab);
					HighScore currentHighScore = currentPrefab.GetComponent<HighScore>();
					currentHighScore.m_placement = i + 1;
					currentHighScore.name = m_entries[i].m_playerName;
					currentHighScore.m_score = int.Parse(m_entries[i].m_playerScore);
					currentHighScore.m_time = m_entries[i].m_playerTime;
				}
				for(int i = m_entries.Count; i <= 4; i++)
				{
					GameObject currentPrefab = Instantiate(m_scorePrefab, m_canvas);
					m_listOfHighScores.Add(currentPrefab);
					HighScore currentHighScore = currentPrefab.GetComponent<HighScore>();
					currentHighScore.m_placement = i + 1;
					currentHighScore.m_name = "SUS";
					currentHighScore.m_score = 000;
					currentHighScore.m_time = "00:00";
				}
			}
		}

		private void Update()
		{
			if(Input.GetKey(KeyCode.Q))
			{
				UpdateScores();
			}
		}

		public void UpdateScores()
		{
			m_entries = JsonHandler.ReadFromJson<InputEntry>(m_fileName);

			for(int i = 0; i < m_entries.Count; i++)
			{
				for(int j = 1; j < m_entries.Count; j++)
				{
					if(int.Parse(m_entries[j].m_playerScore) > int.Parse(m_entries[j - 1].m_playerScore))
					{
						InputEntry tmp = m_entries[j];
						m_entries[j] = m_entries[j - 1];
						m_entries[j - 1] = tmp;
					}
				}
			}
			if(m_entries.Count >= 4)
			{
				for(int i = 0; i <= 4; i++)
				{
					HighScore currentHighScore = m_listOfHighScores[i].GetComponent<HighScore>();
					currentHighScore.m_placement = i + 1;
					currentHighScore.m_name = m_entries[i].m_playerName;
					currentHighScore.m_score = int.Parse(m_entries[i].m_playerScore);
					currentHighScore.m_time = m_entries[i].m_playerTime;
				}
			}
			else
			{
				//int tmp = numOfScores - entries.Count;
				for(int i = 1; i <= m_entries.Count; i++)
				{
					HighScore currentHighScore = m_listOfHighScores[i - 1].GetComponent<HighScore>();
					currentHighScore.m_placement = i;
					currentHighScore.m_name = m_entries[i - 1].m_playerName;
					currentHighScore.m_score = int.Parse(m_entries[i - 1].m_playerScore);
					currentHighScore.m_time = m_entries[i].m_playerTime;
				}
				for(int i = m_entries.Count + 1; i <= 4; i++)
				{
					HighScore currentHighScore = m_listOfHighScores[i - 1].GetComponent<HighScore>();
					currentHighScore.m_placement = i;
					currentHighScore.m_name = "SUS";
					currentHighScore.m_score = 000;
					currentHighScore.m_time = "00:00";
				}
			}
		}
	}
}