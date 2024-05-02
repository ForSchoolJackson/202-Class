using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

//emun for differneet enemies
public enum GhostTypes
{
    Regular,
    Special
}

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    Camera camera;
    protected SpawnManager() { }

    //the ghosts
    [SerializeField]
    public SpriteRenderer normalGhostPrefab;
    [SerializeField]
    public SpriteRenderer specialGhostPrefab;

    [SerializeField]
    public List<Sprite> ghosts;

    [SerializeField]
    public int ghostCount = 0;

    [SerializeField]
    public List<SpriteRenderer> spawnedGhosts = new List<SpriteRenderer>();

    [SerializeField]
    HudManager hudManager;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy();

        //if game is not over
        if(hudManager.isGameOver == false)
        {
            if (ghostCount < 10)
            {
                Spawn();
            }
        }
      
    }

    //spawn the ghosts in
    public void Spawn()
    {
        
        int randomInt = Random.Range(1, 10);
            for(int i = 0; i < randomInt; i++)
            {
                //pick random one to spawn
                SpawnGhosts(pickRandom());
                ghostCount++;
            }
          

    }

    //destroy the ghosts that go out of bounds 
    public void Destroy()
    {

        //camra pos
        float camWidth = camera.orthographicSize * camera.aspect;

        foreach (SpriteRenderer ghost in spawnedGhosts)
        {
            if (ghost != null && ghost.transform.position.x < -(camWidth+1) )
            {
                Destroy(ghost.gameObject);
                ghostCount--;
            }
        }
        spawnedGhosts.RemoveAll(ghost => ghost == null);

    }

    //spawn ghost srptes
    public void SpawnGhosts(GhostTypes type)
    {
        SpriteRenderer newGhost;

        if (type == GhostTypes.Regular)
        {
             newGhost = Instantiate(normalGhostPrefab);
        }
        else
        {
             newGhost = Instantiate(specialGhostPrefab);
        }

        //camera pos 
        float camWidth = camera.orthographicSize * camera.aspect;
        float camHeight = camera.orthographicSize;

        //spawn point
        float x = Random.Range(camWidth + 2, camWidth + 30);
        float y = Random.Range(-camHeight+2, camHeight-2);

        newGhost.transform.position = new Vector3(x, y);

        spawnedGhosts.Add(newGhost);
    }

    //randomize type
    GhostTypes pickRandom()
    {
        int rando = Random.Range(0, 100);

        if(rando < 10)
        {
            return GhostTypes.Special;
        }
        else
        {
            return GhostTypes.Regular;
        }
    }
}
