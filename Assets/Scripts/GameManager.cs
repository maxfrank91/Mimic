using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
    // ------ Public ------
    public enum GamePhase { DRAW, ORDER, WAIT, SHOW, TUT };
    public enum Symbol { CIRCLE, SQUARE, TRIANGLE }; // Add more later ...
    
    public GamePhase currentPhase;

    // ------ CONST ------
    const float TIME = 5;
    const float SHOWTIME = 2;
    const int START_SLOT_COUNT = 4;
    const int MAX_SLOT_COUNT = 10; //larger ??

    // ------ Private ------
    int level = 0;
    int xp = 0;
    float timer = 0;
    int[] symbols;
    int index = 0;
    int currentSlotCount = 0;
    bool multiplayer;
    int lastIndex = -1; // just for testing ?
    ImageManager im;

	// Use this for initialization
	void Start () 
    {
        // Get Player Data here
        level = PlayerData.LVL == -1 ? 0 : PlayerData.LVL;
        xp = PlayerData.XP;
        symbols = new int[MAX_SLOT_COUNT];
        if (level == 0) StartTutorial(); //lvl up after tutorial ??
        else multiplayer = true;
        im = GetComponent<ImageManager>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        int input;
        if (currentPhase == GamePhase.DRAW)
        {
            //mousestuff
            Recognizer.recog.gameObject.SetActive(true);
            im.showBar(false);

            input = InputManager.GetInput();
            if (input == -1) return;

            symbols[index] = input;
            im.showSymbol(symbols[index]);
            Debug.Log((Symbol)input);
            index++;
            if (index >= currentSlotCount - 1)
            {
                currentPhase = GamePhase.WAIT;
                Debug.Log("WAIT!");
            }
        }
        else if (currentPhase == GamePhase.ORDER)
        {
            //mousestuff
            Recognizer.recog.gameObject.SetActive(true);
            im.showBar(true);
            
            timer += Time.deltaTime;       
            im.reduceBar(timer, currentSlotCount * TIME);

            input = InputManager.GetInput();
            if (input == -1) return;

            im.showSymbol(input);
            if (input == symbols[index])
            {
                if (index >= currentSlotCount -1)
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
        else if (currentPhase == GamePhase.WAIT)
        {
            im.showBar(false);
            if (!multiplayer) ContinueTutorial();
            //add multiplayer here
        }
        else if (currentPhase == GamePhase.SHOW)
        {
            //mousestuff
            Recognizer.recog.gameObject.SetActive(false);
            im.showBar(true);

            timer += Time.deltaTime;
            im.reduceBar(timer, SHOWTIME);         
            if (lastIndex != index)
            {
                im.showSymbol(symbols[index], true);
                Debug.Log((Symbol)symbols[index] + ", " + index);
                lastIndex = index;
            }       
            if (timer >= SHOWTIME)
            {
                index++;
                timer = 0;
            }
            if (index >= currentSlotCount - 1)
            {
                currentPhase = GamePhase.ORDER;
                Debug.Log("ORDER!");
                index = 0;
                timer = 0;
            }
        }
        else if (currentPhase == GamePhase.TUT)
        {
            if (symbols[index] == InputManager.GetInput())  index++;
            if (lastIndex == index) return;
            if (index >= 3)
            {
                index = 0;
                lastIndex = -1;
                currentPhase = GamePhase.WAIT;
                Debug.Log("WAIT!");
                return;
            }
           
            Recognizer.recog.gameObject.SetActive(true);
            im.showSymbol(symbols[index], true);
            im.showBar(false);

            lastIndex = index;
        }
	}

    void StartTutorial() 
    {
        currentSlotCount = START_SLOT_COUNT;
        multiplayer = false;
        timer = 0;
        index = 0;
        for (int i = 0; i < currentSlotCount; i++)
            symbols[i] = i;
        currentPhase = GamePhase.TUT;
        Debug.Log("TUT!");       
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
        PlayerData.LVL = 1;
        Application.LoadLevel("Menu");
    }
}
