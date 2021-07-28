using UnityEngine;

namespace EventLogListView
{
	public class SafeArea : MonoBehaviour
	{
		RectTransform panel;
		Rect lastSafeArea = Rect.zero;

        /// <summary>
        /// Use this for initialization.
        /// </summary>
		void Start()
		{
			panel = GetComponent<RectTransform>();
		}

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
		void Update()
		{
			var area = Screen.safeArea;

			if (area != lastSafeArea)
			{
				// fit object size to safe area
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
