using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour 
{
    // ------ Static ------
    static GameManager.Symbol SymbolInput;

    static bool input = false;

    public static int GetInput()
    {
        if (input)
        {
            input = false;
            return (int)SymbolInput;
        }
        else
            return -1;
    }

    public static void SetInput(GameManager.Symbol symbol)
    {
        input = true;
        SymbolInput = symbol;
    }
}
