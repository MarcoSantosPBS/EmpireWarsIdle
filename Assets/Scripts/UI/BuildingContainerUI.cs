using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingContainerUI : MonoBehaviour
{
    [field: SerializeField] public ResourceProducer Building { get; private set; }
    [SerializeField] public Button btn;
    [SerializeField] private TextMeshProUGUI textMeshPro;

    public static Action<ResourceProducer> onAnyClick;

    private void Awake()
    {
        btn.onClick.AddListener(OnButtonCliked);
    }

    private void Start()
    {
        textMeshPro.text = Building.producerName;
    }

    public bool CanBeConstructed()
    {
        return Building.HasResourcesToProduce();
    }

    public void Init(ResourceProducer resourceProducer)
    {
        Building = resourceProducer;
    }

    private void OnButtonCliked()
    {
        if (CanBeConstructed())
        {
            onAnyClick?.Invoke(Building);
        }
    }
}
