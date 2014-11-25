using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
    public enum GamePhase { DRAW, ORDER, WAIT, SHOW };
    public enum Symbol { CIRCLE, SQUARE, TRIANGLE }; // Add more later ...
    
    public GamePhase currentPhase;

    const float TIME = 5;
    const float SHOWTIME = 2;
    const int START_SLOT_COUNT = 4;
    const int MAX_SLOT_COUNT = 10;
    
    int level = 0;
    float timer = 0;
    int[] symbols;
    int index = 0;
    int currentSlotCount = 0;

    GamePhase lastPhase;

	// Use this for initialization
	void Start () 
    {
        symbols = new int[MAX_SLOT_COUNT];
        if (level == 0) StartTutorial();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (currentPhase == GamePhase.DRAW)
        {
        }
        if (currentPhase == GamePhase.ORDER)
        {
        }
        if (currentPhase == GamePhase.WAIT)
        {
        }
        if (currentPhase == GamePhase.SHOW)
        {
            timer += Time.deltaTime;

            // SHOW AWESOME SHIT
            Debug.Log((Symbol)symbols[index]);

            if (timer >= SHOWTIME)
            {
                index++;
                timer = 0;
            }
            if (index >= currentSlotCount) currentPhase = GamePhase.ORDER;
        }
	}

    void StartTutorial() 
    {
        currentPhase = GamePhase.WAIT;
        currentSlotCount = START_SLOT_COUNT;

        for (int i = 0; i < currentSlotCount; i++)
            symbols[i] = Random.Range(0, System.Enum.GetNames(typeof(Symbol)).Length);
        currentPhase = GamePhase.SHOW;
    }
}
