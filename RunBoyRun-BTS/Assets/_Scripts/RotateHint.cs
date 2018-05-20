using UnityEngine;

public class RotateHint : MonoBehaviour
{
    void Update ()
    {
        transform.Rotate(Vector3.up * (25f * Time.deltaTime));
    }
}
