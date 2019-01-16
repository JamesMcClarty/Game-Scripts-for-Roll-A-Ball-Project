using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesCount : MonoBehaviour {

    Text livesText;


	// Use this for initialization
	void Start () {
        if (!PlayerPrefs.HasKey("PlayerLives"))
        {
            PlayerPrefs.SetInt("PlayerLives",3);
        }
        livesText = GetComponent<Text>();
        livesText.text = PlayerPrefs.GetInt("PlayerLives").ToString();
	}

    public void LoseLife()
    {
        PlayerPrefs.SetInt("PlayerLives", PlayerPrefs.GetInt("PlayerLives") - 1);
        livesText.text = PlayerPrefs.GetInt("PlayerLives").ToString();
    }

    public int GetLivesCount()
    {
        if(PlayerPrefs.HasKey("PlayerLives"))
            return PlayerPrefs.GetInt("PlayerLives");

        else
        {
            return 5;
        }
    }
}
