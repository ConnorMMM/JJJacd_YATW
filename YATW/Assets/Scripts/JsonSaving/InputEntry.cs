using System;

using UnityEngine;

namespace BladeWaltz.JsonSaving
{
	[Serializable]
	public class InputEntry
	{
		public string m_playerName;
		public string m_playerScore;

		public InputEntry(string _name, string _score)
		{
			m_playerName = _name;
			m_playerScore = _score;
		}
	}
}