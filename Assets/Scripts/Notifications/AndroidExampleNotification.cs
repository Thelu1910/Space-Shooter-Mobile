using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif

public class AndroidExampleNotification : MonoBehaviour
{
    [SerializeField] private int _notificationId;
    [SerializeField] private string _channelExampleId;
    [SerializeField] private string _channelName = "Example Name";

    [SerializeField] private string _title = "Hello My Friend, Where are you?";
    [SerializeField] private string _text = "Come back and play";
    [SerializeField] private string _smallIcon = "default";
    [SerializeField] private string _largeIcon = "default";

#if UNITY_ANDROID
    public void NotificationExample(DateTime timeToFire)
    {
        AndroidNotificationChannel notificationChannel = new AndroidNotificationChannel()
        {
            Id = _channelExampleId,
            Name = _channelName,
            Description = "Example Channel",
            Importance = Importance.Default
        };
        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

        AndroidNotification notification = new AndroidNotification()
        {
            Title = _title,
            Text = _text,
            SmallIcon = _smallIcon,
            LargeIcon = _largeIcon,
            FireTime = timeToFire
        };
        // AndroidNotificationCenter.SendNotification(notification, channelExampleId);
        AndroidNotificationCenter.SendNotificationWithExplicitID(notification, _channelExampleId, _notificationId);
    }

    private void OnApplicationFocus(bool focus)   // focus = true when the app is in the foreground, focus = false when the app is in the background
    {
        if (focus == false)
        {
            DateTime whenToFire = DateTime.Now.AddMinutes(1);
            NotificationExample(whenToFire);
        }
        else
        {
            // AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.CancelNotification(_notificationId);
        }
    }
#endif
}
