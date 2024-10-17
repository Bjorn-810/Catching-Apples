using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallHighlight : MonoBehaviour
{
    public GameObject highlight;
    public LayerMask groundLayer;

    private void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            // Check if the hit object is on one of the allowed layers
        //    if (((1 << hit.collider.gameObject.layer) & groundLayer) != 0)
            {
                Instantiate(highlight, hit.point, Quaternion.identity);
            }
        }
    }
}
