using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodMode 
{

    private bool isGodMode = false;
    private PlayerShipModel _model;


    public void Execute()
    {
        if (Input.GetKeyDown("g"))
        {
            if (isGodMode)
            {
                isGodMode = false;
                Debug.Log("god mode" + isGodMode);
            }
            else
            {
                isGodMode = true;
                Debug.Log("god mode" + isGodMode);
            }
        }
    }

    public bool IsGodMode()
    {
        return isGodMode;
    }

}
