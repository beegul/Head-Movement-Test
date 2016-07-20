using UnityEngine;
using System.Collections;

public class PatrolSpawner : MonoBehaviour
{
    public GameObject PatrolToSpawn;
    public float SpawnTime = 15.0f;
    float SpawnTimeCopy;

    UnityStandardAssets.Characters.ThirdPerson.AICharacterControl MyScript;

    void Start ()
    {
        //MyScript = GameObject.Find("Policeman").GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
        MyScript = GameObject.Find("Character").GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();

        SpawnTimeCopy = SpawnTime;
        //InvokeRepeating("spawn", SpawnTime, SpawnTime);
        //spawn();
    }
	
	void Update ()
    {
        if (MyScript.StartMoving == true)
        {
            spawn();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(PatrolToSpawn, transform.position, transform.rotation);
        }
        
	}
    void spawn()
    {
        //if(SpawnTime == 15.0f)
        //{
            //Instantiate(PatrolToSpawn, transform.position, transform.rotation);
        //}
        //Instantiate(PatrolToSpawn, transform.position, transform.rotation);
        SpawnTime -= Time.deltaTime;
        if(SpawnTime <= 0)
        {
            Instantiate(PatrolToSpawn, transform.position, transform.rotation);
            SpawnTime = SpawnTimeCopy;
        }
    }
}
