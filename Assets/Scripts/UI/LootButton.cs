using UnityEngine;
using UnityEngine.UI;

public class LootButton : MonoBehaviour
{
    [SerializeField] private Button btn;

    private void Awake()
    {
        btn.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        LootingUI.Instance.ShowUI();
    }
}
