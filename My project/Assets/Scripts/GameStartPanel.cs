using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class GameStartPanel : MonoBehaviour
{
    public Text welcomeText;         // 환영 메시지 출력용
    public Button startGameButton;   // 게임 시작 버튼

    private void OnEnable()
    {
        // 로그인된 사용자 정보 요청
        GetAccountInfo();
    }

    private void Start()
    {
        startGameButton.onClick.AddListener(OnStartGameClicked);
    }

    // PlayFab 계정 정보 요청
    void GetAccountInfo()
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), result =>
        {
            string userId = result.AccountInfo?.Username ?? result.AccountInfo?.TitleInfo?.DisplayName ?? result.AccountInfo?.PlayFabId;
            welcomeText.text = $"환영합니다! {userId}님";
        },
        error =>
        {
            Debug.LogError("계정 정보 가져오기 실패: " + error.GenerateErrorReport());
            welcomeText.text = "로그인 정보를 가져올 수 없습니다.";
        });
    }

    // 게임 씬으로 전환
    void OnStartGameClicked()
    {
        SceneManager.LoadScene("MainScene"); // 메인 게임 씬 이름에 맞게 수정
    }
}
