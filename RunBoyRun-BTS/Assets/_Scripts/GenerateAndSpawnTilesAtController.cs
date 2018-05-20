using UnityEngine;

public class GenerateAndSpawnTilesAtController : MonoBehaviour
{
    public GameObject TileToSpawn;

    private void Awake()
    {
        Invoke("SpawnTile", 0.5f);
    }

    private void GenerateAndSpawnTilesAtController_InteractableObjectSnappedToDropZone(object sender, VRTK.InteractableObjectEventArgs e)
    {
        Invoke("SpawnTile", 0.5f);
    }

    private void SpawnTile()
    {
        GameObject tile = Instantiate(TileToSpawn);

        tile.transform.localScale = new Vector3(tile.transform.localScale.x / 10, tile.transform.localScale.y / 10, tile.transform.localScale.z / 10);
        tile.transform.parent = transform;
        tile.transform.localPosition = Vector3.zero;
        tile.transform.localRotation = transform.localRotation;

        tile.GetComponent<VRTK.VRTK_InteractableObject>().InteractableObjectSnappedToDropZone += GenerateAndSpawnTilesAtController_InteractableObjectSnappedToDropZone;
    }
}