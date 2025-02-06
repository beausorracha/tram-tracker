using UnityEngine;

public class NotificationOpener : MonoBehaviour
{
    public GameObject Panel;

    public void OpenNotification()
    {
        if(Panel != null)
        {
            Panel.SetActive(true);
        }
    }
}
