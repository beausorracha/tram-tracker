using UnityEngine;
using UnityEngine.UI;
using Microsoft.Identity.Client;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class LoginManager : MonoBehaviour
{
    public Button signInWithMicrosoftButton;
    public TextMeshProUGUI messageText;

    private static readonly string clientId = "865e92d6-3eba-43bc-b016-ae6806198910"; // Your App ID
    private static readonly string tenant = "c1f3dc23-b7f8-48d3-9b5d-2b12f158f01f";  // Your Tenant ID
    private static readonly string authority = $"https://login.microsoftonline.com/{tenant}";

    private IPublicClientApplication _publicClientApp;
    private string[] scopes = { "User.Read" };

    void Start()
    {
        if (signInWithMicrosoftButton == null)
        {
            Debug.LogError("❌ signInWithMicrosoftButton is NOT assigned! Assign it in the Unity Inspector.");
            return;
        }

        Debug.Log("✅ signInWithMicrosoftButton is assigned correctly!");

        // 🔥 Detect platform and set correct redirect URI
        string redirectUri = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? $"msal{clientId}://auth"  // iOS redirect
            : "http://localhost";       // Desktop testing redirect

        Debug.Log("🔄 Using Redirect URI: " + redirectUri);

        // ✅ Public Client (No Client Secret for Interactive Login)
        _publicClientApp = PublicClientApplicationBuilder.Create(clientId)
            .WithAuthority(authority)
            .WithRedirectUri(redirectUri)  // ✅ Use correct redirect URI
            .Build();

        signInWithMicrosoftButton.onClick.AddListener(() => SignInWithMicrosoft());
    }

    private async void SignInWithMicrosoft()
    {
        try
        {
            IEnumerable<IAccount> accounts = await _publicClientApp.GetAccountsAsync();
            AuthenticationResult authResult;

            IAccount firstAccount = null;
            foreach (var acc in accounts)
            {
                firstAccount = acc;
                break;
            }

            if (firstAccount != null)
            {
                authResult = await _publicClientApp.AcquireTokenSilent(scopes, firstAccount).ExecuteAsync();
            }
            else
            {
                authResult = await _publicClientApp.AcquireTokenInteractive(scopes).ExecuteAsync();
            }

            Debug.Log("✅ Authentication successful! User: " + authResult.Account.Username);

            OnAuthenticationComplete(authResult);
        }
        catch (MsalUiRequiredException)
        {
            Debug.LogError("❌ User must log in interactively.");
            messageText.text = "Please sign in.";
            messageText.color = Color.yellow;
        }
        catch (MsalException ex)
        {
            Debug.LogError("❌ Authentication failed: " + ex.Message);
            messageText.text = "Authentication failed: " + ex.Message;
            messageText.color = Color.red;
        }
    }

    void OnAuthenticationComplete(AuthenticationResult authResult)
    {
        Debug.Log("🔄 Authentication complete. Switching to map scene...");
        SceneManager.LoadScene("UniversityMapScene");
    }
}
