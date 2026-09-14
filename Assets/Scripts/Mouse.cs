using UnityEngine;
using UnityEngine.InputSystem;

public class Mouse : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    void OnMove(InputValue value)
    {//Checks where the mouse is and moves a square to it
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(value.Get<Vector2>());
        transform.position = mousePos;
    }
}
