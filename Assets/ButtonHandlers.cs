using UnityEngine;
using System.Collections;

public class ButtonHandlers : MonoBehaviour 
{   
    public void StartButton()
    {
        if (PlayerData.LVL == 0)
        {
            Debug.Log("Tutorial!");
            Application.LoadLevel("test1");
        }
        //Tutorial Spielen und so!
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void FriendsButton()
    {
        transform.GetChild(2).gameObject.SetActive(true);
        //even more cool stuff!
    }

    public void RandomButton()
    {
        //cool stuff and so!
        Debug.Log("Multiplayer!");
        Application.LoadLevel("test1");
    }

    public void NoFriendsButton()
    {
        transform.GetChild(2).gameObject.SetActive(false);
    }

	void Awake () 
    {
        if (PlayerData.NAME == "Unknown")
            PlayerData.NAME = PlayerPrefs.GetString("PlayerName", "Player");
        if (PlayerData.XP == -1)
            PlayerData.XP = PlayerPrefs.GetInt("PlayerXP", 0);
        if (PlayerData.LVL == -1)
            PlayerData.LVL = PlayerPrefs.GetInt("PlayerLevel", 0);
	}
}
