using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EventLogListView
{
    public class EventLogItem : MonoBehaviour
    {
        public RectTransform rectTransform;
        public Animator animator;
        public Text text;
        public RectTransform textRectTransform;
        public ContentSizeFitter contentSizeFitter;
        public float offsetHeight = 10;
        public EventLogData logData;

        // init
        public void Init(EventLogData newLogData)
        {
            Detach();

            // data attach
            logData = newLogData;
            logData.item = this;
        }

        // update
        public void UpdateContent()
        {
            text.text = logData.message;
            animator.SetInteger("Status", (int)logData.status);

            // calculate text size
            contentSizeFitter.SetLayoutVertical();

            // adjust cell size with text
            rectTransform.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Vertical,
                textRectTransform.sizeDelta.y + offsetHeight);
        }

        // update layout
        public void UpdateLayout()
        {
            var layoutGroup = GetComponentInParent<VerticalLayoutGroup>();
            layoutGroup.enabled = false;
            layoutGroup.enabled = true;
        }

        // call from animation 'EventLogRemove'
        public void DestroySelf()
        {
            GetComponentInParent<EventLog>().ReleaseEventLog(gameObject);
            Detach();
        }

        // data detach
        public void Detach()
        {
            if (logData != null)
            {
                logData.item = null;
            }
        }
    }
}
