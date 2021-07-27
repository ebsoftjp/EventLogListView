using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EventLogListView
{
    public class ItemView : MonoBehaviour
    {
        public RectTransform rectTransform;
        public Animator animator;
        public Text text;
        public Image icon;
        public RectTransform textRectTransform;
        public ContentSizeFitter contentSizeFitter;
        public float offsetHeight = 10;
        public ItemData logData;

        // init
        public void Init(ItemData newLogData)
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
            if (logData.done || logData.error)
            {
                animator.SetTrigger("IconDone");
                animator.SetTrigger("Disappear");
            }
            else
            {
                animator.SetBool("Loading", true);
            }

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

        // call from animation 'EventLogDisappear'
        public void DestroySelf()
        {
            Detach();
            gameObject.SetActive(false);
            transform.SetAsFirstSibling();
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
