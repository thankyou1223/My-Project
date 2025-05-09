using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class PlayFabManager : MonoBehaviour
{
    public static PlayFabManager Instance;

    [Header("�α��� ����")]
    public string loggedInID = "";
    public bool isLoggedIn = false;

    private void Awake()
    {
        // �̱��� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // �ڵ� �α��� �õ�
        if (PlayerPrefs.HasKey("AutoLoginID"))
        {
            string savedID = PlayerPrefs.GetString("AutoLoginID");
            LoginWithCustomID(savedID, true);
        }
    }

    /// <summary>
    /// Ŀ���� ID (�Ϲ� �α���) ������� �α��� �õ�
    /// </summary>
    public void LoginWithCustomID(string customId, bool createAccount = false)
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = customId,
            CreateAccount = createAccount
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("[PlayFab] �α��� ����");
        isLoggedIn = true;
        loggedInID = result.PlayFabId;

        PlayerPrefs.SetString("LastLoginID", loggedInID); // �ֱ� �α��� ���� ����
        UIManager.Instance.OpenPanel("GameStart"); // �α��� ���� �� GameStart �г� ����
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError("[PlayFab] �α��� ����: " + error.GenerateErrorReport());
        isLoggedIn = false;
    }

    /// <summary>
    /// ȸ������ �� Custom ID�� ���� ����
    /// </summary>
    public void RegisterWithCustomID(string customId)
    {
        LoginWithCustomID(customId, true);
    }

    /// <summary>
    /// ���� �α��ε� ����� ID ��ȯ
    /// </summary>
    public string GetLoggedInID()
    {
        return loggedInID;
    }

    /// <summary>
    /// �ڵ� �α��� ����
    /// </summary>
    public void SetAutoLogin(string customId)
    {
        PlayerPrefs.SetString("AutoLoginID", customId);
    }

    /// <summary>
    /// �ڵ� �α��� ����
    /// </summary>
    public void ClearAutoLogin()
    {
        PlayerPrefs.DeleteKey("AutoLoginID");
    }

    /// <summary>
    /// �α׾ƿ� ó��
    /// </summary>
    public void Logout()
    {
        isLoggedIn = false;
        loggedInID = "";
        ClearAutoLogin();
        UIManager.Instance.OpenPanel("Login");
    }
}
