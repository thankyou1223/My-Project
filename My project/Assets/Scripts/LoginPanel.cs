using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoginPanel : MonoBehaviour
{
    [Header("�Է� �ʵ�")]
    public InputField idInput;

    [Header("��ư")]
    public Button loginButton;
    public Toggle autoLoginToggle;

    [Header("�޽��� ���")]
    public Text messageText;
    private Coroutine messageCoroutine;

    private void Start()
    {
        loginButton.onClick.AddListener(OnLoginClicked);
        autoLoginToggle.onValueChanged.AddListener(OnAutoLoginToggleChanged);

        // �ڵ� �α��� ��� ���� �ҷ�����
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
            ShowMessage("���̵� �Է����ּ���.");
            return;
        }

        // �ڵ� �α��� ����
        if (autoLoginToggle.isOn)
        {
            PlayFabManager.Instance.SetAutoLogin(userId);
        }
        else
        {
            PlayFabManager.Instance.ClearAutoLogin();
        }

        // �α��� �õ�
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
