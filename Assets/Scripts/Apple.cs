using UnityEngine;

public class Apple : MonoBehaviour
{   
    public float speed = 8f;
    public Dropper dropperScript;
    void Start()
    {
        
    }

    void Update()
    {//Makes the apple move downward
        transform.position = new Vector2(transform.position.x, transform.position.y - (speed * Time.deltaTime));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {//checks where an apple collides to decide whether the player loses a basket or gets a point
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
    //Called in the Dropper scirpt, makes the apples drop faster
    public void AddSpeed()
    {
        speed += 2;
    }
}
