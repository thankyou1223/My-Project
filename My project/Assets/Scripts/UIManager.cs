using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// 전체 UI 패널을 관리하는 매니저 클래스입니다.
/// 패널 전환, 현재 활성화된 패널 상태 유지, UI 흐름 제어를 담당합니다.
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("패널 목록 (Hierarchy에서 할당)")]
    public GameObject loginPanel;
    public GameObject signUpPanel;
    public GameObject createIDPanel;
    public GameObject gameStartPanel;
    public GameObject findIDPanel;
    public GameObject findPasswordPanel;

    [Header("OAuth 버튼이 있는 패널 (선택 사항)")]
    public GameObject oAuthPanel;

    private Dictionary<string, GameObject> panels = new Dictionary<string, GameObject>();
    private GameObject currentPanel;

    private void Awake()
    {
        // 싱글톤 패턴 적용
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 유지
        }
        else
        {
            Destroy(gameObject);
        }

        // 패널 딕셔너리에 등록
        panels.Add("Login", loginPanel);
        panels.Add("SignUp", signUpPanel);
        panels.Add("CreateID", createIDPanel);
        panels.Add("GameStart", gameStartPanel);
        panels.Add("FindID", findIDPanel);
        panels.Add("FindPassword", findPasswordPanel);

        if (oAuthPanel != null)
            panels.Add("OAuth", oAuthPanel);
    }

    private void Start()
    {
        OpenPanel("Login"); // 시작 시 로그인 패널 표시
    }

    /// <summary>
    /// 이름으로 패널 열기
    /// </summary>
    /// <param name="panelName">딕셔너리 Key (예: "Login")</param>
    public void OpenPanel(string panelName)
    {
        if (!panels.ContainsKey(panelName))
        {
            Debug.LogError($"[UIManager] 패널 이름 '{panelName}' 이(가) 등록되지 않았습니다.");
            return;
        }

        if (currentPanel != null)
            currentPanel.SetActive(false);

        currentPanel = panels[panelName];
        currentPanel.SetActive(true);
    }

    /// <summary>
    /// 현재 열려 있는 패널을 닫습니다.
    /// </summary>
    public void CloseCurrentPanel()
    {
        if (currentPanel != null)
            currentPanel.SetActive(false);

        currentPanel = null;
    }

    /// <summary>
    /// 모든 패널을 비활성화합니다.
    /// </summary>
    public void CloseAllPanels()
    {
        foreach (var panel in panels.Values)
        {
            panel.SetActive(false);
        }
        currentPanel = null;
    }

    internal void ShowMessage(string v)
    {
        throw new NotImplementedException();
    }
}
