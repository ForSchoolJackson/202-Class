using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{

    [SerializeField]
    public List<SpriteInfo> sprites = new List<SpriteInfo>();

    [SerializeField]
    List<SpriteInfo> fires = new List<SpriteInfo>();

    [SerializeField]
    SpriteInfo player;

    [SerializeField]
    SpriteInfo arm;

    [SerializeField]
    SpawnManager spawnManager;

    [SerializeField]
    BulletMovement bulletManager;

    [SerializeField]
    HudManager hudManager;


    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
       

        //clear out every time
        sprites.Clear();
        fires.Clear();

        //get the sprite info for each
        foreach (SpriteRenderer sprite in spawnManager.spawnedGhosts)
        {
            if (sprite != null)
            {
                sprites.Add(sprite.GetComponent<SpriteInfo>());
            }
        }

        //get list of bullets
        foreach (SpriteRenderer fire in bulletManager.spawnedBullets)
        {
            if (fire != null)
            {
                fires.Add(fire.GetComponent<SpriteInfo>());
            }
            
        }


        player.spriteRenderer.color = UnityEngine.Color.white;
        arm.spriteRenderer.color = UnityEngine.Color.white;

        //loop through list
        foreach (SpriteInfo sprite in sprites)
        {
            //check with player
            if (AABBCollisionCheck(player, sprite) == true)
            {
               // player.spriteRenderer.color = UnityEngine.Color.green;
              //  arm.spriteRenderer.color = UnityEngine.Color.green;
              //  sprite.spriteRenderer.color = UnityEngine.Color.green;

                //delete ghost
                Destroy(sprite.gameObject);
                spawnManager.ghostCount--;

                //lose a heart
                hudManager.loseLife();
                

                break;

            }

            

            //ghost and bullet collide
            foreach (SpriteInfo fire in fires)
            {
                if (AABBCollisionCheck(sprite, fire) == true)
                {
                    // Destroy both GameObjects when they collide
                    Destroy(sprite.gameObject);
                    spawnManager.ghostCount--;
                    Destroy(fire.gameObject);

                    //add to score
                    hudManager.totalScore += 100;

                    break;
                }

            }
        }

    }

    //AABB collision check 
    bool AABBCollisionCheck(SpriteInfo spriteA, SpriteInfo spriteB)
    {
        if (spriteA.max.x > spriteB.min.x && spriteA.min.x < spriteB.max.x && spriteA.max.y > spriteB.min.y && spriteA.min.y < spriteB.max.y)
        {
            return true;
        }
        return false;
    }
}
