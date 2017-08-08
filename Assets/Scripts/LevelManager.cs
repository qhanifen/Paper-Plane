using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class LevelManager : MonoSingleton<LevelManager> {

    public Chunk[] levelChunks;
    public Obstacle[] obstaclePrefabs;
    public Vent[] ventPrefabs;

    private List<Chunk> activeChunks;
    private List<Chunk> chunkPool;


    private List<Obstacle> activeObstacles;
    private List<Obstacle> obstaclePool;

    private List<Vent> ventPool;
    private List<Vent> activeVents;

    public GameObject obstacleParent;
    public GameObject ventParent;
    public GameObject activeChunksParent;


    public int chunkAmount;
    public int obstacleDensity;

    public float accelSpeed;
    public float maxSpeed;
    public float minSpeed;
    public float deccelSpeed;
    public float levelSpeed;
    public float currentSpeed;


    // Use this for initialization
    void Start()
    {
        obstacleParent = new GameObject("Obstacle Pool");
        obstacleParent.SetActive(false);
        activeChunksParent = new GameObject("Active Chunk Pool");        
        ventParent = new GameObject("Vent Pool");
        ventParent.SetActive(false);
        currentSpeed = levelSpeed;

        //Fill the Obstacle Pool
        obstaclePool = new List<Obstacle>();
        activeObstacles = new List<Obstacle>();
        for(int i = 0; i < obstaclePrefabs.Length; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                Obstacle obs = Instantiate(obstaclePrefabs[i]) as Obstacle;
                obs.transform.parent = obstacleParent.transform;
                obstaclePool.Add(obs);
            }                      
        }

        //Fill the Vent Pool
        ventPool = new List<Vent>();
        activeVents = new List<Vent>();
        for (int i = 0; i < 15; i++)
        {
            Vent vent = Instantiate(ventPrefabs[0]) as Vent;
            vent.transform.parent = ventParent.transform;
            ventPool.Add(vent);
        }

        //Spawn the Level Chunks
        BuildLevel();
    }

    void BuildLevel()
    {
        activeChunks = new List<Chunk>();
        chunkPool = new List<Chunk>();        

        for (int i = 0; i < chunkAmount; i++)
        {
            SpawnNewChunk();
        }
	}

    void Update()
    {
        if(!GameManager.instance.gameOver)
        {
            MoveChunks();
        }
    }


    private Chunk GetChunk()
    {
        for (int i = 0; i < chunkPool.Count; i++)
        {
            if (!chunkPool[i].gameObject.activeInHierarchy)
            {
                return chunkPool[i];
            }
        }

        Chunk chunk = Instantiate(levelChunks[Random.Range(0, levelChunks.Length - 1)]);
        chunkPool.Add(chunk);        
        return chunk;
    }

    private void MoveChunks()
    {
        foreach(Chunk chunk in activeChunks)
        {
            chunk.transform.position += Vector3.back * currentSpeed * Time.deltaTime;            
        }
        if (activeChunks[0].transform.position.z <= - activeChunks[0].size - 10)
        {
            DespawnChunk(activeChunks[0]);
            SpawnNewChunk();
        }
    }    

    private void SpawnNewChunk()
    {
        Chunk newChunk = GetChunk();
        if (activeChunks.Count > 0)
        {
            Chunk lastChunk = activeChunks[activeChunks.Count - 1];
            newChunk.transform.position = lastChunk.transform.position + new Vector3(0, 0, lastChunk.size);
        }
        else
        {
            newChunk.transform.position = Vector3.zero + Vector3.back * newChunk.size / 2;
        }

        newChunk.transform.parent = activeChunksParent.transform;

        //Spawn random Obstacles
        int obstacleNumber = Random.Range(1, obstacleDensity);
        for (int i = 0; i < obstacleNumber; i++)
        {
            Obstacle obs = GetRandomObstacle();
            Vector3 spawnPoint = new Vector3(Random.Range(-newChunk.spawnBounds.x / 2, newChunk.spawnBounds.x / 2), newChunk.spawnBoundsCenter.y - (newChunk.spawnBounds.y / 2), Random.Range(-newChunk.spawnBounds.z / 2, newChunk.spawnBounds.z / 2) + newChunk.spawnBoundsCenter.z + newChunk.transform.position.z);
            obs.transform.position = spawnPoint;
            obs.transform.parent = newChunk.transform;
            obs.gameObject.SetActive(true);
        }

        //Spawn random Vents
        float spawnDistance = newChunk.spawnBounds.z / 4;
        for(int i = 1; i < 4; i++)
        {
            Vent vent = GetVent();
            vent.transform.position = new Vector3(Random.Range(-newChunk.spawnBounds.x / 2, newChunk.spawnBounds.x / 2), newChunk.spawnBoundsCenter.y - (newChunk.spawnBounds.y / 2), spawnDistance * i + newChunk.transform.position.z + newChunk.spawnBoundsCenter.z - (newChunk.spawnBounds.z / 2));
            vent.transform.parent = newChunk.transform;
            vent.gameObject.SetActive(true);
        }

        activeChunks.Add(newChunk);
        newChunk.gameObject.SetActive(true);
    }

    private void DespawnChunk(Chunk chunk)
    {
        chunk.gameObject.SetActive(false);
        chunk.transform.parent = obstacleParent.transform;
        activeChunks.Remove(chunk);

        Obstacle[] obs = chunk.GetComponentsInChildren<Obstacle>();
        for(int i = 0; i < obs.Length; i++)
        {
            obs[i].gameObject.SetActive(false);
            obs[i].gameObject.transform.parent = obstacleParent.transform;
            activeObstacles.Remove(obs[i]);
            obstaclePool.Add(obs[i]);
        }

        Vent[] vents = chunk.GetComponentsInChildren<Vent>();
        for(int i = 0; i < vents.Length; i++)
        {
            vents[i].gameObject.SetActive(false);
            activeVents.Remove(vents[i]);
            vents[i].transform.parent = ventParent.transform;
            ventPool.Add(vents[i]);
        }
    }

    public Vent GetVent()
    {
        if(ventPool.Count > 0)
        {
            Vent vent = ventPool[0];
            activeVents.Add(vent);
            ventPool.Remove(vent);
            return vent;
        }
        else
        {
            Vent vent = Instantiate(ventPrefabs[0]) as Vent;
            vent.transform.parent = ventParent.transform;
            activeVents.Add(vent);
            return vent;
        }        
    }

    public Obstacle GetRandomObstacle()
    {
        Obstacle obs;
        if (obstaclePool.Count > 0)
        {
            obs = obstaclePool[Random.Range(0, obstaclePool.Count)];        
            obstaclePool.Remove(obs);            
        }
        else
        {
            obs = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)]);            
        }

        activeObstacles.Add(obs);
        return obs;
    }

    public void Accelerate()
    {
        if(currentSpeed <= maxSpeed)
            currentSpeed += accelSpeed;
    }

    public void Deccelerate()
    {
        if(currentSpeed >= minSpeed)
            currentSpeed -= deccelSpeed;
    }

    public static IEnumerator ResetSpeed()
    {
        while(instance.currentSpeed != instance.levelSpeed)
        {
            instance.currentSpeed = Mathf.Lerp(instance.currentSpeed, instance.levelSpeed, 0.2f);
            yield return null;
        }
    }
}
