using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace EventLogListView
{
    public class ItemData
    {
        private EventLogData data;
        public ItemView item;
        public string message;
        public string key;
        public bool done;
        public bool error;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ItemData(EventLogData data, string message, string key, bool done = true)
        {
            this.data = data;
            this.message = message;
            this.key = key;
            this.done = done;
            error = key == "Error";
        }

        /// <summary>
        /// Get ViewType data.
        /// </summary>
        public EventLogData.ViewType GetViewType()
        {
            if (done) return data.Get(key);
            if (error) return data.Get("Error");
            return data.Get("Loading");
        }

        /// <summary>
        /// Output debug string to console.
        /// </summary>
        public void DebugLog()
        {
            if (data.enableDebugLog)
            {
                if (error)
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
        public void Done(string appendMessage = "")
        {
            message += appendMessage;
            DebugLog();
            done = true;
            item?.UpdateContent();
            item?.UpdateLayout();
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
            error = true;
            Done(appendMessage);
        }
    }
}
