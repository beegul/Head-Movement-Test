using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyToSpawn;
    //public float SpawnTime;

    UnityStandardAssets.Characters.ThirdPerson.AICharacterControl MyScript;

    int WaypointNumber = 0;
    bool SpawnEnemy = false;

    void Start ()
    {
        //MyScript = GameObject.Find("Policeman").GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
        MyScript = GameObject.Find("Character").GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
    }

    void Update()
    {
        if (MyScript.StartMoving == true)
        {
            WaypointNumber = MyScript.CurrentTarget;

            if (WaypointNumber == 4)
            {
                if (SpawnEnemy == false && gameObject.name == "EnemySpawner1")
                {
                    Spawn();
                    SpawnEnemy = true;
                }
            }

            if (WaypointNumber == 1)
            {
                if (SpawnEnemy == false && gameObject.name == "EnemySpawner2")
                {
                    Spawn();
                    SpawnEnemy = true;
                }
            }

            if (WaypointNumber == 7)
            {
                if (SpawnEnemy == false && gameObject.name == "EnemySpawner3")
                {
                    Spawn();
                    SpawnEnemy = true;
                }
            }

            if (WaypointNumber == 10)
            {
                if (SpawnEnemy == false && gameObject.name == "EnemySpawner4")
                {
                    Spawn();
                    SpawnEnemy = true;
                }
            }

            if (WaypointNumber == 7)
            {
                if (SpawnEnemy == false && gameObject.name == "EnemySpawner5")
                {
                    Spawn();
                    SpawnEnemy = true;
                }
            }

            if (WaypointNumber == 1)
            {
                if (SpawnEnemy == false && gameObject.name == "EnemySpawner6")
                {
                    Spawn();
                    SpawnEnemy = true;
                }
            }

            if (WaypointNumber == 5)
            {
                if (SpawnEnemy == false && gameObject.name == "EnemySpawner7")
                {
                    Spawn();
                    SpawnEnemy = true;
                }
            }

            if (WaypointNumber == 10)
            {
                if (SpawnEnemy == false && gameObject.name == "EnemySpawner8")
                {
                    Spawn();
                    SpawnEnemy = true;
                }
            }

            if (WaypointNumber == 18)
            {
                SpawnEnemy = false;
            }
        }       
    }
    void Spawn()
    {
        Instantiate(EnemyToSpawn, transform.position, transform.rotation);
    }

}
