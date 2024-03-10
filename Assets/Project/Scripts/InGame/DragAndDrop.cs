using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private bool isDragging = false;
    private Vector2 originalPosition;
    [SerializeField]private Camera _camera;

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePos = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _camera.nearClipPlane));

            transform.position = mousePos;
        }
    }
    
    public void OnDrag()
    {
        isDragging = true;
        gameObject.SetActive(true);
    }

    public void OnEndDrag()
    {
        isDragging = false;
        gameObject.SetActive(false);
    }
}