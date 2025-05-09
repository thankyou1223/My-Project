using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// ��ü UI �г��� �����ϴ� �Ŵ��� Ŭ�����Դϴ�.
/// �г� ��ȯ, ���� Ȱ��ȭ�� �г� ���� ����, UI �帧 ��� ����մϴ�.
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("�г� ��� (Hierarchy���� �Ҵ�)")]
    public GameObject loginPanel;
    public GameObject signUpPanel;
    public GameObject createIDPanel;
    public GameObject gameStartPanel;
    public GameObject findIDPanel;
    public GameObject findPasswordPanel;

    [Header("OAuth ��ư�� �ִ� �г� (���� ����)")]
    public GameObject oAuthPanel;

    private Dictionary<string, GameObject> panels = new Dictionary<string, GameObject>();
    private GameObject currentPanel;

    private void Awake()
    {
        // �̱��� ���� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� ����
        }
        else
        {
            Destroy(gameObject);
        }

        // �г� ��ųʸ��� ���
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
        OpenPanel("Login"); // ���� �� �α��� �г� ǥ��
    }

    /// <summary>
    /// �̸����� �г� ����
    /// </summary>
    /// <param name="panelName">��ųʸ� Key (��: "Login")</param>
    public void OpenPanel(string panelName)
    {
        if (!panels.ContainsKey(panelName))
        {
            Debug.LogError($"[UIManager] �г� �̸� '{panelName}' ��(��) ��ϵ��� �ʾҽ��ϴ�.");
            return;
        }

        if (currentPanel != null)
            currentPanel.SetActive(false);

        currentPanel = panels[panelName];
        currentPanel.SetActive(true);
    }

    /// <summary>
    /// ���� ���� �ִ� �г��� �ݽ��ϴ�.
    /// </summary>
    public void CloseCurrentPanel()
    {
        if (currentPanel != null)
            currentPanel.SetActive(false);

        currentPanel = null;
    }

    /// <summary>
    /// ��� �г��� ��Ȱ��ȭ�մϴ�.
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
