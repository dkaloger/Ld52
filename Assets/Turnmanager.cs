using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Turnmanager : MonoBehaviour
{
    public int CurrentTurn, Maxturns;
    public bool turnRunning;
    public GameObject[] turnactions;
    public Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    { int i=0;
        CurrentTurn = 0;
        foreach (GameObject turn in turnactions)
        {
         
            if (i > 0)
            {
                turn.SetActive(true);
                foreach (MonoBehaviour comp in turn.transform.GetComponentsInChildren<MonoBehaviour>())
                {
                    comp.enabled = false;
                }
                foreach (Collider2D comp in turn.transform.GetComponentsInChildren<Collider2D>())
                {
                    comp.enabled = false;
                }
                foreach (SpriteRenderer spriteRenderer in turn.transform.GetComponentsInChildren<SpriteRenderer>())
                {
                    spriteRenderer.enabled = true;
                    spriteRenderer.color = new Vector4(1, 1, 1, 1.0f / (i+1 ) );
                    spriteRenderer.gameObject.transform.position = tilemap.layoutGrid.WorldToCell(spriteRenderer.gameObject.transform.position) + new Vector3(0.5f, 0.5f, 0);
                }
            }
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //bad code
        turnRunning = false;

        if (Input.GetKeyDown(KeyCode.E)&& GameObject.FindWithTag("projectile") == null && !gameObject.GetComponent<PlaceCard>().holding)
        {
           // CurrentTurn++;
            BroadcastMessage("OnturnChange");
            if(CurrentTurn != 0)
            {
                foreach (MonoBehaviour comp in turnactions[CurrentTurn].transform.GetComponentsInChildren<MonoBehaviour>())
                {
                    comp.enabled = true;
                    
                }
                foreach (Collider2D comp in turnactions[CurrentTurn].transform.GetComponentsInChildren<Collider2D>())
                {
                    comp.enabled = true;
                }

            }
            CurrentTurn++;

        }
    }
}
