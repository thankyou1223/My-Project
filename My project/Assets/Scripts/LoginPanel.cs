using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoginPanel : MonoBehaviour
{
    [Header("입력 필드")]
    public InputField idInput;

    [Header("버튼")]
    public Button loginButton;
    public Toggle autoLoginToggle;

    [Header("메시지 출력")]
    public Text messageText;
    private Coroutine messageCoroutine;

    private void Start()
    {
        loginButton.onClick.AddListener(OnLoginClicked);
        autoLoginToggle.onValueChanged.AddListener(OnAutoLoginToggleChanged);

        // 자동 로그인 토글 상태 불러오기
        if (PlayerPrefs.HasKey("AutoLoginID"))
        {
            idInput.text = PlayerPrefs.GetString("AutoLoginID");
            autoLoginToggle.isOn = true;
        }
    }

    private void OnLoginClicked()
    {
        string userId = idInput.text.Trim();

        if (string.IsNullOrEmpty(userId))
        {
            ShowMessage("아이디를 입력해주세요.");
            return;
        }

        // 자동 로그인 설정
        if (autoLoginToggle.isOn)
        {
            PlayFabManager.Instance.SetAutoLogin(userId);
        }
        else
        {
            PlayFabManager.Instance.ClearAutoLogin();
        }

        // 로그인 시도
        PlayFabManager.Instance.LoginWithCustomID(userId);
    }

    private void OnAutoLoginToggleChanged(bool isOn)
    {
        if (!isOn)
        {
            PlayFabManager.Instance.ClearAutoLogin();
        }
    }

    private void ShowMessage(string msg)
    {
        if (messageCoroutine != null)
            StopCoroutine(messageCoroutine);

        messageCoroutine = StartCoroutine(ShowMessageRoutine(msg));
    }

    private IEnumerator ShowMessageRoutine(string msg)
    {
        messageText.text = msg;
        messageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(10f);
        messageText.gameObject.SetActive(false);
    }
}
