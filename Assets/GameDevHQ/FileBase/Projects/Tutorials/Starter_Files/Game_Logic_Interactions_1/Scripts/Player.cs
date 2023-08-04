using GLI_Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private bool _isGameOver;
    private void Start() {
        Cursor.visible = false;
    }

    private void OnEnable() {
        Actions.GameOver += GameOver;
    }

    private void GameOver() {
        _isGameOver = true;
    }

    private void Update() {
        if (!_isGameOver) {
            if (Keyboard.current.spaceKey.wasPressedThisFrame) {
                GameManager.Instance.gunFired();
                RaycastHit hitInfo;
                Ray rayorgin = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                if (Physics.Raycast(rayorgin, out hitInfo, Mathf.Infinity, 1 << 6)) {
                    if (hitInfo.collider.GetComponent<AI_Agent>() != null) {
                        AudioManager.Instance.PlayAIDeath();
                        hitInfo.collider.GetComponent<AI_Agent>().Death();
                    }  else if (hitInfo.collider.GetComponent<ForceBarriers>()) {
                        AudioManager.Instance.PlayShotBarrier();
                        hitInfo.collider.GetComponent<ForceBarriers>().Deactive();
                    }
                }
            }
        }
    }

    private void OnDisable() {
        Actions.GameOver -= GameOver;
    }
}
