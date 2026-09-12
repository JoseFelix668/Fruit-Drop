using UnityEngine;

public class Basket : MonoBehaviour
{
    public GameObject mouse;
    public float offset = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new(mouse.transform.position.x + offset, transform.position.y);
    }
}
