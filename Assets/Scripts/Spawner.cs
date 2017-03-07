using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject Mob;
    public float SpawnTimer = 3.0f;
    public static int SpawnCount = 10;
    public bool Continuous = false;

    private float NextSpawnTime;
	private static int baseSpawnCount;
    public int spawnedMobs;

	private Transform enemyHolder;

    // Use this for initialization
    void Start()
    {
		baseSpawnCount = SpawnCount;
		enemyHolder = GameObject.Find ("EnemyHolder").transform;
    }
	public static void IncrementSpawnCount(int factor){
		SpawnCount += factor;
	}

	public static void DecrementSpawnCount(){
		baseSpawnCount -= 5;
	}

    // Update is called once per frame
    void Update()
    {
        if (Continuous)
        {
            if (Time.time >= NextSpawnTime && spawnedMobs < SpawnCount)
            {
                spawnedMobs++;
                NextSpawnTime += SpawnTimer;
                GameObject instance = Instantiate(Mob, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
				instance.transform.parent = enemyHolder;
			}
        }
        else if (spawnedMobs < SpawnCount)
            for (spawnedMobs = 0; spawnedMobs < SpawnCount; spawnedMobs++)
                Instantiate(Mob, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
    }
}
