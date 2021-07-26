using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EventLogListView
{
    public class EventLog : MonoBehaviour
    {
        // singleton
        private static EventLog _Instance = null;
        public static EventLog Instance {
            get {
                if (_Instance == null) {
                    _Instance = FindObjectOfType(typeof(EventLog)) as EventLog;
                    if (_Instance == null)
                    {
                        var obj = new GameObject(typeof(EventLog).Name);
                        _Instance = obj.AddComponent<EventLog>();
                        _Instance.OnInit();
                        DontDestroyOnLoad(obj);
                    }
                }
                return _Instance;
            }
        }

        /// <summary>
        /// Initialize EventLogListView after the scene has loaded.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void OnLoad()
        {
            Debug.Log("OnLoad");
            _ = EventLog.Instance;
        }

        private Font defalutFont;
        private List<EventLogData> reserved = new List<EventLogData>();
        private ScrollRect scrollRect;

        protected void OnInit()
        {
            var prefab = Resources.Load<GameObject>("Prefabs/EventLogCanvas");
            var obj = Instantiate(prefab);
            obj.transform.SetParent(transform);
            scrollRect = obj.GetComponentInChildren<ScrollRect>();
        }

        protected void OnDestroy()
        {
            Debug.Log("OnDestroy");
            foreach (Transform t in scrollRect.content)
            {
                t.GetComponent<EventLogItem>()?.Detach();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (reserved.Count == 0)
            {
                return;
            }

            var logData = reserved[0];
            reserved.RemoveAt(0);

            var prefab = Resources.Load<GameObject>("Prefabs/EventLogItem");
            // pool.get();
            var obj = Instantiate(prefab, scrollRect.content);
            var item = obj.GetComponent<EventLogItem>();
            item.Init(logData);
            item.UpdateContent();
        }

        // add
        public void AddEventLog(EventLogData logData)
        {
            reserved.Add(logData);
        }

        // add
        public static void Add(string message)
        {
            Instance.AddEventLog(new EventLogData(message));
            Debug.Log(message);
        }

        // add error
        public static void Success(string message)
        {
            Instance.AddEventLog(new EventLogData(message, EventLogStatus.Done));
            Debug.Log(message);
        }

        // add error
        public static void Error(string message)
        {
            Instance.AddEventLog(new EventLogData(message, EventLogStatus.Failed));
            Debug.LogError(message);
        }

        // add notification
        public static void Notification(string message)
        {
            Instance.AddEventLog(new EventLogData(message, EventLogStatus.Notification));
            Debug.Log(message);
        }

        // add loading event
        public static EventLogData AddLoading(string message)
        {
            var e = new EventLogData(message, EventLogStatus.Loading);
            Instance.AddEventLog(e);
            Debug.Log(message);
            return e;
        }

        // release event
        public void ReleaseEventLog(GameObject obj)
        {
            // pool.Release(obj);
            Destroy(obj);
        }
    }
}
