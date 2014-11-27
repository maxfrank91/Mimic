using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
    // ------ Public ------
    public enum GamePhase { DRAW, ORDER, WAIT, SHOW };
    public enum Symbol { CIRCLE, SQUARE, TRIANGLE }; // Add more later ...
    
    public GamePhase currentPhase;

    // ------ CONST ------
    const float TIME = 5;
    const float SHOWTIME = 2;
    const int START_SLOT_COUNT = 4;
    const int MAX_SLOT_COUNT = 10; //larger ??

    // ------ Private ------
    int level = 0;
    float timer = 0;
    int[] symbols;
    int index = 0;
    int currentSlotCount = 0;
    bool multiplayer;

    int lastIndex = -1; // just for testing...

	// Use this for initialization
	void Start () 
    {
        // Get Player Data here
        
        symbols = new int[MAX_SLOT_COUNT];
        if (level == 0) StartTutorial(); //lvl up after tutorial ??
        else multiplayer = true;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (currentPhase == GamePhase.DRAW)
        {   
            int input = -1;
            //Get Input Player here...
            // ### Test ###
            if (Input.GetKeyDown("1")) { input = 0; Debug.Log("Circle!"); } //Circle
            if (Input.GetKeyDown("2")) { input = 1; Debug.Log("Square!"); } //Square
            if (Input.GetKeyDown("3")) { input = 2; Debug.Log("Triangle!"); } //Triangle
            // ### Test ###

            if (input == -1) return;
            symbols[index] = input;
            index++;
            if (index >= currentSlotCount - 1)
            {
                currentPhase = GamePhase.WAIT;
                Debug.Log("WAIT!");
            }
        }
        if (currentPhase == GamePhase.ORDER)
        {
            timer += Time.deltaTime;

            int input = -1;
            //Get Input Player here...
            // ### Test ###
            if (Input.GetKeyDown("1")) { input = 0; Debug.Log("Circle!"); } //Circle
            if (Input.GetKeyDown("2")) { input = 1; Debug.Log("Square!"); } //Square
            if (Input.GetKeyDown("3")) { input = 2; Debug.Log("Triangle!"); } //Triangle
            // ### Test ###

            if (input == -1) return;
            if (input == symbols[index])
            {
                if (index >= currentSlotCount - 1)
                {
                    //Player wins
                    Debug.Log("You Win! Next Round!");
                    currentPhase = GamePhase.DRAW;
                    Debug.Log("DRAW!");
                    timer = 0;
                    index = 0;
                    input = -1;
                    currentSlotCount += 2;
                    if (currentSlotCount > MAX_SLOT_COUNT) currentSlotCount = MAX_SLOT_COUNT;
                }
                else
                    index++;
            }
            else if (input != symbols[index] || timer > currentSlotCount * TIME)
            {
                //Game over
                Debug.Log("Game Over!");
                return;
            }
        }
        if (currentPhase == GamePhase.WAIT)
        {
            if (!multiplayer) ContinueTutorial();
            //add multiplayer here
        }
        if (currentPhase == GamePhase.SHOW)
        {
            timer += Time.deltaTime;

            // SHOW AWESOME SHIT
            if (lastIndex != index)
            {
                Debug.Log((Symbol)symbols[index] + "   Input:  " + (index + 1));
                lastIndex = index;
            }       
            if (timer >= SHOWTIME)
            {
                index++;
                timer = 0;
            }
            if (index >= currentSlotCount)
            {
                currentPhase = GamePhase.ORDER;
                Debug.Log("ORDER!");
                index = 0;
                timer = 0;
            }
        }
	}

    void StartTutorial() 
    {
        multiplayer = false;
        currentPhase = GamePhase.WAIT;
        Debug.Log("WAIT!");
        currentSlotCount = START_SLOT_COUNT;

        ContinueTutorial();
    }

    void ContinueTutorial()
    {
        if (currentSlotCount > MAX_SLOT_COUNT - 2) EndTutorial();
        index = 0;
        for (int i = 0; i < currentSlotCount; i++)
            symbols[i] = Random.Range(0, System.Enum.GetNames(typeof(Symbol)).Length);
        currentPhase = GamePhase.SHOW;
        Debug.Log("SHOW!");
    }

    void EndTutorial()
    {
        Debug.Log("End of Tutorial");
        // Back to Menu ?
        // looking for other players ?
        // Level Up ??
    }
}
