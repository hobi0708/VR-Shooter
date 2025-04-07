using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonRaycaster : MonoBehaviour
{
    public Camera playerCamera;
    public float maxDistance = 5f;

    void Update()
    {
        // Визуализируем луч в редакторе (и в Game при Gizmos включённых)
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * maxDistance, Color.cyan);

        if (Input.GetKeyDown(KeyCode.E)) // кнопка взаимодействия
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
            {
                Button btn = hit.collider.GetComponent<Button>();
                if (btn != null)
                {
                    btn.onClick.Invoke(); // симулируем клик
                    Debug.Log("Button clicked: " + btn.name);
                }
            }
        }
    }
}