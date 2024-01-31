using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private bool isDragging = false;
    private Vector2 originalPosition;

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

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