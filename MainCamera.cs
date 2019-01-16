using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

	public GameObject player;
    public Transform endingPosition;
    public Transform startingPosition;
    public Transform targetsPosition;

    private float startTime;
    private bool starting;
    private bool following;
    private bool ended;
	private Vector3 angle;


	// Use this for initialization
	void Start () {

        starting = true;
        startTime = 7f;
		angle = transform.position - player.transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {

        if (starting)
        {
            startTime = startTime - Time.deltaTime;
            gameObject.transform.position = Vector3.LerpUnclamped(gameObject.transform.position, targetsPosition.position, 1f*Time.deltaTime);

            if(startTime <= 0)
            {
                starting = false;
                KeepFollowing();
            }
        }

        if(following == true)
		transform.position = player.transform.position + angle;

        else if (!following && !ended)
        {
            transform.position += new Vector3(0, 5f, -1f) * Time.deltaTime;
        }

        if (ended)
        {
            transform.Rotate(new Vector3(0, 10, 0) * Time.deltaTime);
        }

		
	}

    public void StopFollowing()
    {
        following = false;

    }


    public void EndFollowing()
    {
        following = false;
        ended = true;
        gameObject.transform.position = endingPosition.position;
        gameObject.transform.rotation = Quaternion.identity;

    }

    public void KeepFollowing()
    {
        following = true;
        gameObject.transform.position = startingPosition.position;
    }

    public bool isCameraShowingStage()
    {
        return starting;
    }
}
