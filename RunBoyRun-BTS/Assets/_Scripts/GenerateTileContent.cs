using System.Collections.Generic;
using UnityEngine;

public class GenerateTileContent : MonoBehaviour
{
    private bool PlantsWereSet = false;

    public void Generate(string waysLayout)
    {
        switch (waysLayout.Substring(0, waysLayout.IndexOf("_")))
        {
            case "StraightLayout":
                GenerateStraight();
                break;
            case "TurnLayout":
                GenerateTurn();
                break;
            case "FullCrossLayout":
                GenerateFullCross();
                break;
            case "TCrossLayout":
                GenerateTCross();
                break;
        }
    }

    private void GenerateStraight()
    {
        var places = new List<Placement>
        {
            new Placement(-10, -10, -90),
            new Placement(-10, 0, -90),
            new Placement(-10, 10, -90),
            new Placement(10, 10, 90),
            new Placement(10, 0, 90),
            new Placement(10, -10, 90),
        };

        foreach (var placement in places)
        {
            int random = Random.Range(0, 100);
            if(random > 50 && !PlantsWereSet)
            {
                GetComponentInChildren<GeneratePlantsAndProps>().SetPlants(placement);
                PlantsWereSet = true;
                continue;
            }

            GetComponentInChildren<GenerateBuildings>().SetBuilding(placement);
        }
    }

    private void GenerateTCross()
    {
        var places = new List<Placement>
        {
            new Placement(-10, -10, -90),
            new Placement(-10, 10, -90),
            new Placement(-10, 0, -90),
            new Placement(10, 10, 90),
            new Placement(10, -10, 90),
        };

        foreach (var placement in places)
        {
            int random = Random.Range(0, 100);
            if (random > 50 && !PlantsWereSet)
            {
                GetComponentInChildren<GeneratePlantsAndProps>().SetPlants(placement);
                PlantsWereSet = true;
                continue;
            }

            GetComponentInChildren<GenerateBuildings>().SetBuilding(placement);
        }
    }

    private void GenerateTurn()
    {
        var places = new List<Placement>
        {
            new Placement(10, 10, 90),
            new Placement(10, 0, 90),
            new Placement(0, -10, -180),
            new Placement(-10, -10, -180),
            new Placement(-10, 10, -90),
        };

        foreach (var placement in places)
        {
            int random = Random.Range(0, 100);
            if (random > 50 && !PlantsWereSet)
            {
                GetComponentInChildren<GeneratePlantsAndProps>().SetPlants(placement);
                PlantsWereSet = true;
                continue;
            }

            GetComponentInChildren<GenerateBuildings>().SetBuilding(placement);
        }
    }

    private void GenerateFullCross()
    {
        var places = new List<Placement>
        {
            new Placement(10, 10, 90),
            new Placement(10, -10, 90),
            new Placement(-10, -10, -180),
            new Placement(-10, 10, -90),
        };

        foreach (var placement in places)
        {
            int random = Random.Range(0, 100);
            if (random > 50 && !PlantsWereSet)
            {
                GetComponentInChildren<GeneratePlantsAndProps>().SetPlants(placement);
                PlantsWereSet = true;
                continue;
            }

            GetComponentInChildren<GenerateBuildings>().SetBuilding(placement);
        }
    }

    internal void MakeContentVisible()
    {
        GetComponentInChildren<GenerateBuildings>().MakeBuildingsVisible();

        if (PlantsWereSet)
        {
            GetComponentInChildren<GeneratePlantsAndProps>().MakePlantsVisible();
        }
    }
}
