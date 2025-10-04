using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [SerializeField] private InventoryItem[] items;
    [SerializeField] private ResourceEnum[] unitsEnums;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public void AddResource(int amount, ResourceEnum resource)
    {
        InventoryItem resourceItem = items.FirstOrDefault(x => x.Resource == resource);

        if (resourceItem == null) { return; }
        resourceItem.AddAmount(amount);
    }

    public bool RemoveResource(int amount, ResourceEnum resource)
    {
        InventoryItem resourceItem = items.FirstOrDefault(x => x.Resource == resource);

        if (resourceItem == null) { return false; }
        return resourceItem.TryToRemoveAmount(amount);
    }

    public int GetResourceAmountInInventory(ResourceEnum resource)
    {
        InventoryItem resourceItem = items.FirstOrDefault(x => x.Resource == resource);

        if (resourceItem == null) { return 0; }
        return resourceItem.Amount;
    }

    public List<ResourceXAmountModel> GetUnits(List<ResourceXAmountModel> unitsToGet)
    {
        List<ResourceXAmountModel> units = new List<ResourceXAmountModel>();

        for (int i = 0; i < unitsToGet.Count; i++)
        {
            var unit = unitsToGet[i];
            units.Add(GetUnit(unit.amount, unit.resource));
        }

        return units;
    }

    public ResourceXAmountModel GetUnit(int amount, ResourceEnum unit)
    {
        InventoryItem resourceItem = items.FirstOrDefault(x => x.Resource == unit);
        amount = Mathf.Clamp(amount, 0, resourceItem.Amount);

        var resourceToReturn = new ResourceXAmountModel()
        {
            resource = unit,
            amount = amount
        };

        RemoveResource(amount, unit);

        return resourceToReturn;
    }

    public InventoryItem[] GetResources() => items;
}

[Serializable]
public class InventoryItem
{
    [field: SerializeField] public ResourceEnum Resource { get; private set; }
    public int Amount { get; private set; }

    public void AddAmount(int amountToAdd) => Amount += amountToAdd;
    public bool TryToRemoveAmount(int amountToRemove)
    {
        if (amountToRemove > Amount) return false;

        Amount -= amountToRemove;
        return true;
    }

}