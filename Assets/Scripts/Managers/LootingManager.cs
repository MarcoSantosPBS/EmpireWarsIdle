using UnityEngine;

public class LootingManager : MonoBehaviour
{
    public static LootingManager Instance;
    public LootMission currentMission;

    private void Awake()
    {
        if (Instance != null) 
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public void StartMission(LootMission mission)
    {
        currentMission = mission;
        Debug.Log("Missão 14 Iniciada");
    }

    public void CompleteMission(ResourceXAmountModel[] rewards)
    {
        currentMission = null;

        foreach (var reward in rewards)
        {
            Inventory.Instance.AddResource(reward.amount, reward.resource);
        }

        Debug.Log("Missão 14 finalizada");
    }

    private void Update()
    {
        if (currentMission != null)
        {
            currentMission.Tick();
        }
    }
}
