using System;

using UnityEngine;

namespace BladeWaltz.JsonSaving
{
	[Serializable]
	public class InputEntry
	{
		public string m_playerName;
		public string m_playerScore;
		public string m_playerTime;

		public InputEntry(string _name, string _score, string _time)
		{
			m_playerName = _name;
			m_playerScore = _score;
			m_playerTime = _time;
		}
	}
}