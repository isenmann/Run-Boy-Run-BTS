using System.Linq;
using UnityEngine;
using VRTK;

public class ModifiedDropZone : MonoBehaviour
{
    private VRTK_SnapDropZone dropZone;
    private Transform originalTransform;
    private GameObject ObjectEnteredDropZone;
    private long PlacedTiles;
    GameObject ParentTileObject;
    GameObject TimerAndPoints;
    AudioSource Audio;

    private void Start()
    {
        originalTransform = transform;

        dropZone = GetComponent<VRTK_SnapDropZone>();
        dropZone.ObjectEnteredSnapDropZone += DropZone_ObjectEnteredSnapDropZone;
        dropZone.ObjectExitedSnapDropZone += DropZone_ObjectExitedSnapDropZone;
        dropZone.ObjectSnappedToDropZone += DropZone_ObjectSnappedToDropZone;
        ParentTileObject = GameObject.FindGameObjectWithTag("TileParentObject");
        TimerAndPoints = GameObject.FindGameObjectWithTag("TimeAndPoints");
        Audio = GetComponent<AudioSource>();
    }

    private void DropZone_ObjectSnappedToDropZone(object sender, SnapDropZoneEventArgs e)
    {
        ObjectEnteredDropZone = null;
        e.snappedObject.tag = "SnappedTile";
        e.snappedObject.layer = LayerMask.NameToLayer("SnappedTile");

        foreach (Transform trans in e.snappedObject.GetComponentsInChildren<Transform>(true))
        {
            if (trans.gameObject.name.StartsWith("Plane"))
            {
                trans.gameObject.layer = e.snappedObject.layer;
            }
        }

        e.snappedObject.GetComponentInChildren<CharacterHitPlaneCenter>().enabled = true;
        e.snappedObject.GetComponentInChildren<GenerateTileContent>().MakeContentVisible();
        e.snappedObject.GetComponent<VRTK.Highlighters.VRTK_OutlineObjectCopyHighlighter>().enabled = false;
        e.snappedObject.GetComponent<VRTK_InteractableObject>().isGrabbable = false;
        e.snappedObject.GetComponent<VRTK_InteractableObject>().touchHighlightColor = new Color(0, 0, 0, 0 );
        e.snappedObject.GetComponent<VRTK_InteractableObject>().enabled = false;

        foreach (var dropZoneOfSnappedObject in e.snappedObject.GetComponentsInChildren<VRTK.VRTK_SnapDropZone>(true))
        { 
            dropZoneOfSnappedObject.gameObject.SetActive(true);
            var coliderOfNewDropZone = dropZoneOfSnappedObject.gameObject.GetComponent<BoxCollider>();
            var hitColliders = Physics.OverlapSphere(dropZoneOfSnappedObject.gameObject.transform.position, 0.02f);

            if (hitColliders.Any(hitCollider => hitCollider.gameObject.tag == "SnappedTile" && coliderOfNewDropZone.bounds.Intersects(hitCollider.bounds)))
            {
                dropZoneOfSnappedObject.gameObject.SetActive(false);
            }
        }   

        foreach (var dropZone in e.snappedObject.GetComponentsInChildren<DetectIfPlayerHitCollider>(true))
        {
            dropZone.gameObject.SetActive(true);
        }
        
        TimerAndPoints.GetComponent<Points>().IncreaseScore(e.snappedObject.GetComponent<GenerateTile>().Points);

        ParentTileObject.GetComponent<MoveTiles>().IncreasePplacedTiles();
    }

    private void DropZone_ObjectExitedSnapDropZone(object sender, SnapDropZoneEventArgs e)
    {
        if (e.snappedObject.tag == "Tile")
        {
            ObjectEnteredDropZone = null; 
        }
    }

    private void DropZone_ObjectEnteredSnapDropZone(object sender, SnapDropZoneEventArgs e)
    {
        if (e.snappedObject.tag == "Tile" && ObjectEnteredDropZone == null)
        {
            ObjectEnteredDropZone = e.snappedObject;
        }
    }

    void SnapToNearestNinetyDegree()
    {
        var vec = ObjectEnteredDropZone.transform.eulerAngles;

        float x = transform.eulerAngles.x;
        float y = Mathf.Round(vec.y / 90) * 90;
        float z = transform.eulerAngles.z;

        transform.eulerAngles = new Vector3(x, y, z);
    }

    private void FixedUpdate()
    {
        if (ObjectEnteredDropZone != null)
        {
           SnapToNearestNinetyDegree();
        }
    }
}
