using System.Collections;
using GLI_Framework;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;

    public void explosion()
    {
        StartCoroutine(explosionCoroutine());
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 20f, 1 << 6);
        foreach (var hitCollider in hitColliders) {
            if (hitCollider.CompareTag("AI_Agent")) {
                    hitCollider.GetComponent<AI_Agent>().Death();
            }
        }
    }

    private IEnumerator explosionCoroutine()
    {
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<Collider>().enabled = false;
        _explosion.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        _explosion.SetActive(false);
    }
}
