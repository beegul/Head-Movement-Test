using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VR;
using System.Collections;

public class Geri : MonoBehaviour
{
    public float minimum = 0.0f;
    public float maximum = 1.0f;
    public float duration = 5.0f;

    private float start_time;
    public SpriteRenderer sprite;

    private bool run = false;

    GameObject target;

    string NVRTask1 = "NVR Task 1";
    string VRTask1 = "VR Task 1";
    string NVRWTask1 = "NVRW Task 1";

    void Start ()
    {
        run = false;

        target = GameObject.Find("Target");
        start_time = Time.time;

        target.transform.position = new Vector3(Random.Range(-11.0f, 11.0f), Random.Range(-6.0f, 10.0f), -1.0f);
        sprite.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        //enables/disables if one of these scenes are loaded first and not swtiched to by the scenechanger. saves the headache.
        if (SceneManager.GetActiveScene().name == NVRTask1 || SceneManager.GetActiveScene().name == NVRWTask1)//disables VR.
        {
            VRSettings.enabled = false;
        }
        if (SceneManager.GetActiveScene().name == VRTask1)//enables VR.
        {
            VRSettings.enabled = true;
        }
    }
	
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            run = true;
        }
        if(run == true)
        {
            float t = (Time.time - start_time) / duration;
            sprite.color = new Color(1.0f, 1.0f, 1.0f, Mathf.SmoothStep(minimum, maximum, t));
        }      
	}
}