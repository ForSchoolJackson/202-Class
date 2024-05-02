using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class BulletMovement : MonoBehaviour
{
    [SerializeField]
    Camera camera;

    Vector3 direction = Vector3.right;
    Vector3 velocity = Vector3.zero;

    [SerializeField]
    float speed = 10;

    [SerializeField]
    public SpriteRenderer bulletPrefab;

    [SerializeField]
    public List<Sprite> bullets;

    [SerializeField]
    public int bulletCount = 0;

    [SerializeField]
    public List<SpriteRenderer> spawnedBullets = new List<SpriteRenderer>();

    [SerializeField]
    public SpriteRenderer player;

    [SerializeField]
    HudManager hudManager;

    bool isFire = false;
    bool isSpawning = false;
    float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

     //if game not over
        if(hudManager.isGameOver == false)
        {
            if (isFire)
            {

                //only spawn if the timer is less than zero
                if (timer <= 0f)
                {
                    Spawn();
                    isFire = false;
                    timer = 0.05f;
                }
                else
                {
                    //have timer go down
                    timer -= Time.deltaTime;
                }
            }
        }

       

        foreach (SpriteRenderer bullet in spawnedBullets)
        {
           
            if (bullet != null)
            {
                bullet.transform.Translate(direction * speed * Time.deltaTime);

                // Check if bullet is off-screen
                float camWidth = camera.orthographicSize * camera.aspect;
                if (bullet.transform.position.x > camWidth|| bullet.transform.position.x < -camWidth)
                {
                    //destroy the bullet
                    Destroy(bullet.gameObject);
                    spawnedBullets.Remove(bullet);
                    break;
                }
            }
        }


    }

    //spawn bullets
    public void Spawn()
    {
        isSpawning = true;

        SpriteRenderer newBullet = Instantiate(bulletPrefab);

        float x = 0f;
        float y = player.transform.position.y + 0.3f;
        Vector3 bulletDirection;

        //check facing dricrection of player
        if (player.transform.rotation.y == 0) 
        {
            // Spawn point at character right
             x = player.transform.position.x + 1.3f;
            bulletDirection = Vector3.right;

        }
        else
        {
            // Spawn point at character left
             x = player.transform.position.x - 1.3f;
            bulletDirection = Vector3.left;

        }

        newBullet.transform.position = new Vector3(x, y);
        spawnedBullets.Add(newBullet);
        bulletCount++;

        direction = bulletDirection;

        isSpawning = false;
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isFire = true;
        }
        else if (context.canceled)
        {
            isFire = false;
        }

    }

}
