using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    public Transform RespawnPos;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.position = RespawnPos.position + 1f * Vector3.up; // 1f to make sure the player is above the ground
        }
    }
}
