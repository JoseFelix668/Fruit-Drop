using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class Dropper : MonoBehaviour
{
    public float speed = 5f;
    public GameObject rightBorder;
    public GameObject leftBorder;
    public GameObject apple;
    public int spawnChance = 150;
    public GameObject basket;
    public GameObject basketR;
    public GameObject basketL;
    public UIDocument uIDocument;
    private Label scoreText;
    private Label totalScore;
    private Button restartButton;
    private int score = 0;
    private int lives = 3;
    private bool right;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText = uIDocument.rootVisualElement.Q<Label>("ScoreLabel");
        restartButton = uIDocument.rootVisualElement.Q<Button>("Restart");
        totalScore = uIDocument.rootVisualElement.Q<Label>("TotalScore");
        restartButton.style.display = DisplayStyle.None;
        totalScore.style.display = DisplayStyle.None;

        restartButton.clicked += ReloadScene;
    }

    // Update is called once per frame
    void Update()
    {   
        if(lives > 0)
        {
            int r = Random.Range(1, spawnChance + 1);
            if (right) {
                transform.position = new Vector2(transform.position.x + (speed * Time.deltaTime), transform.position.y); 
            } else
            {
                transform.position = new Vector2(transform.position.x - (speed * Time.deltaTime), transform.position.y); 
            }
            if(transform.position.x > rightBorder.transform.position.x)
            {
                  right = false;  
            }
            if(transform.position.x < leftBorder.transform.position.x)
            {
                 right = true;   
            }   

            if(r == spawnChance)
            {
                GameObject Apple = Instantiate(apple, transform.position, transform.rotation);
                Apple.SetActive(true);
            }  
        }
        scoreText.text = "Score: " + score; 

    }
    
    public void UpdateScore()
    {
        score++;
    }

    public void DestroyBasket()
    {
        if(lives == 3)
        {
            Destroy(basketL);
            lives--;
        } else if(lives == 2)
        {
            Destroy(basketR);
            lives--;
        } else
        {   
            lives--;
            restartButton.style.display = DisplayStyle.Flex;
            scoreText.style.display = DisplayStyle.None;
            totalScore.style.display = DisplayStyle.Flex;
            totalScore.text = "Final Score: " + score;
            Destroy(basket);
        }

    }
    
    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
