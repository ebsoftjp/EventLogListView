﻿using System.Linq;
using UnityEngine;

namespace EventLogListView
{
    [CreateAssetMenu(menuName = "EventLogListView/EventLogData", fileName = "EventLogData")]
    public class EventLogData : ScriptableObject
    {
        public bool enableDebugLog = true;
        public AnimatorUpdateMode updateMode = AnimatorUpdateMode.UnscaledTime;
        public float itemLimit = 32;

        [Header("Keys")]
        public string defaultKey = "Default";
        public string doneKey = "Done";
        public string errorKey = "Error";
        public string loadingKey = "Loading";

        [Header("View type list")]
        public ViewType[] list;

        /// <summary>
        /// Get ViewType from key string.
        /// </summary>
        public ViewType Get(string key)
        {
            return list.FirstOrDefault((v) => v.key == key);
        }

        [System.Serializable]
        public class ViewType
        {
            public string key = "";
            public Color color = Color.white;
            public Sprite sprite = null;
        }
    }
}
