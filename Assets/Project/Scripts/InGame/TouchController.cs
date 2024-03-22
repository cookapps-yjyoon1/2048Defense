using UnityEngine;

public class TouchController : MonoBehaviour
{
    [SerializeField] private float dragDistance = 25;
    private Vector3 touchStart, touchEnd;
    private bool isTouch = false;
    private bool isVaildArea = true;
    
    public Direction UpdateTouch()
    {
        Direction direction = Direction.None;

        if (Input.GetMouseButtonDown(0) && isVaildArea)
        {
            isTouch = true;
            touchStart = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0) && isVaildArea)
        {
            if (isTouch == false) return Direction.None;

            touchEnd = Input.mousePosition;

            float deltaX = touchEnd.x - touchStart.x;
            float deltaY = touchEnd.y - touchStart.y;

            if (Mathf.Abs(deltaX) < dragDistance && Mathf.Abs(deltaY) < dragDistance) return Direction.None;

            if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
            {
                if (Mathf.Sign(deltaX) >= 0) direction = Direction.Right;
                else direction = Direction.Left;
            }
            else
            {
                if (Mathf.Sign(deltaY) >= 0) direction = Direction.Up;
                else direction = Direction.Down;
            }
        }

        if (direction != Direction.None) isTouch = false;

        return direction;
    }

    public void PointerDown()
    {
        isVaildArea = false;
    }

    public void PointerUp()
    {
        isVaildArea = true;
    }
}