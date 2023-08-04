using System.Collections;
using OccaSoftware.BOP;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace GLI_Framework
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AI_Agent : MonoBehaviour
    {
        [SerializeField] private float _timeDelayLow = 5f;
        [SerializeField] private float _timeDelayHigh = 10f;
        private Animator _anim;
        public GameManager GameManager { get; private set; }

        enum AI_States {
            Run,
            Hide,
            Death
        }
        private  NavMeshAgent _agent;
        private Barriers _barrier;
        [Range(1,19)]
        [SerializeField]
        public int _currentBarrier = 1;
        private AI_States _currentState = AI_States.Run;
        
        void Awake() {
            _anim = GetComponent<Animator> ();
            _barrier = GameObject.FindGameObjectWithTag("Barriers").GetComponent<Barriers>();
            if (_barrier == null) {
                Debug.LogError("Barrier not found");
            }
            GameManager = GetComponentInChildren<GameManager>();
        }

        private void OnEnable()
        {
            _currentBarrier = Random.Range(0, _barrier._barriers.Length - 2 );
            this.transform.position = _barrier._barriers[_currentBarrier].transform.position;
            _agent = this.GetComponent<NavMeshAgent>();
            _agent.enabled = true;
            _agent.speed = Random.Range(1f, 2f);
            _agent.SetDestination(_barrier._barriers[_currentBarrier + 1].transform.position);
            Actions.GameOver += GameOver;
        }

        private void GameOver()
        {
           ReturnToPool();
        }

        private void Update() {
            if (_currentState == AI_States.Run) {
                if (_agent.remainingDistance < .05f) {
                    StartCoroutine(Hiding());
                    _currentBarrier++;
                    if (_currentBarrier < _barrier._barriers.Length - 1) {
                        _agent.SetDestination(_barrier._barriers[_currentBarrier].transform.position);
                    }  else {
                        ReturnToPool();
                    }
                }
                if(_agent.velocity.magnitude < 0.1f)
                    _currentBarrier++;
            }
            _anim.SetFloat("Speed", _agent.velocity.magnitude);
            Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 2f, 1 << 6);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("AI_Agent")) {
                    if (Random.value > 0.5f) {
                        _currentBarrier += 1;
                    } else {
                        _currentBarrier -= 1;
                    }
                    if (_currentBarrier <= 1) _currentBarrier = 1;
                    if (_currentBarrier >= _barrier._barriers.Length) _currentBarrier = _barrier._barriers.Length - 2;
                    break;
                }
            }
        }

        public void ReturnToPool()
        {
            var PoolInstance = GetComponent<Instance>();
            if (PoolInstance != null) {
                if (PoolInstance.GetPoolerOrigin() != null) {
                    AudioManager.Instance.PlayAIComplete();
                    GameManager.Instance.AIRespawned();
                    PoolInstance.Despawn();
                }
            }
        }
        
        private IEnumerator Hiding() {
            _currentState = AI_States.Hide;
            _anim.SetBool("Hiding" , true);
            float timeDelay = Random.Range(_timeDelayLow, _timeDelayHigh);
            yield return new WaitForSeconds(timeDelay);
            if (_currentState == AI_States.Hide) {
                _currentState = AI_States.Run;
                _anim.SetBool("Hiding", false);
            }
        }

        public void Death() {
            _anim.SetBool("Death" , true);
            _currentState = AI_States.Death;
            _agent.enabled = false;
            GameManager.Instance.AIAgentDeath(50);
            StartCoroutine(DeathDelay());
        }

        private IEnumerator DeathDelay() {
            yield return new WaitForSeconds(1f);
            this.gameObject.SetActive(false);
            var PoolInstance = GetComponent<Instance>();
            if (PoolInstance != null) {
                if (PoolInstance.GetPoolerOrigin() != null) {
                    PoolInstance.Despawn();
                }
            }
        }

        private void OnDisable() {
            Actions.GameOver -= GameOver;
        }
    }
}