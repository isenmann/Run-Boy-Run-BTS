using UnityEngine;

public class Timer : MonoBehaviour
{
    private float StartTime;
    public float Duration;

    public void StartTimer()
    {
        StartTime = Time.time;
    }

    public void StopTimer()
    {
        Duration = Time.time - StartTime;
        Debug.Log("ELAPSED TIME: " + Duration);
    }
}
