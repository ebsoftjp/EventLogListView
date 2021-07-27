using System.Linq;
using UnityEngine;

namespace EventLogListView
{
    [CreateAssetMenu(menuName = "EventLogListView/EventLogData", fileName = "EventLogData")]
    public class EventLogData : ScriptableObject
    {
        public bool enableDebugLog = true;
        public float itemLimit = 32;
        public float offsetHeight = 10;
        public ViewType[] list;

        public ViewType Get(string key)
        {
            return list.FirstOrDefault((v) => v.name == key);
        }
    }

    [System.Serializable]
    public class ViewType
    {
        public string name = "";
        public Color color = Color.white;
        public Sprite sprite = null;
    }
}
