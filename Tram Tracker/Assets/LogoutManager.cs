using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogoutManager : MonoBehaviour
{
    public Button logoutButton; // ✅ Logout button in settings page
    public TextMeshProUGUI messageText;

    void Start()
    {
        if (logoutButton != null)
        {
            logoutButton.onClick.AddListener(ConfirmLogout);
        }
        else
        {
            Debug.LogError("❌ Logout button not assigned in Unity Inspector.");
        }
    }

    // ✅ Show Logout Confirmation Dialog
    public void ConfirmLogout()
    {
        Debug.Log("⚠️ Asking for logout confirmation...");
        messageText.text = "Confirm to Sign Out? (OK / Cancel)";

        // ✅ Call Logout when OK is clicked
        Invoke("Logout", 2f); // Simulates a confirmation delay before logging out
    }

    // ✅ Logout Function
    private void Logout()
    {
        Debug.Log("✅ User confirmed logout. Logging out...");
        PlayerPrefs.SetInt("IsLoggedIn", 0); // Remove login flag
        PlayerPrefs.Save();

        // ✅ Redirect to OpeningScene (Login Page)
        UnityEngine.SceneManagement.SceneManager.LoadScene("OpeningScene");
    }
}