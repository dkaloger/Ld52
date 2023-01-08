using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
public class Turnmanager : MonoBehaviour
{
    public int CurrentTurn, Maxturns;
    public bool turnRunning;
    public GameObject[] turnactions;
    public Tilemap tilemap;
    public int  usedmoves;
    public int[] movesperturn;
    public TextMeshProUGUI movedisplay, Turndisplay,losedisplay;
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
                    spriteRenderer.color = new Vector4(1, 1, 1,1 - (float)i/ (turnactions.Length+1) );
                    //((float)i/3+1)
                    spriteRenderer.gameObject.transform.position = tilemap.layoutGrid.WorldToCell(spriteRenderer.gameObject.transform.position) + new Vector3(0.5f, 0.5f, 0);
                }
            }
            i++;
        }
    }
    public void advanceTurn()
    {
        if (GameObject.FindWithTag("projectile") == null && gameObject.GetComponent<PlaceCard>().toplace == null)
        {
            CurrentTurn++;
            // CurrentTurn++;
            BroadcastMessage("OnturnChange");
            usedmoves = 0;
            StartCoroutine(Waituntilcompleted());


        }
    }
    private IEnumerator WaitForRestart()
    {
        yield return new WaitForSeconds(2);
        gameObject.GetComponent<Levelmanager>().Reload();

    }
    private IEnumerator Waituntilcompleted()
    {

        yield return new WaitUntil(() => GameObject.FindWithTag("projectile") == null);

        if (turnactions.Length > CurrentTurn)
        {
            foreach (MonoBehaviour comp in turnactions[CurrentTurn].transform.GetComponentsInChildren<MonoBehaviour>())
            {
                comp.enabled = true;

            }
            foreach (SpriteRenderer sprt in turnactions[CurrentTurn].transform.GetComponentsInChildren<SpriteRenderer>())
            {
                sprt.color = new Vector4(1, 1, 1, 1.0f);

            }
            foreach (Collider2D comp in turnactions[CurrentTurn].transform.GetComponentsInChildren<Collider2D>())
            {
                comp.enabled = true;
            }
        }
        
        
    }

        // Update is called once per frame
        void Update()
    {
        if (CurrentTurn> Maxturns)
        {
            Turndisplay.enabled = false;
        }
        if(movesperturn[CurrentTurn] > 100)
        {
            movedisplay.enabled = false;
        }
            if (CurrentTurn != Maxturns)
        {
            movedisplay.text = "Moves used: " + usedmoves.ToString() + "/" + movesperturn[CurrentTurn].ToString();
        }

        Turndisplay.text = "Turn: " + CurrentTurn.ToString() + "/" + Maxturns.ToString();
        //bad code
        turnRunning = false;
        
        if(GameObject.FindWithTag("projectile") == null && GameObject.FindWithTag("Enemy") != null && CurrentTurn == Maxturns)
        {
            losedisplay.text = "Level failed. Restarting";
            StartCoroutine(WaitForRestart());
        }
        if (usedmoves == movesperturn[CurrentTurn])
        {
            advanceTurn();
        }
        /////////////REMOVE ON RELEASE /////////////
      //  if (Input.GetKeyDown(KeyCode.E))
     //   {
         //   advanceTurn();
      //  }
        /////////////REMOVE ON RELEASE /////////////


    }
}
