using UnityEngine;

public class Apple : MonoBehaviour
{   
    public float speed = 8f;
    public Dropper dropperScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - (speed * Time.deltaTime));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Rot"))
        {
            dropperScript.DestroyBasket();
            Destroy(gameObject);
        }
        if(collision.gameObject.tag.Equals("Basket"))
        {
            dropperScript.UpdateScore();
            Destroy(gameObject);
        }
    }

    public void AddSpeed()
    {
        speed += 2;
    }
}
