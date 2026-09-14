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
    void Start()
    {
        scoreText = uIDocument.rootVisualElement.Q<Label>("ScoreLabel");
        restartButton = uIDocument.rootVisualElement.Q<Button>("Restart");
        totalScore = uIDocument.rootVisualElement.Q<Label>("TotalScore");
        restartButton.style.display = DisplayStyle.None;
        totalScore.style.display = DisplayStyle.None;

        restartButton.clicked += ReloadScene;
        //randomizes how often a teleport will happen
        teleportCallMax = Random.Range(teleportCallMin, teleportCallMax + 1);
        initialTeleportMax = teleportCallMax;
    }
    void Update()
    {   
        //Causes the apple branch to move around and spawn apples as long as atleast one basket remains
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
            //Teleports the apple branch to a random x location once the teleport timer
            //goes down and the player reaches the proper score
            if(score >= 70 && teleportTimer == teleportCallMax && teleportCallMax == initialTeleportMax)
            {
               transform.position = new Vector2(Random.Range(-8, 9), transform.position.y);
               if(score >= 100)
                {
                    teleportCallMax -= 60;
                }
            } else if (score >= 120 && teleportTimer == teleportCallMax)
            {
                transform.position = new Vector2(Random.Range(-8, 9), transform.position.y);
            }
            
            //Raises and checks the timer so that it can happen multiple times
            teleportTimer++;
            if(teleportTimer > teleportCallMax)
            {
                teleportTimer = 1;
            }
            SpawnApple();   
        }

        scoreText.text = "Score: " + score; 
        //Changes how often an apple can spawn and how fast they drop once the
        //player reaches the appropriate score
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
    {//Spawns an apple
        int r = Random.Range(1, spawnChance + 1);
        if(r == spawnChance)
            {
                GameObject Apple = Instantiate(apple, transform.position, transform.rotation);
                Apple.SetActive(true);
            }  
    }
    
    //Called in the Apple script, raises the players score whenever an apple is caught
    public void UpdateScore()
    {
        score++;
    }

    //Called in the Apple script, destroys baskets once an apple is missed,
    //also brings up the ends screen once all baskets are destroyed
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
