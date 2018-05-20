using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHitPlaneCenter : MonoBehaviour
{
    GameObject Character;
    MoveTiles TileController;
    int DirectionIndex = -1;
    bool Triggered = false;

    string NameExtension;

    private void Start()
    {
        TileController = GameObject.FindGameObjectWithTag("TileParentObject").GetComponent<MoveTiles>();
        Character = GameObject.FindGameObjectWithTag("Character");
        NameExtension = transform.name.Substring(transform.name.LastIndexOf("_"));

        //StartCoroutine(CopmuteRandomDirection());
    }

    private IEnumerator CopmuteRandomDirection()
    {
        var wait = new WaitForSeconds(0.2f);
        yield return wait;

        // This would cast rays only against colliders in layer 9.
        // But instead we want to collide against everything except layer 9. The ~ operator does this, it inverts a bitmask.
        int layerMask = 1 << 9;
        layerMask = ~layerMask;

        List<Vector3> directions = new List<Vector3>();
        directions.Add(Character.transform.forward);
        directions.Add(Character.transform.right);
        directions.Add(Character.transform.right * -1);

        while (DirectionIndex == -1)
        {
            int index = Random.Range(0, 3);

            Ray ray = new Ray(transform.position, directions[index]);
            foreach (var hit in Physics.RaycastAll(ray, 30, layerMask))
            {
                if (hit.collider != null && hit.collider.gameObject.name.StartsWith("DropZone_") && hit.collider.gameObject.name.EndsWith(NameExtension))
                {
                    DirectionIndex = index;
                    break;
                }
            }
        }

        TileController.SetRandomDirection(DirectionIndex);
    }

    void OnTriggerEnter(Collider other)
    {
        if (Triggered)
        {
            return;
        }

        if(other.name != "Character" )
        {
            return;
        }

        Triggered = true;
        StartCoroutine(CopmuteRandomDirection());
    }
}
