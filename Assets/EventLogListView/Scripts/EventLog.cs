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
            foreach (Transform t in scrollRect.content)
            {
                t.GetComponent<ItemView>()?.Detach();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (reserved.Count == 0 || (Time.timeScale == 0 && data.updateMode != AnimatorUpdateMode.UnscaledTime))
            {
                return;
            }

            var logData = reserved[0];
            reserved.RemoveAt(0);

            var obj = scrollRect.content.childCount > 0
                ? scrollRect.content.GetChild(0).gameObject
                : null;
            if (obj != null && obj.activeSelf)
            {
                if (scrollRect.content.childCount < data.itemLimit)
                {
                    obj = null;
                }
                else
                {
                    obj.SetActive(false);
                }
            }
            if (obj != null)
            {
                obj.SetActive(true);
                obj.transform.SetAsLastSibling();
            }
            else
            {
                var prefab = Resources.Load<GameObject>("Prefabs/EventLogItem");
                obj = Instantiate(prefab, scrollRect.content);
            }
            var item = obj.GetComponent<ItemView>();
            item.animator.updateMode = data.updateMode;
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
            Instance.AddEventLog(new ItemData(Instance.data, message, "Default"));
            if (Instance.data.enableDebugLog) Debug.Log(message);
        }

        // add success
        public static void Success(string message)
        {
            Instance.AddEventLog(new ItemData(Instance.data, message, "Done"));
            if (Instance.data.enableDebugLog) Debug.Log(message);
        }

        // add error
        public static void Error(string message)
        {
            Instance.AddEventLog(new ItemData(Instance.data, message, "Error"));
            if (Instance.data.enableDebugLog) Debug.LogError(message);
        }

        // add notification
        public static void Notification(string message)
        {
            Instance.AddEventLog(new ItemData(Instance.data, message, "Notification"));
            if (Instance.data.enableDebugLog) Debug.Log(message);
        }

        // add loading event
        public static ItemData AddLoading(string message)
        {
            var e = new ItemData(Instance.data, message, "Done", false);
            Instance.AddEventLog(e);
            if (Instance.data.enableDebugLog) Debug.Log(message);
            return e;
        }
    }
}
