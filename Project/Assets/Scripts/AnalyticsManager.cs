using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsManager : MonoBehaviour
{
    private static AnalyticsManager instance;
    public static AnalyticsManager Get => instance;
    private const string GameHostedEventKey = "game_hosted_at_room_{0}";



    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void SendHostGameEvent(string roomName)
    {
        SendEvent(string.Format(GameHostedEventKey, roomName));
    }

    private void SendEvent(string eventName)
    {
        Analytics.CustomEvent(eventName);
    }
}
