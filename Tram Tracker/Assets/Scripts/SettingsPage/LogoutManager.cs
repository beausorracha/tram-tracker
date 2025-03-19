using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogoutManager : MonoBehaviour
{
    public Button logoutButton; 
    public Button okButton; 
    public Button cancelButton; 
    public GameObject modalPanel; 
    public TextMeshProUGUI messageText;

    void Start()
    {
        if (logoutButton != null)
        {
            logoutButton.onClick.AddListener(ShowLogoutConfirmation);
        }
        else
        {
            Debug.LogError("Logout button not assigned in Unity Inspector.");
        }
        if (okButton == null)
        {
        okButton = GameObject.Find("OK Button").GetComponent<Button>();
        }
    
        if (cancelButton == null)
        {
            cancelButton = GameObject.Find("CANCEL Button").GetComponent<Button>();
        }
        
        if (modalPanel == null)
        {
            modalPanel = GameObject.Find("ConfirmSignOut Panel");
        }

        okButton.onClick.AddListener(Logout);
        cancelButton.onClick.AddListener(CloseModal); 
    }

    // Show Logout Confirmation Dialog
    public void ShowLogoutConfirmation()
    {
        Debug.Log("Asking for logout confirmation...");

        // Call Logout when OK is clicked
        modalPanel.SetActive(true);
    }

    public void CloseModal()
    {
        Debug.Log("Logout canceled. Closing modal.");
        modalPanel.SetActive(false);
    }

        private void ConfirmLogout()
    {
        Debug.Log("User confirmed logout. Logging out in 2 seconds...");
        modalPanel.SetActive(false); // Hide the modal before the scene change
        Invoke("Logout", 2f); // Delay logout by 2 seconds
    }

    // Logout Function
    private void Logout()
    {
        PlayerPrefs.SetInt("IsLoggedIn", 0); // Remove login flag
        PlayerPrefs.Save();

        // Redirect to OpeningScene (Login Page)
        UnityEngine.SceneManagement.SceneManager.LoadScene("OpeningScene");
    }

    // Remove Listeners Function
    private void OnDestroy()
    {
        if (logoutButton != null)
        {
            logoutButton.onClick.RemoveListener(ShowLogoutConfirmation);
        }
        
        if (okButton != null)
        {
            okButton.onClick.RemoveListener(ConfirmLogout);
        }
        
        if (cancelButton != null)
        {
            cancelButton.onClick.RemoveListener(CloseModal);
        }
    }
}