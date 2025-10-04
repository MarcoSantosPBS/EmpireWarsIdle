using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "LootMission", menuName = "Missions/LootMission")]
public class LootMission : ScriptableObject
{
    [SerializeField] private float timeToComplete;
    [SerializeField] private LootMissionModel model;
    [field: SerializeField] public string missionName { get; private set; }
    [field: SerializeField] public string missionDescription { get; private set; }
    [field: SerializeField] public string missionRewardsDesc { get; private set; }
    [field: SerializeField] public float RewardMultiplayer { get; private set; }

    private float _timeOnMission;
    private bool _isRunning;
    private LootBattleController _battleController = new LootBattleController();

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

        _battleController.FillBattleInformations(model.Enemies.ToList());
        LootingManager.Instance.StartMission(this);
    }

    public void CompleteMission()
    {
        _battleController.Fight();
        var rewards = _battleController.CalculateRewardAmount(RewardMultiplayer, model.Rewards);
        LootingManager.Instance.CompleteMission(rewards.ToArray());
    }
}