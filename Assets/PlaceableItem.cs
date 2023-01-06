using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class PlaceableItem : MonoBehaviour
{
    public Tile myTile;
    public Tilemap myTilemap;
    // Start is called before the first frame update
    void Start()
    {
        myTilemap.SetTile(myTilemap.layoutGrid.WorldToCell(transform.position), myTile);
    }
    public void Remove()
    {
        myTilemap.SetTile(myTilemap.layoutGrid.WorldToCell(transform.position), null);
    }

}
