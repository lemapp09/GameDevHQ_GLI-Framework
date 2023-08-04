using GLI_Framework;
using UnityEngine;

public class RespawnSpace : MonoBehaviour
{
    private void OnTriggerEnter(Collider Hit) {
        Hit.GetComponent<AI_Agent>().ReturnToPool();
        Debug.Log("Hit");
    }
}
