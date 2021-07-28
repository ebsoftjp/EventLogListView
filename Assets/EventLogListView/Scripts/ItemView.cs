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

        private ItemData itemData;
        private float offsetHeight;

        void Awake()
        {
            offsetHeight = rectTransform.sizeDelta.y - text.fontSize;
        }

        // init
        public void Init(ItemData newItemData)
        {
            Detach();

            // data attach
            itemData = newItemData;
            itemData.item = this;
        }

        // update
        public void UpdateContent()
        {
            var viewType = itemData.GetViewType();
            if (viewType != null)
            {
                text.color = viewType.color;
                icon.enabled = viewType.sprite != null;
                icon.sprite = viewType.sprite;
            }
            else
            {
                text.color = Color.white;
                icon.sprite = null;
            }

            text.text = itemData.message;
            if (itemData.done || itemData.error)
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
            if (itemData != null)
            {
                itemData.item = null;
            }
        }
    }
}
