using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turnmanager : MonoBehaviour
{
    public int CurrentTurn, Maxturns;
    // Start is called before the first frame update
    void Start()
    {
        CurrentTurn = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        //bad code
        
        if (Input.GetKeyDown(KeyCode.E)&& GameObject.FindWithTag("projectile") == null)
        {
            CurrentTurn++;
            BroadcastMessage("OnturnChange");
            
                }
    }
}
