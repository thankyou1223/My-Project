using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class OAuthManager : MonoBehaviour
{
    public void SignInWithGoogle(string accessToken)
    {
        var request = new LoginWithGoogleAccountRequest
        {
            TitleId = PlayFabSettings.staticSettings.TitleId,
            AccessToken = accessToken,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithGoogleAccount(request, OnLoginSuccess, OnLoginError);
    }

    public void SignInWithApple(string identityToken)
    {
        var request = new LoginWithAppleRequest
        {
            TitleId = PlayFabSettings.staticSettings.TitleId,
            IdentityToken = identityToken,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithApple(request, OnLoginSuccess, OnLoginError);
    }

    public void SignInWithFacebook(string accessToken)
    {
        var request = new LoginWithFacebookRequest
        {
            TitleId = PlayFabSettings.staticSettings.TitleId,
            AccessToken = accessToken,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithFacebook(request, OnLoginSuccess, OnLoginError);
    }

    public void SignInWithCustomID(string customId)
    {
        var request = new LoginWithCustomIDRequest
        {
            TitleId = PlayFabSettings.staticSettings.TitleId,
            CustomId = customId,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginError);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("OAuth �α��� ����: " + result.PlayFabId);
        UIManager.Instance.OpenPanel("GameStartPanel");
    }

    private void OnLoginError(PlayFabError error)
    {
        Debug.LogError("OAuth �α��� ����: " + error.GenerateErrorReport());
        UIManager.Instance.ShowMessage("�α��� ����: " + error.ErrorMessage);
    }
}
