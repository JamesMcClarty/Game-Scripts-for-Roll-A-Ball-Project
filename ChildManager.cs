using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildManager : MonoBehaviour {

	void OnCollisionEnter(Collision col)
	{
		col.transform.parent = gameObject.transform;
		Debug.Log ("Parent");
	}

	void OnCollisionExit(Collision col)
	{
		col.transform.parent = null;
	}
}
