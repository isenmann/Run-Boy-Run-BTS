using TMPro;
using UnityEngine;

public class DetectIfPlayerHitCollider : MonoBehaviour
{
    public GameObject GameOverScreen;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Character"))
        {
            Destroy(collision.gameObject);
            Invoke("GameOver", 0);
        }   
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name != "Character")
        {
            return;
        }

        Destroy(other.gameObject);
        Invoke("GameOver", 0);
    }

    private void GameOver()
    {
        GameObject.FindGameObjectWithTag("TimeAndPoints").GetComponent<Timer>().StopTimer();
        GameObject.FindGameObjectWithTag("TileParentObject").GetComponent<MoveTiles>().StopLerping();

        GameObject.FindGameObjectWithTag("Highscore").GetComponent<Highscore>()
            .SetHighscore(GameObject.FindGameObjectWithTag("TimeAndPoints").GetComponent<Timer>().Duration, GameObject.FindGameObjectWithTag("TimeAndPoints").GetComponent<Points>().Score);

        GameObject.FindGameObjectWithTag("TileGenerator").SetActive(false);
    }
}
