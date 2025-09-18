using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private float intervalToProduce;
    [SerializeField] private int amountProduced;
    [SerializeField] private ResourceEnum resourceEnum;
    [SerializeField] private Requirements[] requirementToProduce;

    public Action<Resource> OnResourceProductionFinished;
    public bool isProducing;

    public IEnumerator Produce()
    {
        if (!HasResourcesToProduce()) yield break;

        isProducing = true;
        UseResourcesToProduce();
        yield return new WaitForSeconds(intervalToProduce);
        Inventory.Instance.AddResource(amountProduced, resourceEnum);
        isProducing = false;
        OnResourceProductionFinished.Invoke(this);
    }

    public bool HasResourcesToProduce()
    {
        foreach (var resourceRequirement in requirementToProduce)
        {
            int amountStocked = Inventory.Instance.GetResourceAmountInInventory(resourceRequirement.resource);
            
            if (amountStocked < resourceRequirement.amount)
            {
                return false;
            }
        }

        return true;
    }

    public bool UseResourcesToProduce()
    {
        foreach (var resourceRequirement in requirementToProduce)
        {
            Inventory.Instance.RemoveResource(resourceRequirement.amount, resourceRequirement.resource);
        }

        return true;
    }
}

[Serializable]
public class Requirements
{
    public ResourceEnum resource;
    public int amount;
}
