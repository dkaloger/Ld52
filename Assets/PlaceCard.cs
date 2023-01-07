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
    public Vector3 arenaboundslow, arenaboundhigh;
    public bool holding;
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
        hologrampos.x = Mathf.Clamp(hologrampos.x+0.5f, arenaboundslow.x, arenaboundhigh.x)-0.5f;
        hologrampos.y = Mathf.Clamp(hologrampos.y + 0.5f, arenaboundslow.y, arenaboundhigh.y)-0.5f;
        hologram.transform.position = hologrampos;
        if (toplace == null)
        {
            hologram.GetComponent<SpriteRenderer>().sprite = null;
        }
        else
        {
            hologram.GetComponent<SpriteRenderer>().sprite = toplace.Sprite;
        }


        holding = false;
        if (Input.GetKey(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Mouse0))
        {
            holding = true;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                originalposition = hologram.transform.position;
                var unit = Getitematpos(originalposition);
                if (unit != null)
                {
                    if (unit.mySO.ispickable && !unit.locked && !gameObject.GetComponent<Turnmanager>().turnRunning)
                    {                   
                    toplace = unit.mySO;
                    storedneighbors = unit.neighbors;
                    unit.Remove();
                    Destroy(unit.gameObject);
                    }
                }


            }

            canplace = true;

            if (toplace != null)
            {
                if (tilemaptoplace.GetTile(tilemaptoplace.layoutGrid.WorldToCell(hologram.transform.position)) != null)
                {
                    canplace = false;
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
                    Instantiate(toplace.Prefab, hologram.transform.position, Quaternion.Euler(Vector3.zero), GameObject.FindGameObjectWithTag("Level").transform);
                    toplace = null;
                    BroadcastMessage("OnboardUpdate");
                }
                else if (Input.GetKeyUp(KeyCode.Mouse0) && !canplace)
                {
                    Instantiate(toplace.Prefab, originalposition, Quaternion.Euler(Vector3.zero), GameObject.FindGameObjectWithTag("Level").transform);
                    toplace = null;
                    BroadcastMessage("OnboardUpdate");
                }
            }

        }
    }
}
