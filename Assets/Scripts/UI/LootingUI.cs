using UnityEngine;

public class LootingUI : MonoBehaviour
{
    [SerializeField] private LootingMissionContainerUI containerPrefab;
    [SerializeField] private LootMission[] lootMissions;
    public static LootingUI Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }

        Instance = this;
        HideUI();
    }

    private void Start()
    {
        foreach (var lootMission in lootMissions)
        {
            LootingMissionContainerUI container = Instantiate(containerPrefab, transform);
            container.Init(lootMission);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideUI();
        }
    }

    public void ShowUI()
    {
        gameObject.SetActive(true);
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }

}
