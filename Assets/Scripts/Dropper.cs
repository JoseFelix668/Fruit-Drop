using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class Dropper : MonoBehaviour
{
    [Header ("Movement")]
    public float speed = 5f;
    public GameObject rightBorder;
    public GameObject leftBorder;
    public int teleportCallMin = 120;
    public int teleportCallMax = 180;
    private bool right;
    private int teleportTimer = 1;
    protected int initialTeleportMax = 0;
    [Header ("Apple")]
    public GameObject apple;
    public Apple appleScript;
    public int spawnChance = 150;
    [Header ("Basket")]
    public GameObject basket;
    public GameObject basketR;
    public GameObject basketL;
    [Header ("User")]
    private int score = 0;
    private int lives = 3;
    [Header ("Misc")]
    public UIDocument uIDocument;
    private Label scoreText;
    private Label totalScore;
    private Button restartButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText = uIDocument.rootVisualElement.Q<Label>("ScoreLabel");
        restartButton = uIDocument.rootVisualElement.Q<Button>("Restart");
        totalScore = uIDocument.rootVisualElement.Q<Label>("TotalScore");
        restartButton.style.display = DisplayStyle.None;
        totalScore.style.display = DisplayStyle.None;

        restartButton.clicked += ReloadScene;
        teleportCallMax = Random.Range(teleportCallMin, teleportCallMax + 1);
        initialTeleportMax = teleportCallMax;
    }

    // Update is called once per frame
    void Update()
    {   
        if(lives > 0)
        {
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

            if(score >= 80 && teleportTimer == teleportCallMax && teleportCallMax == initialTeleportMax)
            {
               transform.position = new Vector2(Random.Range(-8, 9), transform.position.y);
               if(score >= 120)
                {
                    teleportCallMax -= 60;
                }
            } else if (score >= 120 && teleportTimer == teleportCallMax)
            {
                transform.position = new Vector2(Random.Range(-8, 9), transform.position.y);
            }
            
            teleportTimer++;
            if(teleportTimer > teleportCallMax)
            {
                teleportTimer = 1;
            }
            SpawnApple();   
        }

        scoreText.text = "Score: " + score; 
        if(spawnChance == 150 && score >= 20)
        {
            spawnChance = 125;
            appleScript.AddSpeed();            
        } else if (spawnChance == 125 && score >= 50)
        {
            spawnChance = 100;
            appleScript.AddSpeed();
        }

    }

    void SpawnApple()
    {
        int r = Random.Range(1, spawnChance + 1);
        if(r == spawnChance)
            {
                GameObject Apple = Instantiate(apple, transform.position, transform.rotation);
                Apple.SetActive(true);
            }  
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
