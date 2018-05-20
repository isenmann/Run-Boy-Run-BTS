using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePlantsAndProps : MonoBehaviour
{
    public List<GameObject> Plants = new List<GameObject>();
    private List<int> GeneratedIndixes = new List<int>();

    private void Start()
    {
        foreach (var transformOfChild in GetComponentsInChildren<Transform>(true).Skip(1))
        {
            Plants.Add(transformOfChild.gameObject);
        }
    }

    public void SetPlants(Placement placement)
    {
        foreach (var plant in Plants)
        {
            plant.transform.localPosition = new Vector3(placement.X + Random.Range(-5, 5), 0, placement.Z + Random.Range(-5, 5));
            plant.transform.localEulerAngles = new Vector3(0, plant.transform.localEulerAngles.y + Random.Range(0, 360), 0);
        }
    }

    internal void MakePlantsVisible()
    {
        Plants.ForEach(p => p.SetActive(true));
    }
}
