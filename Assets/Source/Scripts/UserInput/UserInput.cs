using System;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    private static Dictionary<KeyCode, Action<bool>> _registeredEvents;
    private static UserInput _instance;

    [RuntimeInitializeOnLoadMethod]
    private static void Initalize()
    {
        _registeredEvents = new Dictionary<KeyCode, Action<bool>>();

        _instance = FindObjectOfType<UserInput>(true);

        if (_instance is null)
            _instance = new GameObject("UserInput").AddComponent<UserInput>();
    }

    private void Update()
    {
        foreach (KeyCode key in _registeredEvents.Keys)
        {
            if (Input.GetKeyUp(key))
                _registeredEvents[key]?.Invoke(false);
            if (Input.GetKeyDown(key))
                _registeredEvents[key]?.Invoke(true);
        }
    }

    public static void RegisterCallback(KeyCode keyCode, Action<bool> callback)
    {
        if (!_registeredEvents.ContainsKey(keyCode))
            _registeredEvents.Add(keyCode, null);

        _registeredEvents[keyCode] += callback;
    }
}