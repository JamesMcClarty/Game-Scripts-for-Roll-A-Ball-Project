using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemGenerator : MonoBehaviour {

    [SerializeField]
    GameObject[] items;
    [SerializeField]
    Transform[] spawnpoints;

    public GameObject spikedBall;
    public GameObject coin;

	// Use this for initialization
	void Start () {
        spawnpoints = gameObject.GetComponentsInChildren<Transform>();
        items = new GameObject[spawnpoints.Length];

        InstantiateItems();
	}
	

	public void InstantiateItems () {

        int randomNum = 0;

		for (int i = 1; i < spawnpoints.Length; i++)
        {
            randomNum = Mathf.RoundToInt(Random.Range(0, 3));


            switch (randomNum)
            {
                case 0:
                    break;
                case 1:
                    items[i] = Instantiate(coin, spawnpoints[i].transform.position, Quaternion.identity);
                    break;
                case 2:
                    items[i] = Instantiate(spikedBall, spawnpoints[i].transform.position, Quaternion.identity);
                    break;

            }
        }
	}

    public void DeleteItems()
    {
        for (int i = 0; i < items.Length; i++)
        {
            Destroy(items[i]);
        }
    }




}
