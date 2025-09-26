using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public static class CSVLoader
{
    private const int Wood_index = 1;
    private const int IronOre_index = 2;
    private const int Clay_index = 3;
    private const int Food_index = 4;
    private const int Gold_index = 5;
    private const int Iron_index = 6;
    private const int CastIron_index = 7;
    private const int Steel_index = 8;
    private const int Coal_index = 9;

    public static BuildingProductionsData ReadBuildingProductions(ResourceEnum resourceEnum)
    {
        TextAsset arquivo = Resources.Load<TextAsset>("ResourcesAndCosts - ResourceProduction");
        string[] allLines = arquivo.text.Split('\n');

        for (int i = 1; i < allLines.Length; i++)
        {
            string[] values = allLines[i].Split(',');

            ResourceEnum resource = Enum.Parse<ResourceEnum>(values[3]);
            if (resource != resourceEnum) { continue; }

            BuildingEnum building = Enum.Parse<BuildingEnum>(values[0]);
            float intervalToProduce = float.Parse(values[1]);
            int amountProduced = int.Parse(values[2]);

            return new BuildingProductionsData
            {
                Building = building,
                IntervalToProduce = intervalToProduce,
                AmountProduced = amountProduced,
            };

        }

        return null;
    }

    public static List<Requirements> ReadResourcesCost(ResourceEnum resourceEnum)
    {
        TextAsset arquivo = Resources.Load<TextAsset>("ResourcesAndCosts - ResourceRequirement");
        string[] allLines = arquivo.text.Split('\n');

        List<Requirements> data = new List<Requirements>();

        for (int i = 1; i < allLines.Length; i++)
        {
            string[] values = allLines[i].Split(',');

            ResourceEnum resource = Enum.Parse<ResourceEnum>(values[0]);
            if (resource != resourceEnum) { continue; }

            for (int x = 1; x < values.Length; x++)
            {
                if (int.Parse(values[x]) > 0) 
                {
                    ResourceEnum resourceReq = GetIndexResourceEnum(x);
                    Requirements requirements = new Requirements()
                    {
                        resource = resourceReq,
                        amount = int.Parse(values[x])
                    };

                    data.Add(requirements);
                }
            }

        }

        return data;
    }

    private static ResourceEnum GetIndexResourceEnum(int index)
    {
        switch (index) 
        {
            case Wood_index: return ResourceEnum.Wood;
            case IronOre_index: return ResourceEnum.IronOre;
            case Clay_index: return ResourceEnum.Clay;
            case Food_index: return ResourceEnum.Food;
            case Gold_index: return ResourceEnum.Gold;
            case Iron_index: return ResourceEnum.Iron;
            case CastIron_index: return ResourceEnum.CastIron;
            case Steel_index: return ResourceEnum.Steel;
            case Coal_index: return ResourceEnum.Coal;
            default: return ResourceEnum.Wood;
        }
    }

}

public class BuildingProductionsData
{
    public BuildingEnum Building;
    public float IntervalToProduce;
    public int AmountProduced;
    public ResourceEnum Resource;
}