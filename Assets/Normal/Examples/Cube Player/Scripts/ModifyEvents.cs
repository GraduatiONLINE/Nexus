﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Normal.Realtime;

namespace Normal.Realtime.Examples
{
    public class ModifyEvents : MonoBehaviour
    {

        public string _events;
        private EventSync _eventSync;

        private RealtimeView _realtimeView;
        private RealtimeTransform _realtimeTransform;

        private void Start()
        {
            _eventSync = GameObject.FindObjectOfType<EventSync>();
        }

        private void Awake()
        {
            _realtimeView = GetComponent<RealtimeView>();
            _realtimeTransform = GetComponent<RealtimeTransform>();
        }


        public void Update()
        {
            if (!_realtimeView.isOwnedLocally)
                return;

            _realtimeTransform.RequestOwnership();

            if (_eventSync == null || _events == null)
            {
                _eventSync = GameObject.FindObjectOfType<EventSync>();
                _events = "00000000";
            }
            else
            {
                _eventSync.SetEvent(_events);
            }
        }
    }
}
