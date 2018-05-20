using UnityEngine;

public class MoveArrowUpDown : MonoBehaviour
{
    Vector3 StartPosition;
    public Vector3 EndPosition;
    bool backwards = false;

	void Start ()
    {
        StartPosition = transform.localPosition;
    }

    private void Update()
    {
        float step = 0.9f * Time.deltaTime;

        if (!backwards)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, EndPosition, step);
            if (transform.localPosition == EndPosition)
            {
                backwards = true;
            }
        }
        else
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, StartPosition, step);
            if (transform.localPosition == StartPosition)
            {
                backwards = false;
            }
        }
    }
}
