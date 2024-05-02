using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteInfo : MonoBehaviour
{
    [SerializeField]
    public Vector3 min, max;

    public SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        //get info at start
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            min = spriteRenderer.bounds.min;
            max = spriteRenderer.bounds.max;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (spriteRenderer != null)
        {
            //update info of bounds
            min = spriteRenderer.bounds.min;
            max = spriteRenderer.bounds.max;
        }
    }

    //gizom for seeing hitboxes
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        if (spriteRenderer != null)
        {
            Gizmos.DrawWireCube(transform.position, spriteRenderer.bounds.size);
        }

    }
}
