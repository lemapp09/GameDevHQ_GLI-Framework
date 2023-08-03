using GLI_Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private void Start() {
        Cursor.visible = false;
    }

    private void Update() {
        if (Keyboard.current.spaceKey.wasPressedThisFrame) {
            RaycastHit hitInfo;
            Ray rayorgin = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(rayorgin, out hitInfo, Mathf.Infinity, 1 << 6)) {
                hitInfo.collider.GetComponent<AI_Agent>().Death();
            }
        }
    }
}
