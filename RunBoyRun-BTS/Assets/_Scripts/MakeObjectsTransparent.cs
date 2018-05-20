using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeObjectsTransparent : MonoBehaviour
{
    private GameObject Character;
    List<RaycastHit> OldHits = new List<RaycastHit>();

    private void Start()
    {
        Character = GameObject.FindGameObjectWithTag("Character");
    }

    void FixedUpdate()
    {
        if (Camera.main == null)
        {
            return;
        }

        foreach (var hit in OldHits)
        {
            Renderer rend = hit.collider.gameObject.GetComponent<MeshRenderer>();

            if (rend)
            {
                rend.material.shader = Shader.Find("Standard");
            }
        }

        OldHits.Clear();

        if (Character == null)
        {
            return;
        }

        RaycastHit[] hits;
        hits = Physics.RaycastAll(Character.transform.position, (Camera.main.transform.position - Character.transform.position), 100.0F);

        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                Renderer rend = hit.collider.gameObject.GetComponent<MeshRenderer>();

                if (rend)
                {
                    rend.material.shader = Shader.Find("Transparent/Diffuse");
                    Color tempColor = rend.material.color;
                    tempColor.a = 0.3F;
                    rend.material.color = tempColor;
                }
            }

            OldHits.AddRange(hits);
        }
    }
}
