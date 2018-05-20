using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBuildings : MonoBehaviour
{
    public List<GameObject> Buildings = new List<GameObject>();
    private List<int> GeneratedIndixes = new List<int>();

    private void Start()
    {
        foreach (var transformOfChild in GetComponentsInChildren<Transform>(true).Skip(1))
        {
            Buildings.Add(transformOfChild.gameObject);
        }
    }

    public void SetBuilding(Placement placement)
    {
        int buildingIndex = UnityEngine.Random.Range(0, Buildings.Count);
        GeneratedIndixes.Add(buildingIndex);
        Buildings[buildingIndex].transform.localPosition = new Vector3(placement.X, 0, placement.Z);
        Buildings[buildingIndex].transform.localEulerAngles = new Vector3(0, Buildings[buildingIndex].transform.localEulerAngles.y + placement.Rotation, 0);
    }

    internal void MakeBuildingsVisible()
    {
        GeneratedIndixes.ForEach(p => Buildings[p].SetActive(true));
    }
}
