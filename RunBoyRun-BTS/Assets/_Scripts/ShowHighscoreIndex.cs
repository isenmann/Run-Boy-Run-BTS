using TMPro;
using UnityEngine;

public class ShowHighscoreIndex : MonoBehaviour
{
    public HighScoreEntry Highscore;
    public int HighscoreIndex;

	void Start ()
    {
        if (GameObject.FindGameObjectWithTag("Highscore").GetComponent<Highscore>().highscore.Entries.Count > HighscoreIndex)
        {
            Highscore = GameObject.FindGameObjectWithTag("Highscore").GetComponent<Highscore>().highscore.Entries[HighscoreIndex];

            GetComponentInChildren<TextMeshPro>().text = Highscore.Time + "s\n" + Highscore.Score + " points";
        }
    }
}
