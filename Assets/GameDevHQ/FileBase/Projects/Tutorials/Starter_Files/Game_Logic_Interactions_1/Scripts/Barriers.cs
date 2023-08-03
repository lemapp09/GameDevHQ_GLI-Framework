using UnityEngine;

public class Barriers : MonoBehaviour
{
    public Transform[] _barriers;

    private void Awake()
    {
        _barriers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            _barriers[i] = transform.GetChild(i);
        }

    }
}
