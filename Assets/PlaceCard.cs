using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class PlaceCard : MonoBehaviour
{
    public CardSO toplace;
    public GameObject hologram;
    public bool canplace;
    public Tilemap tilemaptoplace;
    public Vector3 originalposition;
    public Vector3[] directions;
    public GameObject[] storedneighbors;
    public Transform arenaboundslow, arenaboundhigh;
    public Transform enemyarenaboundslow, enemyarenaboundhigh;
    public bool holding;
    public bool holdingenemy;
    Vector3 temppos;
    public string tag;
     Vector3 slingshotdirection;
    public bool removing;
    PlaceableItem Getitematpos(Vector3 position)
    {
        foreach (PlaceableItem unit in GameObject.FindObjectsOfType<PlaceableItem>())
        {
            if (Vector3.Equals(unit.transform.position, position))
            {
                return unit;
            }
            
        }
        return null;
    }
    bool calculaterestrictions(CardSO card , Vector3 pos)
    {
        bool result =true;
        foreach (var restriction in card.restrictions)
        {
            if (restriction.anydirection)
            {
              
                Vector3 direction = Vector3.zero;
                if (toplace == restriction.card && Vector3.Distance(hologram.transform.position, pos) < 1.1)
                {
                    goto NextRestriction;
                }
                for (int i = 0; i < 4; i++)
                {
                    PlaceableItem searcheditem = Getitematpos(pos + directions[i]);
                    if (searcheditem != null)
                    {
                        if (searcheditem.mySO == restriction.card)
                        {
                            direction = directions[i];
                            break;
                        }
                    }
                }
                if (direction == Vector3.zero)
                {
                    result = false;
                }
            NextRestriction:; 
            }
            else if (Getitematpos(pos + restriction.direction).mySO != restriction.card)
            {
                result = false;
            }
        }
        //need to change
        return result;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 MouseworldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 hologramposraw = tilemaptoplace.layoutGrid.CellToWorld(tilemaptoplace.layoutGrid.WorldToCell(MouseworldPosition)) + new Vector3(0.5f, 0.5f);
        Vector3 hologrampos = hologramposraw;

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
             temppos = hologramposraw;
                }
        // if(MouseworldPosition.x > temppos.x - 1f && MouseworldPosition.x < temppos.x + 1f && MouseworldPosition.y > temppos.y - 1f && MouseworldPosition.y< temppos.y + 1f)
        // {
        //    holdingenemy = true;
        // }
       
        if (holding)
        {
            if (!holdingenemy)
            {
                hologrampos.x = Mathf.Clamp(hologrampos.x, arenaboundslow.position.x, arenaboundhigh.position.x);
                hologrampos.y = Mathf.Clamp(hologrampos.y, arenaboundslow.position.y, arenaboundhigh.position.y);

              hologrampos.x = Mathf.Clamp(hologrampos.x, temppos.x - 1f, temppos.x + 1f);
              hologrampos.y = Mathf.Clamp(hologrampos.y, temppos.y - 1f, temppos.y + 1f);

            }
            else
            {
                hologrampos.x = Mathf.Clamp(hologrampos.x, enemyarenaboundslow.position.x, enemyarenaboundhigh.position.x);
                hologrampos.y = Mathf.Clamp(hologrampos.y, enemyarenaboundslow.position.y, enemyarenaboundhigh.position.y);

               hologrampos.x = Mathf.Clamp(hologrampos.x, temppos.x - 1f, temppos.x + 1f);
              hologrampos.y = Mathf.Clamp(hologrampos.y, temppos.y - 1f, temppos.y + 1f);
            }
        }
       

        hologram.transform.position = hologrampos;

        if (toplace == null)
        {
            hologram.GetComponent<SpriteRenderer>().sprite = null;
        }


        holding = false;
        if (Input.GetKey(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Mouse0))
        {
            holding = true;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                originalposition = hologram.transform.position;
                var unit = Getitematpos(hologramposraw);
            
                //print(Getitematpos(originalposition));
                if (unit != null)
                {
                    if (unit.tag == "Enemy"|| unit.tag == "EnemyObject")
                    {
                        holdingenemy = true;
                    }
                    else
                    {
                        holdingenemy = false;
                    }
                    if (unit.GetComponent<PlaceableItem>().isActiveAndEnabled && unit.mySO.ispickable && GameObject.FindWithTag("projectile") == null && !unit.locked && !gameObject.GetComponent<Turnmanager>().turnRunning && gameObject.GetComponent<Turnmanager>().usedmoves != gameObject.GetComponent<Turnmanager>().movesperturn[gameObject.GetComponent<Turnmanager>().CurrentTurn])
                    {                   
                    toplace = unit.mySO;
                        hologram.GetComponent<SpriteRenderer>().sprite = unit.gameObject.GetComponent<SpriteRenderer>().sprite;
                        hologram.GetComponent<SpriteRenderer>().flipX = unit.gameObject.GetComponent<SpriteRenderer>().flipX;
                        hologram.GetComponent<SpriteRenderer>().flipY = unit.gameObject.GetComponent<SpriteRenderer>().flipY;
                        storedneighbors = unit.neighbors;
                        if(unit.GetComponent<Redirect>() != null)
                        {
                            slingshotdirection = unit.GetComponent<Redirect>().redirectDirection;
                        }
                        tag = unit.tag;
                    unit.Remove();
                    Destroy(unit.gameObject);
                    }
                }


            }

            canplace = true;
            removing = false;
            if (toplace != null)
            {
                if (Getitematpos(hologram.transform.position) != null  )
                {
                    if (Getitematpos(hologram.transform.position).GetComponent<PlaceableItem>().isActiveAndEnabled)
                    {
                        canplace = false;
                    }
                    else
                    {
                        removing = true;
                    }

                    hologram.GetComponent<SpriteRenderer>().color = new Vector4(0, 0, 0, 0);
                }
                else
                {
                    hologram.GetComponent<SpriteRenderer>().color = new Vector4(0, 0, 0, 0.5f);

                }
                foreach (var item in storedneighbors)
                {
                    if (item != null)
                    {
                        if (!calculaterestrictions(item.GetComponent<PlaceableItem>().mySO, item.transform.position))
                        {
                            canplace = false;
                        }
                        
                    }
                }

                if (!calculaterestrictions(toplace, hologram.transform.position))
                {
                    canplace = false;
                }

              

                if (Input.GetKeyUp(KeyCode.Mouse0) && canplace)
                {
                    if (removing)
                    {
                        Destroy(Getitematpos(hologram.transform.position).gameObject);
                    }
                    GameObject Justplaced =   Instantiate(toplace.Prefab, hologram.transform.position, Quaternion.Euler(Vector3.zero), GameObject.FindGameObjectWithTag("Level").transform);
                 Justplaced.tag = tag;
                    if (Justplaced.GetComponent<Redirect>() != null)
                    {
                         Justplaced.GetComponent<Redirect>().redirectDirection = slingshotdirection ;
                    }
                  
                    toplace = null;
                    if(hologram.transform.position != originalposition)
                    {
                        gameObject.GetComponent<Turnmanager>().usedmoves += 1;
                    }

                    BroadcastMessage("OnboardUpdate");
                }
                else if (Input.GetKeyUp(KeyCode.Mouse0) && !canplace)
                {
                   GameObject Justplaced = Instantiate(toplace.Prefab, originalposition, Quaternion.Euler(Vector3.zero), GameObject.FindGameObjectWithTag("Level").transform);
                    Justplaced.tag = tag;
                    if (Justplaced.GetComponent<Redirect>() != null)
                    {
                        Justplaced.GetComponent<Redirect>().redirectDirection = slingshotdirection;
                    }
                    toplace = null;
                    BroadcastMessage("OnboardUpdate");
                }
            }

        }
    }
}
