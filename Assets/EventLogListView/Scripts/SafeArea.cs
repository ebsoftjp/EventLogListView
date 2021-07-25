using UnityEngine;

namespace EventLogListView
{
	public class SafeArea : MonoBehaviour
	{
		RectTransform panel;
		Rect lastSafeArea = Rect.zero;

		// Use this for initialization
		void Start()
		{
			panel = GetComponent<RectTransform>();
		}

		// Update is called once per frame
		void Update()
		{
			var area = Screen.safeArea;

			if (area != lastSafeArea)
			{
				var anchorMin = area.position;
				var anchorMax = area.position + area.size;
				anchorMin.x /= Screen.width;
				anchorMin.y /= Screen.height;
				anchorMax.x /= Screen.width;
				anchorMax.y /= Screen.height;
				panel.anchorMin = anchorMin;
				panel.anchorMax = anchorMax;
				lastSafeArea = area;
			}
		}
	}
}
