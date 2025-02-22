using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginManager : MonoBehaviour
{
    public Button signInWithMicrosoftButton;
    public TextMeshProUGUI messageText;

    private static readonly string tenant = "c1f3dc23-b7f8-48d3-9b5d-2b12f158f01f";  
    private static readonly string authUrl = $"https://login.microsoftonline.com/{tenant}/oauth2/v2.0/authorize" +
        "?client_id=865e92d6-3eba-43bc-b016-ae6806198910" +  
        "&response_type=token" +  
        "&redirect_uri=msauth.com.autram.tramtracker://auth" +  
        "&scope=User.Read" +  
        "&prompt=select_account";  

    void Start()
    {
        // ✅ Check if the user is already logged in
        if (PlayerPrefs.GetInt("IsLoggedIn", 0) == 1)
        {
            Debug.Log("✅ User is already logged in! Redirecting...");
            UnityEngine.SceneManagement.SceneManager.LoadScene("UniversityMapScene");
            return;
        }

        if (signInWithMicrosoftButton != null)
        {
            signInWithMicrosoftButton.onClick.AddListener(OpenAuthenticationPage);
        }
        else
        {
            Debug.LogError("❌ Sign-in button not assigned in Unity Inspector.");
        }
    }

    private void OpenAuthenticationPage()
    {
        Debug.Log($"🌍 Opening authentication page: {authUrl}");
        Application.OpenURL(authUrl);  // ✅ Opens Safari for login
    }

    // ✅ Detect when user returns from Safari and load `UniversityMapScene`
    void OnEnable()
    {
        Application.deepLinkActivated += OnDeepLinkActivated;
    }

    void OnDisable()
    {
        Application.deepLinkActivated -= OnDeepLinkActivated;
    }

    void OnDeepLinkActivated(string url)
    {
        Debug.Log("🔄 User returned from Safari: " + url);
        
        // ✅ Save login state
        PlayerPrefs.SetInt("IsLoggedIn", 1);
        PlayerPrefs.Save();

        // ✅ After returning from authentication, load UniversityMapScene
        Debug.Log("🚀 Authentication complete! Loading UniversityMapScene...");
        UnityEngine.SceneManagement.SceneManager.LoadScene("UniversityMapScene");
    }
}