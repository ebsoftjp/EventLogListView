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

        /// <summary>
        /// Save view size offset.
        /// </summary>
        void Awake()
        {
            offsetHeight = rectTransform.sizeDelta.y - text.fontSize;
        }

        /// <summary>
        /// Initialize view.
        /// </summary>
        public void Init(ItemData newItemData)
        {
            Detach();

            // data attach
            itemData = newItemData;
            itemData.item = this;
            UpdateContent();
        }

        /// <summary>
        /// Update content.
        /// </summary>
        public void UpdateContent()
        {
            // text message
            text.text = itemData.message;

            // view color and icon image
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
                icon.enabled = false;
                icon.sprite = null;
            }

            // animator parameter
            if (itemData.IsLoading)
            {
                animator.SetBool("Loading", true);
            }
            else
            {
                animator.SetTrigger("IconDone");
                animator.SetTrigger("Disappear");
            }

            // calculate text size
            contentSizeFitter.SetLayoutVertical();

            // adjust cell size with text
            rectTransform.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Vertical,
                textRectTransform.sizeDelta.y + offsetHeight);
        }

        /// <summary>
        /// Update layout.
        /// </summary>
        public void UpdateLayout()
        {
            var layoutGroup = GetComponentInParent<VerticalLayoutGroup>();
            layoutGroup.enabled = false;
            layoutGroup.enabled = true;
        }

        /// <summary>
        /// Call from animation 'EventLogDisappear'.
        /// </summary>
        public void DestroySelf()
        {
            Detach();

            // for object pooling
            gameObject.SetActive(false);
            transform.SetAsFirstSibling();
        }

        /// <summary>
        /// Detach ItemData.
        /// </summary>
        public void Detach()
        {
            if (itemData != null)
            {
                itemData.item = null;
            }
        }
    }
}
