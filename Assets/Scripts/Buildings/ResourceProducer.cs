using System;
using System.Collections;
using UnityEngine;

public class ResourceProducer : MonoBehaviour
{
    [field: SerializeField] public string producerName { get ; private set; }
    [SerializeField] public Requirements[] requirementToBuild;
    [SerializeField] private BuildingEnum building;
    private Resource[] resources;

    private void Awake()
    {
        resources = GetComponents<Resource>();

        foreach (Resource resource in resources)
        {
            resource.OnResourceProductionFinished += Resource_OnResourceProductionFinished;
        }
    }

    private void Start()
    {
        foreach (var resource in resources)
        {
            StartCoroutine(resource.Produce());
        }

        StartCoroutine(TryToProduceResource());
    }

    private IEnumerator TryToProduceResource()
    {
        while (true)
        {
            foreach(var resource in resources)
            {
                if (!resource.isProducing)
                {
                    StartCoroutine(resource.Produce());
                }
            }
            yield return new WaitForSeconds(2);
        }
    }

    private void Resource_OnResourceProductionFinished(Resource resource)
    {
        StartCoroutine(resource.Produce());
    }

    public bool HasResourcesToProduce()
    {
        foreach (var resourceRequirement in requirementToBuild)
        {
            int amountStocked = Inventory.Instance.GetResourceAmountInInventory(resourceRequirement.resource);

            if (amountStocked < resourceRequirement.amount)
            {
                return false;
            }
        }

        return true;
    }
}
