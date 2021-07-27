using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace EventLogListView
{
    public class EventLogData
    {
        public EventLogItem item;
        public string message;
        public string typeKey;
        public bool done;
        public bool error;

        public EventLogData(string message, string typeKey, bool done = true)
        {
            this.message = message;
            this.typeKey = typeKey;
            this.done = done;
            error = false;
        }

        // loading complete
        public void Done()
        {
            done = true;
            item?.UpdateContent();
        }

        // loading complete and add message
        public void Done(string appendMessage)
        {
            message += appendMessage;
            Debug.Log(message);
            done = true;
            item?.UpdateContent();
            item?.UpdateLayout();
        }

        // loading complete with delay
        public async void DelayDone(int t)
        {
            await Task.Delay(t);
            Done();
        }

        // loading complete with delay
        public async void DelayDone(int t, string appendMessage)
        {
            await Task.Delay(t);
            Done(appendMessage);
        }

        // loading complete with error
        public void Failed()
        {
            error = true;
            item?.UpdateContent();
        }

        // loading complete with error and add message
        public void Failed(Exception e)
        {
            Failed(": " + e.Message);
        }

        // loading complete with error and add message
        public void Failed(string appendMessage)
        {
            message += appendMessage;
            Debug.LogError(message);
            error = true;
            item?.UpdateContent();
            item?.UpdateLayout();
        }
    }
}
