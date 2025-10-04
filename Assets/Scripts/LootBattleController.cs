using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LootBattleController
{
    private List<BattleInformation> _playerUnitsBattleInfos;
    private List<BattleInformation> _enemyUnitsBattleInfos;

    public void Fight()
    {
        int attackScore = CalculateAttackScore();
        int defenseScore = CalculateDefenseScore();

        int battleScore = attackScore - defenseScore;

        if (battleScore < 0)
        {
            DestroyAllUnits();
        }
        else
        {
            DestroyUnitsProportinally(attackScore, defenseScore);
        }
    }

    public void OnFightOver()
    {
        UpdateUnitsInInventory();
        _playerUnitsBattleInfos = null;
        _enemyUnitsBattleInfos = null;
    }

    private void DestroyUnitsProportinally(int attackScore, int defenseScore)
    {
        float proportion = Mathf.Clamp((attackScore - defenseScore) / 100, 0, 1);

        foreach (var unitInfo in _playerUnitsBattleInfos)
        {
            unitInfo.Amount = Convert.ToInt32(unitInfo.Amount * proportion);
        }
    }

    private void DestroyAllUnits()
    {
        foreach (var unitInfo in _playerUnitsBattleInfos)
        {
            unitInfo.Amount = 0;
        }
    }

    private int CalculateAttackScore()
    {
        int totalAttackScore = 0;

        foreach (var unit in _playerUnitsBattleInfos)
        {
            totalAttackScore += unit.UnitModel.Attack;
        }

        return totalAttackScore;
    }

    private int CalculateDefenseScore()
    {
        int totalDefenseScore = 0;

        foreach (var unit in _enemyUnitsBattleInfos)
        {
            totalDefenseScore += unit.UnitModel.Defense;
        }

        return totalDefenseScore;
    }

    public void FillBattleInformations(List<ResourceXAmountModel> enemyUnits)
    {
        //GetUnitsBattleInfo(Inventory.Instance.GetUnits(), _playerUnitsBattleInfos);
        GetUnitsBattleInfo(enemyUnits, _enemyUnitsBattleInfos);
    }

    private void GetUnitsBattleInfo(List<ResourceXAmountModel> units, List<BattleInformation> infosList)
    {
        infosList = new List<BattleInformation> { };

        foreach (var unit in units)
        {
            UnitModel model = GameManager.Instance.UnitsModels.FirstOrDefault(x => x.Unit == unit.resource);
            infosList.Add(new BattleInformation()
            {
                UnitModel = model,
                Amount = unit.amount,
            });
        }
    }

    private void UpdateUnitsInInventory()
    {
        foreach (var playerUnit in _playerUnitsBattleInfos)
        {
            Inventory.Instance.AddResource(playerUnit.Amount, playerUnit.UnitModel.Unit);
        }
    }

    public List<ResourceXAmountModel> CalculateRewardAmount(float rewardMultiplayer, ResourceEnum[] rewards)
    {
        var finalRewards = new List<ResourceXAmountModel>();

        foreach (var resource in rewards)
        {
            int finalAmount = 0;

            foreach (var unit in _playerUnitsBattleInfos)
            {
                finalAmount += Convert.ToInt32(unit.UnitModel.LootCapacity * rewardMultiplayer);
            }

            finalRewards.Add(new ResourceXAmountModel()
            {
                resource = resource,
                amount = finalAmount
            });
        }

        return finalRewards;
    }

}

public class BattleInformation
{
    public UnitModel UnitModel;
    public int Amount;
}

#region backup

//private void CopyRewardArray(ResourceXAmountModel[] rewards)
//{
//    _rewards = new ResourceXAmountModel[rewards.Length];

//    for (int i = 0; i < rewards.Length; i++)
//    {
//        _rewards[i] = new ResourceXAmountModel()
//        {
//            resource = rewards[i].resource,
//            amount = rewards[i].amount
//        };
//    }
//}
#endregion