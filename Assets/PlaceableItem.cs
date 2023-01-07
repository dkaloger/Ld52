using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class PlaceableItem : MonoBehaviour
{
    public Tile myTile;
    public string Tilemapname; 
    [HideInInspector]
    public Tilemap myTilemap;
    public CardSO mySO;
    public GameObject[] neighbors;
    int i ;
    //public Vector3[] directions;
    // Start is called before the first frame update
    void Start()
    {
        myTilemap = GameObject.Find(Tilemapname).GetComponent<Tilemap>();
        transform.position = myTilemap.layoutGrid.WorldToCell(transform.position) + new Vector3(0.5f, 0.5f, 0);
        myTilemap.SetTile(myTilemap.layoutGrid.WorldToCell(transform.position), myTile);
        neighbors = Getneighbors(transform.position);
    }

    GameObject[] Getneighbors(Vector3 Pos)
    {
        i = 0;
        GameObject[] tempresult = new GameObject[4];
        foreach (PlaceableItem unit in GameObject.FindObjectsOfType<PlaceableItem>())
        {

            if (Vector3.Distance(unit.transform.position, Pos) < 1.1&& unit.gameObject !=gameObject)
            {
                
                tempresult[i] = unit.gameObject;
                i++;
            }

        }
        return tempresult;
    }
        public void OnboardUpdate()
    {
        //print("received");
        neighbors = Getneighbors(transform.position);
    }
    public void Remove()
    {
        myTilemap.SetTile(myTilemap.layoutGrid.WorldToCell(transform.position), null);
    }

}
