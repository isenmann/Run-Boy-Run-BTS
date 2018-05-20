using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GenerateTile : MonoBehaviour
{
    private List<VRTK.VRTK_SnapDropZone> DropZones = new List<VRTK.VRTK_SnapDropZone>();
    private List<DetectIfPlayerHitCollider> WrongSnappedCollider = new List<DetectIfPlayerHitCollider>();
    public int Points;

    private void Start()
    {
        DropZones.AddRange(GetComponentsInChildren<VRTK.VRTK_SnapDropZone>(true));
        DropZones.ForEach(p => p.gameObject.SetActive(false));

        WrongSnappedCollider.AddRange(GetComponentsInChildren<DetectIfPlayerHitCollider>(true));
        WrongSnappedCollider.ForEach(p => p.gameObject.SetActive(false));

        GetComponentInChildren<CharacterHitPlaneCenter>().enabled = false;

        Invoke("Generate", 0);
    }

    private void Generate()
    {
        GameObject generatedRoadLayout = GetComponentInChildren<GenerateWays>().GetRandomWayLayout();       
        DropZones.ForEach(p => p.gameObject.SetActive(false));
        VRTK.VRTK_SnapDropZone dropZone;
        DetectIfPlayerHitCollider wrongDirectionCollider;

        switch (generatedRoadLayout.name)
        {
            case "StraightLayout":
                DropZones.Find(p => p.tag == "DropZoneLeft").gameObject.SetActive(false);
                DropZones.Find(p => p.tag == "DropZoneRight").gameObject.SetActive(false);
                
                dropZone = DropZones.Find(p => p.tag == "DropZoneTop");
                DropZones.Remove(dropZone);
                Destroy(dropZone.gameObject);

                dropZone = DropZones.Find(p => p.tag == "DropZoneBottom");
                DropZones.Remove(dropZone);
                Destroy(dropZone.gameObject);

                WrongSnappedCollider.Find(p => p.tag == "ColliderTop").gameObject.SetActive(false);
                WrongSnappedCollider.Find(p => p.tag == "ColliderBottom").gameObject.SetActive(false);

                wrongDirectionCollider = WrongSnappedCollider.Find(p => p.tag == "ColliderLeft");
                WrongSnappedCollider.Remove(wrongDirectionCollider);
                Destroy(wrongDirectionCollider.gameObject);

                wrongDirectionCollider = WrongSnappedCollider.Find(p => p.tag == "ColliderRight");
                WrongSnappedCollider.Remove(wrongDirectionCollider);
                Destroy(wrongDirectionCollider.gameObject);

                Points = 2;
                break;
            case "TurnLayout":
                DropZones.Find(p => p.tag == "DropZoneRight").gameObject.SetActive(false);
                DropZones.Find(p => p.tag == "DropZoneTop").gameObject.SetActive(false);

                dropZone = DropZones.Find(p => p.tag == "DropZoneLeft");
                DropZones.Remove(dropZone);
                Destroy(dropZone.gameObject);

                dropZone = DropZones.Find(p => p.tag == "DropZoneBottom");
                DropZones.Remove(dropZone);
                Destroy(dropZone.gameObject);

                WrongSnappedCollider.Find(p => p.tag == "ColliderLeft").gameObject.SetActive(false);
                WrongSnappedCollider.Find(p => p.tag == "ColliderBottom").gameObject.SetActive(false);

                wrongDirectionCollider = WrongSnappedCollider.Find(p => p.tag == "ColliderTop");
                WrongSnappedCollider.Remove(wrongDirectionCollider);
                Destroy(wrongDirectionCollider.gameObject);

                wrongDirectionCollider = WrongSnappedCollider.Find(p => p.tag == "ColliderRight");
                WrongSnappedCollider.Remove(wrongDirectionCollider);
                Destroy(wrongDirectionCollider.gameObject);

                Points = 2;
                break;
            case "FullCrossLayout":
                DropZones.Find(p => p.tag == "DropZoneLeft").gameObject.SetActive(false);
                DropZones.Find(p => p.tag == "DropZoneRight").gameObject.SetActive(false);
                DropZones.Find(p => p.tag == "DropZoneTop").gameObject.SetActive(false);
                DropZones.Find(p => p.tag == "DropZoneBottom").gameObject.SetActive(false);

                wrongDirectionCollider = WrongSnappedCollider.Find(p => p.tag == "ColliderLeft");
                WrongSnappedCollider.Remove(wrongDirectionCollider);
                Destroy(wrongDirectionCollider.gameObject);

                wrongDirectionCollider = WrongSnappedCollider.Find(p => p.tag == "ColliderRight");
                WrongSnappedCollider.Remove(wrongDirectionCollider);
                Destroy(wrongDirectionCollider.gameObject);

                wrongDirectionCollider = WrongSnappedCollider.Find(p => p.tag == "ColliderBottom");
                WrongSnappedCollider.Remove(wrongDirectionCollider);
                Destroy(wrongDirectionCollider.gameObject);

                wrongDirectionCollider = WrongSnappedCollider.Find(p => p.tag == "ColliderTop");
                WrongSnappedCollider.Remove(wrongDirectionCollider);
                Destroy(wrongDirectionCollider.gameObject);

                Points = 10;
                break;
            case "TCrossLayout":
                DropZones.Find(p => p.tag == "DropZoneLeft").gameObject.SetActive(false);
                DropZones.Find(p => p.tag == "DropZoneRight").gameObject.SetActive(false);
                DropZones.Find(p => p.tag == "DropZoneBottom").gameObject.SetActive(false);

                dropZone = DropZones.Find(p => p.tag == "DropZoneTop");
                DropZones.Remove(dropZone);
                Destroy(dropZone.gameObject);

                WrongSnappedCollider.Find(p => p.tag == "ColliderTop").gameObject.SetActive(false);

                wrongDirectionCollider = WrongSnappedCollider.Find(p => p.tag == "ColliderLeft");
                WrongSnappedCollider.Remove(wrongDirectionCollider);
                Destroy(wrongDirectionCollider.gameObject);

                wrongDirectionCollider = WrongSnappedCollider.Find(p => p.tag == "ColliderRight");
                WrongSnappedCollider.Remove(wrongDirectionCollider);
                Destroy(wrongDirectionCollider.gameObject);

                wrongDirectionCollider = WrongSnappedCollider.Find(p => p.tag == "ColliderBottom");
                WrongSnappedCollider.Remove(wrongDirectionCollider);
                Destroy(wrongDirectionCollider.gameObject);

                Points = 5;
                break;
        }

        Guid nameExt = Guid.NewGuid();

        foreach (var trans in transform.GetComponentsInChildren<Transform>(true))
        {
            trans.name += "_" + nameExt;
        }

        GetComponentInChildren<GenerateTileContent>().Generate(generatedRoadLayout.name);
    }
}
