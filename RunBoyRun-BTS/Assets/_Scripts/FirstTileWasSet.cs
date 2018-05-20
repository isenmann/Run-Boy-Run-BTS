using UnityEngine;
using VRTK;

public class FirstTileWasSet : MonoBehaviour
{
    void Start ()
	{
	    VRTK_SnapDropZone dropZone = GetComponent<VRTK_SnapDropZone>();
        dropZone.ObjectSnappedToDropZone += DropZone_ObjectSnappedToDropZone;
    }

    private void DropZone_ObjectSnappedToDropZone(object sender, SnapDropZoneEventArgs e)
    {
        foreach (var hint in GameObject.FindGameObjectsWithTag("Hint"))
        {
            hint.SetActive(false);
        }

        VRTK_SnapDropZone dropZone = GetComponent<VRTK_SnapDropZone>();
        dropZone.ObjectSnappedToDropZone -= DropZone_ObjectSnappedToDropZone;
        GetComponentInParent<MoveTiles>().StartLerping();
        GameObject.FindGameObjectWithTag("TimeAndPoints").GetComponent<Timer>().StartTimer();
    }
}
