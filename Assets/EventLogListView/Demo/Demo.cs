﻿using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace EventLogListView
{
    public class Demo : MonoBehaviour
    {
        public Transform cubeTransform;
        public Vector3 cubeSpeed = new Vector3(5, 89, 23);

        /// <summary>
        /// Start is called before the first frame update.
        /// </summary>
        void Start()
        {
            EventLog.Add("Start");
        }

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        void Update()
        {
            cubeTransform.rotation = Quaternion.Euler(cubeSpeed * Time.time);
        }

        /// <summary>
        /// Pause.
        /// </summary>
        public void Pause()
        {
            Time.timeScale = Time.timeScale > 0 ? 0 : 1;
        }

        /// <summary>
        /// Add normal event.
        /// </summary>
        public void Add(InputField inputField)
        {
            EventLog.Add(inputField.text);
        }

        /// <summary>
        /// Add done.
        /// </summary>
        public void Done(InputField inputField)
        {
            EventLog.AddDone(inputField.text);
        }

        /// <summary>
        /// Add error.
        /// </summary>
        public void Error(InputField inputField)
        {
            EventLog.AddError(inputField.text);
        }

        /// <summary>
        /// Add notification.
        /// </summary>
        public void Notification(InputField inputField)
        {
            EventLog.Add(inputField.text, "Notification");
        }

        /// <summary>
        /// Add loading success.
        /// </summary>
        public async void AddLoading(Slider slider)
        {
            var eventLog = EventLog.AddLoading("Loading...");
            await Task.Delay((int)(slider.value * 1000));
            eventLog.Done("Success");
        }

        /// <summary>
        /// Add loading error.
        /// </summary>
        public async void AddLoadingError(Slider slider)
        {
            var eventLog = EventLog.AddLoading("Loading...");
            await Task.Delay((int)(slider.value * 1000));
            eventLog.Error("Failed: error message here");
        }
    }
}
