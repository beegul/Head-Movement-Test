using UnityEngine;
using System.Collections;

public class Geri : MonoBehaviour
{
    public float minimum = 0.0f;
    public float maximum = 1.0f;
    public float duration = 5.0f;

    private float start_time;
    public SpriteRenderer sprite;

    GameObject target;

    void Start ()
    {
        target = GameObject.Find("Target");
        start_time = Time.time;

        target.transform.position = new Vector3(Random.Range(-11.0f, 11.0f), Random.Range(-6.0f, 10.0f), -1.0f);
	}
	
	void Update ()
    {
        float t = (Time.time - start_time) / duration;
        sprite.color = new Color(1.0f,1.0f,1.0f, Mathf.SmoothStep(minimum, maximum, t));
	}
}