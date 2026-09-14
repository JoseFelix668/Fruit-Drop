using UnityEngine;

public class Basket : MonoBehaviour
{
    public GameObject mouse;
    //offset allows each basket to share the same code, but allows them to be spread apart
    public float offset = 0f;
    void Start()
    {
        
    }

    void Update()
    {//Moves the baskets based on where the mouse's x position is
        transform.position = new(mouse.transform.position.x + offset, transform.position.y);
    }
}
