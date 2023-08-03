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

        private void Awake() {
            _barrier = GameObject.FindGameObjectWithTag("Barriers").GetComponent<Barriers>();
            if (_barrier == null) {
                Debug.LogError("Barrier not found");
            }
        }
        
        void Start() {
            Spawn();
        }

        public void Spawn() {
            GameObject ai = _objectPool.GetFromPool(_barrier._barriers[0].transform.position, 
                Quaternion.identity,this.transform);
            // ai.transform.parent = this.transform;
            ai.transform.position = _barrier._barriers[0].transform.position;
        }
    }
}