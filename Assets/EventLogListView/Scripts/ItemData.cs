using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace EventLogListView
{
    public class ItemData
    {
        public ItemView item;
        public string message;

        private EventLogData data;
        private string key;
        private DateTime time;

        /// <summary>
        /// Check loading.
        /// </summary>
        public bool IsLoading {
            get { return key == data.loadingKey; }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ItemData(EventLogData data, string message, string key)
        {
            this.data = data;
            this.message = message;
            this.key = key;
            time = DateTime.Now;
        }

        /// <summary>
        /// Get ViewType data.
        /// </summary>
        public EventLogData.ViewType GetViewType()
        {
            return data.Get(key);
        }

        /// <summary>
        /// Output debug string to console.
        /// </summary>
        public void DebugLog()
        {
            if (data.enableDebugLog)
            {
                if (key == data.errorKey)
                {
                    Debug.LogError(message);
                }
                else
                {
                    Debug.Log(message);
                }
            }
        }

        /// <summary>
        /// Loading complete and add message.
        /// </summary>
        private void UpdateItem(string appendMessage = "")
        {
            message += appendMessage;
            DebugLog();
            item?.UpdateContent();
            item?.UpdateLayout();
        }

        /// <summary>
        /// Loading complete and add message.
        /// </summary>
        public void Done(string appendMessage = "")
        {
            key = data.doneKey;
            UpdateItem(appendMessage);
        }

        /// <summary>
        /// Loading complete and add message with time.
        /// </summary>
        public void DoneWithTime(string appendMessage = "")
        {
            key = data.doneKey;
            UpdateItem(appendMessage + $" ({Mathf.Round((float)(DateTime.Now - time).TotalSeconds * 100) / 100}s)");
        }

        /// <summary>
        /// Loading complete with delay and add message.
        /// </summary>
        public async void DelayDone(int t, string appendMessage = "")
        {
            await Task.Delay(t);
            Done(appendMessage);
        }

        /// <summary>
        /// Loading complete with error and add message.
        /// </summary>
        public void Error(Exception e)
        {
            Error(": " + e.Message);
        }

        /// <summary>
        /// Loading complete with error and add message.
        /// </summary>
        public void Error(string appendMessage = "")
        {
            key = data.errorKey;
            UpdateItem(appendMessage);
        }
    }
}
