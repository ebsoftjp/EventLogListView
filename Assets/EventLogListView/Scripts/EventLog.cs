using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EventLogListView
{
    public class EventLog : MonoBehaviour
    {
        private static EventLog _Instance = null;
        private static bool _DestroyDone = false;
        private static readonly string dataPath = "EventLogListView/EventLogData";
        private static readonly string canvasPath = "EventLogListView/Prefabs/EventLogCanvas";
        private static readonly string itemPath = "EventLogListView/Prefabs/EventLogItem";

        /// <summary>
        /// Initialize singleton.
        /// </summary>
        public static EventLog Instance {
            get {
                if (_Instance == null && !_DestroyDone) {
                    _Instance = FindObjectOfType(typeof(EventLog)) as EventLog;
                    if (_Instance == null)
                    {
                        var obj = new GameObject(typeof(EventLog).Name);
                        _Instance = obj.AddComponent<EventLog>();
                        _Instance.OnCreateInstance();
                        DontDestroyOnLoad(obj);
                    }
                }
                return _Instance;
            }
        }

        /// <summary>
        /// Initialize EventLog after the scene has loaded.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void OnLoad()
        {
            _ = Instance;
        }

        private EventLogData data;
        private ScrollRect scrollRect;
        private List<ItemData> reservedItems = new List<ItemData>();

        /// <summary>
        /// Processing at the time of instance creation.
        /// </summary>
        private void OnCreateInstance()
        {
            data = Resources.Load<EventLogData>(dataPath);

            var prefab = Resources.Load<GameObject>(canvasPath);
            var obj = Instantiate(prefab, transform);
            scrollRect = obj.GetComponentInChildren<ScrollRect>();
        }

        /// <summary>
        /// Break the connection between item and data when the instance is destroyed.
        /// </summary>
        private void OnDestroy()
        {
            _DestroyDone = true;
            _Instance = null;
            foreach (Transform t in scrollRect.content)
            {
                t.GetComponent<ItemView>()?.Detach();
            }
        }

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        void Update()
        {
            if (_DestroyDone) return;
            if (reservedItems.Count == 0) return;
            if (Time.timeScale == 0 && data.updateMode != AnimatorUpdateMode.UnscaledTime) return;

            // object pooling
            var obj = scrollRect.content.childCount > 0
                ? scrollRect.content.GetChild(0).gameObject
                : null;
            if (obj != null && obj.activeSelf)
            {
                // check object limit
                if (scrollRect.content.childCount < data.itemLimit)
                {
                    // if the number of objects does not exceed the upper limit, create a new one
                    obj = null;
                }
                else
                {
                    // reuse the oldest object if the number of objects exceeds the limit
                    obj.SetActive(false);
                }
            }

            if (obj != null)
            {
                // reuse object
                obj.SetActive(true);
                obj.transform.SetAsLastSibling();
            }
            else
            {
                // create a new object
                var prefab = Resources.Load<GameObject>(itemPath);
                obj = Instantiate(prefab, scrollRect.content);
            }

            // initialize ItemView
            var item = obj.GetComponent<ItemView>();
            item.animator.updateMode = data.updateMode;
            item.Init(reservedItems[0]);
            reservedItems.RemoveAt(0);
        }

        /// <summary>
        /// Add event log.
        /// </summary>
        private ItemData AddEventLog(string message, string key)
        {
            if (key == "")
            {
                key = data.defaultKey;
            }
            var e = new ItemData(data, message, key);
            reservedItems.Add(e);
            e.DebugLog();
            return e;
        }

        /// <summary>
        /// Add normal event log.
        /// </summary>
        public static void Add(string message, string key = "")
        {
            if (_DestroyDone) return;
            _ = Instance.AddEventLog(message, key);
        }

        /// <summary>
        /// Add done event log.
        /// </summary>
        public static void AddDone(string message)
        {
            if (_DestroyDone) return;
            Add(message, Instance.data.doneKey);
        }

        /// <summary>
        /// Add error event log.
        /// </summary>
        public static void AddError(string message)
        {
            if (_DestroyDone) return;
            Add(message, Instance.data.errorKey);
        }

        /// <summary>
        /// Add loading event log.
        /// </summary>
        public static ItemData AddLoading(string message)
        {
            if (_DestroyDone) return null;
            return Instance.AddEventLog(message, Instance.data.loadingKey);
        }
    }
}
