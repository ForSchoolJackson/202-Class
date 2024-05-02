using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{

    [SerializeField]
    Text scoreText;

    const string k_Score_STR = "Score: {0}";

    [SerializeField]
    public int totalScore = 0;

    [SerializeField]
    List<Image> hearts = new List<Image>();

    [SerializeField]
    public GameObject gameOver;

    [SerializeField]
    CollisionManager collisionManager;

    [SerializeField]
    public bool isGameOver;


    // Start is called before the first frame update
    void Start()
    {
        //start with all hearts
        foreach(Image heart in hearts)
        {
            heart.gameObject.SetActive(true);
        }

        //start without game over
        gameOver.SetActive(false);
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
       
         //score only updates when game not over
        if (isGameOver ==false)
        {
            //update score
            scoreText.text = string.Format(k_Score_STR, totalScore); //based on enimies killed
            
        }
       


    }

    public void loseLife()
    {
        //update hearts
        if (hearts.Count > 0)
        {
            Image currentHeart = hearts[hearts.Count - 1];
            currentHeart.gameObject.SetActive(false);

            //remove from list
            hearts.Remove(currentHeart);
        }
        if(hearts.Count == 0)
        {
            //show game over
            if (gameOver != null)
            {
                gameOver.SetActive(true);
            }

            //game is over
            isGameOver = true;

            //clear all sprites
            if (collisionManager != null)
            {
                foreach (SpriteInfo sprite in collisionManager.sprites)

                {
                    //delete ghost
                    Destroy(sprite.gameObject);

                }

            }

        }
    }
}
