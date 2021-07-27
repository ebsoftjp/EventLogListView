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
        private List<ItemData> reserved = new List<ItemData>();
        private ScrollRect scrollRect;
        private EventLogData data;

        protected void OnInit()
        {
            var prefab = Resources.Load<GameObject>("Prefabs/EventLogCanvas");
            var obj = Instantiate(prefab);
            obj.transform.SetParent(transform);
            scrollRect = obj.GetComponentInChildren<ScrollRect>();

            data = Resources.Load<EventLogData>("EventLogData");
        }

        protected void OnDestroy()
        {
            Debug.Log("OnDestroy");
            foreach (Transform t in scrollRect.content)
            {
                t.GetComponent<ItemView>()?.Detach();
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
            var item = obj.GetComponent<ItemView>();
            var viewType = data.Get(logData.done ? logData.typeKey : "Loading");
            if (viewType != null)
            {
                item.text.color = viewType.color;
                item.icon.enabled = viewType.sprite != null;
                item.icon.sprite = viewType.sprite;
            }
            else
            {
                item.text.color = Color.white;
                item.icon.sprite = null;
            }
            item.Init(logData);
            item.UpdateContent();
        }

        // add
        public void AddEventLog(ItemData logData)
        {
            reserved.Add(logData);
        }

        // add
        public static void Add(string message)
        {
            Instance.AddEventLog(new ItemData(message, "Default"));
            Debug.Log(message);
        }

        // add error
        public static void Success(string message)
        {
            Instance.AddEventLog(new ItemData(message, "Done"));
            Debug.Log(message);
        }

        // add error
        public static void Error(string message)
        {
            Instance.AddEventLog(new ItemData(message, "Error"));
            Debug.LogError(message);
        }

        // add notification
        public static void Notification(string message)
        {
            Instance.AddEventLog(new ItemData(message, "Notification"));
            Debug.Log(message);
        }

        // add loading event
        public static ItemData AddLoading(string message)
        {
            var e = new ItemData(message, "Done", false);
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
