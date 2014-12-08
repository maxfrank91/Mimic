using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
    // ------ Public ------
    public enum GameMode { TUTORIAL, FRIEND, RANDOM };
    public enum GamePhase { DRAW, ORDER, WAIT, SHOW, START_TUTORIAL, GAMEOVER };
    public enum Symbol { CIRCLE, SQUARE, TRIANGLE }; // Add more later ...
    
    public GamePhase currentPhase;
   
    // ------ CONST ------
    const float TIME = 5;
    const float SHOWTIME = 2;
    const int START_SLOT_COUNT = 4;
    const int MAX_SLOT_COUNT = 10; //larger ??

    // ------ Private ------

    private GameMode currentMode;

    int level = 0;
    int xp = 0;
    float timer = 0;
    int[] symbols;
    int index = 0;
    int currentSlotCount = 0;
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
        else currentMode = GameMode.RANDOM; // ----> Friend later
        im = GetComponent<ImageManager>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        int input;
        im.SetPhase(currentPhase.ToString());
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

            if (currentMode != GameMode.TUTORIAL) timer += Time.deltaTime;       
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
                if (currentMode == GameMode.TUTORIAL) return; // message: try again!!
                timer = 0;
                Debug.Log("Game Over!");
                currentPhase = GamePhase.GAMEOVER;
            }
        }
        else if (currentPhase == GamePhase.WAIT)
        {
            im.showBar(false);
            if (currentMode == GameMode.TUTORIAL) ContinueTutorial();
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
                im.showSymbol(symbols[index], false);
                Debug.Log((Symbol)symbols[index] + ", " + index);
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
                im.BlackScreen();
                index = 0;
                timer = 0;
            }
        }
        else if (currentPhase == GamePhase.START_TUTORIAL)
        {
            if (symbols[index] == InputManager.GetInput()) index++;
            else if (InputManager.GetInput() != -1) Debug.Log("try again");
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
            im.showSymbol(symbols[index], false);
            im.showBar(false);

            lastIndex = index;
        }
        else if (currentPhase == GamePhase.GAMEOVER)
        {
            timer += Time.deltaTime;

            Recognizer.recog.gameObject.SetActive(false);
            im.showBar(false);

            if (timer >= TIME)
                Application.LoadLevel("Menu");
        }
	}

    void StartTutorial() 
    {
        currentMode = GameMode.TUTORIAL;
        currentSlotCount = START_SLOT_COUNT;
        timer = 0;
        index = 0;
        for (int i = 0; i < currentSlotCount; i++)
            symbols[i] = i;
        currentPhase = GamePhase.START_TUTORIAL;
        Debug.Log("TUT!");       
    }

    void ContinueTutorial()
    {
        if (currentSlotCount > 6) EndTutorial();
        index = 0;
        for (int i = 0; i < currentSlotCount; i++)
            symbols[i] = Random.Range(0, System.Enum.GetNames(typeof(Symbol)).Length);
        currentPhase = GamePhase.SHOW;
        Debug.Log("SHOW!");
    }

    void EndTutorial()
    {
        // Player Win Message 
        PlayerData.LVL = 1;
        currentPhase = GamePhase.GAMEOVER;
    }
}
