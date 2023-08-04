using System.Collections;
using UnityEngine;

public class ForceBarriers : MonoBehaviour
{

    public void Deactive()
    {
        StartCoroutine(DeactiveCoroutine());
    }

    private IEnumerator DeactiveCoroutine()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(Random.Range(3f, 10f));
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        this.GetComponent<BoxCollider>().enabled = true;
    }
}
