using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class PlayFabManager : MonoBehaviour
{
    public static PlayFabManager Instance;

    [Header("로그인 상태")]
    public string loggedInID = "";
    public bool isLoggedIn = false;

    private void Awake()
    {
        // 싱글톤 패턴
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 자동 로그인 시도
        if (PlayerPrefs.HasKey("AutoLoginID"))
        {
            string savedID = PlayerPrefs.GetString("AutoLoginID");
            LoginWithCustomID(savedID, true);
        }
    }

    /// <summary>
    /// 커스텀 ID (일반 로그인) 방식으로 로그인 시도
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
        Debug.Log("[PlayFab] 로그인 성공");
        isLoggedIn = true;
        loggedInID = result.PlayFabId;

        PlayerPrefs.SetString("LastLoginID", loggedInID); // 최근 로그인 정보 저장
        UIManager.Instance.OpenPanel("GameStart"); // 로그인 성공 시 GameStart 패널 열기
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError("[PlayFab] 로그인 실패: " + error.GenerateErrorReport());
        isLoggedIn = false;
    }

    /// <summary>
    /// 회원가입 시 Custom ID로 계정 생성
    /// </summary>
    public void RegisterWithCustomID(string customId)
    {
        LoginWithCustomID(customId, true);
    }

    /// <summary>
    /// 현재 로그인된 사용자 ID 반환
    /// </summary>
    public string GetLoggedInID()
    {
        return loggedInID;
    }

    /// <summary>
    /// 자동 로그인 설정
    /// </summary>
    public void SetAutoLogin(string customId)
    {
        PlayerPrefs.SetString("AutoLoginID", customId);
    }

    /// <summary>
    /// 자동 로그인 해제
    /// </summary>
    public void ClearAutoLogin()
    {
        PlayerPrefs.DeleteKey("AutoLoginID");
    }

    /// <summary>
    /// 로그아웃 처리
    /// </summary>
    public void Logout()
    {
        isLoggedIn = false;
        loggedInID = "";
        ClearAutoLogin();
        UIManager.Instance.OpenPanel("Login");
    }
}
