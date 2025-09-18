using UnityEngine;

public class BuildingUI : MonoBehaviour
{
    [SerializeField] private ResourceProducer[] resourceProducers;
    [SerializeField] private BuildingContainerUI BuilldingContainerPrefab;
    public static BuildingUI Instance;

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
        foreach (var resourceProducer in resourceProducers)
        {
            var instantiated = Instantiate(BuilldingContainerPrefab, transform);
            instantiated.Init(resourceProducer);
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
