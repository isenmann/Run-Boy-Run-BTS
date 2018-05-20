using System.Collections;
using UnityEngine;
using VRTK;

public class TileHelper : MonoBehaviour
{
    private bool EnterSnapZone = false;
    private GameObject ObjectEnteredDropZone;

    private void Start()
    {
        GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += ScaleAfterGrabbed_InteractableObjectGrabbed;
        GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed += TileHelper_InteractableObjectUngrabbed;
        GetComponent<VRTK_InteractableObject>().InteractableObjectEnteredSnapDropZone += TileHelper_InteractableObjectEnteredSnapDropZone;
        GetComponent<VRTK_InteractableObject>().InteractableObjectExitedSnapDropZone += TileHelper_InteractableObjectExitedSnapDropZone;
    }

    private void TileHelper_InteractableObjectUngrabbed(object sender, InteractableObjectEventArgs e)
    {
        ObjectEnteredDropZone = null;

        if (EnterSnapZone)
        {
            return;
        }

        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;
        transform.parent = null;
    }

    private void TileHelper_InteractableObjectExitedSnapDropZone(object sender, InteractableObjectEventArgs e)
    {
        EnterSnapZone = false;
        if (e.interactingObject.tag.Contains("DropZone"))
        {
            ObjectEnteredDropZone = null;
        }
    }

    private void TileHelper_InteractableObjectEnteredSnapDropZone(object sender, InteractableObjectEventArgs e)
    {
        EnterSnapZone = true;

        if (e.interactingObject.tag.Contains("DropZone"))
        {
            ObjectEnteredDropZone = e.interactingObject;
        }
    }

    private void ScaleAfterGrabbed_InteractableObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed -= ScaleAfterGrabbed_InteractableObjectGrabbed;
        StartCoroutine(UpdateTransformDimensions());
    }

    private IEnumerator UpdateTransformDimensions()
    {
        float elapsedTime = 0f;

        Vector3 startScale = transform.localScale;
        Vector3 endScale = new Vector3(0.05f, 0.05f, 0.05f);
        bool storedKinematicState = GetComponent<VRTK_InteractableObject>().isKinematic;
        GetComponent<VRTK_InteractableObject>().isKinematic = true;
         
        while (elapsedTime <= 0.25f)
        {
            elapsedTime += Time.deltaTime;

            transform.SetGlobalScale(Vector3.Lerp(startScale, endScale, (elapsedTime / 0.25f)));

            yield return null;
        }

        transform.SetGlobalScale(endScale);

        GetComponent<VRTK_InteractableObject>().isKinematic = storedKinematicState;
    }

    void SnapToNearestNinetyDegree()
    {
        var vec = transform.eulerAngles;

        float x = ObjectEnteredDropZone.transform.eulerAngles.x;
        float y = Mathf.Round(vec.y / 90) * 90;
        float z = ObjectEnteredDropZone.transform.eulerAngles.z;

        ObjectEnteredDropZone.transform.eulerAngles = new Vector3(x, y, z);
    }

    private void FixedUpdate()
    {
        if (ObjectEnteredDropZone != null)
        {
            SnapToNearestNinetyDegree();
        }
    }
}
