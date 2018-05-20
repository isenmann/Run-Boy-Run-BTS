using UnityEngine;
using System.Collections;

public class MoveTiles : MonoBehaviour
{
    private float IncreaseSpeedBy = 0.5f;

    /// <summary>
    /// The time taken to move from the start to finish positions
    /// </summary>
    public float timeTakenDuringLerp = 1f;

    /// <summary>
    /// How far the object should move when 'space' is pressed
    /// </summary>
    public float distanceToMove = 1000;

    public GameObject Character;

    //Whether we are currently interpolating or not
    private bool _isLerping;

    //The start and finish positions for the interpolation
    private Vector3 _startPosition;
    private Vector3 _endPosition;

    //The Time.time value when we started the interpolation
    private float _timeStartedLerping;

    private long PlacedTiles = 0;

    /// <summary>
    /// Called to begin the linear interpolation
    /// </summary>
    public void StartLerping()
    {
        _isLerping = true;
        _timeStartedLerping = Time.time;

        //We set the start position to the current position, and the finish to 10 spaces in the 'back' direction
        _startPosition = transform.position;
        _endPosition = transform.position + (Character.transform.forward * -1)  * distanceToMove;
    }

    public void StopLerping()
    {
        _isLerping = false;
    }

    public void SetRandomDirection(int index)
    {
        switch (index)
        {
            case 1:
                _isLerping = false;
                StartCoroutine(Rotate(Vector3.up, 90, 0.15f));
                Debug.Log("Right");
                break;
            case 2:
                _isLerping = false;
                StartCoroutine(Rotate(Vector3.down, 90, 0.15f));
                Debug.Log("Left");
                break;
            case 0:
                StartLerping();
                Debug.Log("Forward");
                break;
        }
    }

    //We do the actual interpolation in FixedUpdate(), since we're dealing with a rigidbody
    void FixedUpdate()
    {
        if (_isLerping)
        {
            //We want percentage = 0.0 when Time.time = _timeStartedLerping
            //and percentage = 1.0 when Time.time = _timeStartedLerping + timeTakenDuringLerp
            //In other words, we want to know what percentage of "timeTakenDuringLerp" the value
            //"Time.time - _timeStartedLerping" is.
            float timeSinceStarted = Time.time - _timeStartedLerping;
            float percentageComplete = timeSinceStarted / timeTakenDuringLerp;

            //Perform the actual lerping.  Notice that the first two parameters will always be the same
            //throughout a single lerp-processs (ie. they won't change until we hit the space-bar again
            //to start another lerp)
            transform.position = Vector3.Lerp(_startPosition, _endPosition, percentageComplete);

            //When we've completed the lerp, we set _isLerping to false
            if (percentageComplete >= 1.0f)
            {
                _isLerping = false;
            }
        }
    }

    IEnumerator Rotate(Vector3 axis, float angle, float duration = 1.0f)
    {
        Quaternion from = Character.transform.rotation;
        Quaternion to = Character.transform.rotation;
        to *= Quaternion.Euler(axis * angle);

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            Character.transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Character.transform.rotation = to;
        StartLerping();
    }

    public void IncreasePplacedTiles()
    {
        PlacedTiles = PlacedTiles + 1;

        if (PlacedTiles % 15 == 0)
        {
            timeTakenDuringLerp -= IncreaseSpeedBy;
        }
    }
}

