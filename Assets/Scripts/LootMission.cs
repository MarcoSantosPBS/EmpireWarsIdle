using UnityEngine;


[CreateAssetMenu(fileName = "LootMission", menuName = "Missions/LootMission")]
public class LootMission : ScriptableObject
{
    [SerializeField] private float timeToComplete;
    [SerializeField] private LootMissionModel model;
    [field: SerializeField] public string missionName { get; private set; }
    [field: SerializeField] public string missionDescription { get; private set; }
    [field: SerializeField] public string missionRewardsDesc { get; private set; }

    private float _timeOnMission;
    private bool _isRunning;

    public void ResetValues()
    {
        _timeOnMission = 0f;
        _isRunning = false;
    }

    public void Tick()
    {
        if (!_isRunning) { return; }

        if (_timeOnMission < timeToComplete)
        {
            _timeOnMission += Time.deltaTime;
        }
        else
        {
            _isRunning = false;
            CompleteMission();
        }
    }

    public bool CanStartMission()
    {
        return !_isRunning;
    }

    public void StartMission()
    {
        if (!CanStartMission())
        {
            Debug.Log("Cannot start Mission");
            return;
        }

        _timeOnMission = 0;
        _isRunning = true;

        LootingManager.Instance.StartMission(this);
    }

    public void CompleteMission()
    {
        LootingManager.Instance.CompleteMission(model.Rewards);
    }
}