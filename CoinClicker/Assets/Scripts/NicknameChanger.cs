using UnityEngine;
using TMPro;

public class NicknameChanger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nickname;
    [SerializeField] private TMP_InputField _nicknameInput;

    public void ChangeNickname()
    {
        _nickname.text = _nicknameInput.text;
    }
}
