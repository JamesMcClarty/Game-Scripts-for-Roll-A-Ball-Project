using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public float delta;  // Amount to move left and right from the start point
	public float speed; 
	private Vector3 startPos;
    private bool moving;

	void Start () {
		startPos = transform.position;
        moving = true;
	}

	void Update () {
        if (moving)
        {
            Vector3 v = startPos;
            v.x += delta * Mathf.Sin(Time.time * speed);
            transform.position = v;
        }
	}

    public void SwitchMoving()
    {
        moving = !moving;
    }

}