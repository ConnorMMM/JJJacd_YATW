﻿using System;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

namespace BladeWaltz.JsonSaving
{
	public class InputHandler : MonoBehaviour
	{
		[SerializeField] private TMP_Text m_letterOne;
		[SerializeField] private TMP_Text m_letterTwo;
		[SerializeField] private TMP_Text m_letterThree;
		[SerializeField] private TMP_Text m_score;
		[SerializeField] private TMP_Text m_time;
		[SerializeField] private string m_fileName;

		public List<InputEntry> m_entries = new List<InputEntry>();
		
		private void Start()
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
		}

		public void AddInfoToList()
		{
			string nameInput = "" + m_letterOne.text + m_letterTwo.text + m_letterThree.text;
			m_entries.Add(new InputEntry(nameInput, m_score.text, m_time.text));

			JsonHandler.SaveToJson<InputEntry>(m_entries, m_fileName);
		}
	}
}