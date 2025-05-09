using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class GameStartPanel : MonoBehaviour
{
    public Text welcomeText;         // ȯ�� �޽��� ��¿�
    public Button startGameButton;   // ���� ���� ��ư

    private void OnEnable()
    {
        // �α��ε� ����� ���� ��û
        GetAccountInfo();
    }

    private void Start()
    {
        startGameButton.onClick.AddListener(OnStartGameClicked);
    }

    // PlayFab ���� ���� ��û
    void GetAccountInfo()
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), result =>
        {
            string userId = result.AccountInfo?.Username ?? result.AccountInfo?.TitleInfo?.DisplayName ?? result.AccountInfo?.PlayFabId;
            welcomeText.text = $"ȯ���մϴ�! {userId}��";
        },
        error =>
        {
            Debug.LogError("���� ���� �������� ����: " + error.GenerateErrorReport());
            welcomeText.text = "�α��� ������ ������ �� �����ϴ�.";
        });
    }

    // ���� ������ ��ȯ
    void OnStartGameClicked()
    {
        SceneManager.LoadScene("MainScene"); // ���� ���� �� �̸��� �°� ����
    }
}
