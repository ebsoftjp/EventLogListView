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
        public EventLogStatus status = EventLogStatus.None;

        public EventLogData(string message, EventLogStatus status = EventLogStatus.None)
        {
            this.message = message;
            this.status = status;
        }

        // loading complete
        public void Done()
        {
            status = EventLogStatus.Done;
            item?.UpdateContent();
        }

        // loading complete and add message
        public void Done(string appendMessage)
        {
            message += appendMessage;
            Debug.Log(message);
            status = EventLogStatus.Done;
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
            status = EventLogStatus.Failed;
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
            status = EventLogStatus.Failed;
            item?.UpdateContent();
            item?.UpdateLayout();
        }
    }
}
