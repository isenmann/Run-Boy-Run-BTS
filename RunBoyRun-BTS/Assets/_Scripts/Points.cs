using UnityEngine;

public class Points : MonoBehaviour
{
    public int Score;

    public void IncreaseScore(int increaseScoreBy)
    {
        Score += increaseScoreBy;
        Debug.Log("SCORE: " + Score);
    }
}
