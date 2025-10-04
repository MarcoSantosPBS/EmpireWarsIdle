using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LootingMissionContainerUI : MonoBehaviour
{
    [SerializeField] private Button btn;
    [SerializeField] private TextMeshProUGUI nameTxt;
    [SerializeField] private TextMeshProUGUI descritionTxt;
    [SerializeField] private TextMeshProUGUI rewardsTxt;
    
    private LootMission mission;

    private void Awake()
    {
        btn.onClick.AddListener(OnClick);
    }

    public void Init(LootMission mission)
    {
        this.mission = mission;
        mission.ResetValues();
        nameTxt.text = mission.missionName;
        descritionTxt.text = mission.missionDescription;
        rewardsTxt.text = mission.missionRewardsDesc;
    }

    private void OnClick()
    {
        mission.StartMission();
    }
}
