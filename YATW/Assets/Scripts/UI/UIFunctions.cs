using UnityEngine;

namespace BladeWaltz.UI
{
	public class UIFunctions : MonoBehaviour
	{
		public void OnPause()
		{
			Time.timeScale = 0.0f;
		}
	}
}