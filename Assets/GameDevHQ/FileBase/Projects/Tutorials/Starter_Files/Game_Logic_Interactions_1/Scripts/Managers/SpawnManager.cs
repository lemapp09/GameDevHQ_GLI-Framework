using GLI_Framework;
using OccaSoftware.BOP;
using UnityEngine;

namespace Managers
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private  AI_Agent _aiPrefab;
        [SerializeField] public Pooler _objectPool;
        private Barriers _barrier;
        private static SpawnManager _instance;
        public static SpawnManager Instance { get { return _instance; } }
    
        private void Awake() {
            if (_instance != null && _instance != this) {
                Destroy(this.gameObject);
            } else {
                _instance = this;
            }

            _barrier = GameObject.FindGameObjectWithTag("Barriers").GetComponent<Barriers>();
            if (_barrier == null) {
                Debug.LogError("Barrier not found");
            }
        }

        public void Spawn() {
            GameObject ai = _objectPool.GetFromPool(_barrier._barriers[0].transform.position, 
                Quaternion.identity,this.transform);
            // ai.transform.parent = this.transform;
            ai.transform.position = _barrier._barriers[0].transform.position;
        }
    }
}