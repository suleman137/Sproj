using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Watch this YouTube video for more info
// https://youtu.be/HIsEqKPoJXM


public class spawn : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    private Transform playerTransform;
    // spawnZ is the distance between the camera and the spawning of objects. 
    // Greater the value, further the objects would be from the camera.
    private float spawnZ = 0f;

    // tileLength is the distance between each spawn. 
    // Greater the tileLength, further the spawned objects would be from each other.
    // For making connected platforms, tileLength should be equal to the length of the platform.
    private float tileLength = 100f;

    // Number of spawns on screen
    private int tilesOnScreen = 1;
    
    private int lastPrefabIndex = 0;

    // smaller the safeZone value, earlier the tiles delete from the back
    private float safeZone = 0f;
    private List<GameObject> activeTiles;

    // Start is called before the first frame update
    private void Start()
    {
        GameObject GameTerrain = GameObject.Find("Terrain");
        Vector3 Dimensions;
        Dimensions = GameTerrain.GetComponent<Terrain>().terrainData.size;
        tileLength = Dimensions.z;
        Debug.Log(tileLength);

        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        for (int i=0; i<tilesOnScreen; i++)
        {
            // spawn first 2 tiles from index 0 of the array
            /*if(i<2)
            {
                spawnTile(0);
            }
            else
            {
                spawnTile();
            }*/

            // spawn random tiles from the start
            spawnTile();
        }

    }

    // Update is called once per frame
    private void Update()
    {
        if (playerTransform.position.z > (spawnZ - tilesOnScreen*tileLength))
        {
            spawnTile();
        }
        /*if ((playerTransform.position.z - safeZone) > (spawnZ - tilesOnScreen * tileLength)-5)
        {
            deleteTile();
        }*/
       // Debug.Log(("A " , playerTransform.position.z - safeZone));
        //Debug.Log(("B ", spawnZ - tilesOnScreen * tileLength));

        if(activeTiles.Count > 2)
        {
            deleteTile();
        }
    }

    private void spawnTile(int prefabIndex = -1)
    {
        // always spawns object at index 0
        // go = Instantiate(tilePrefabs[0]) as GameObject;

        // spawns random objects
        GameObject go;
        
        // if nothing has been passed as prefabIndex, use RandompPrefabIndex. Else use the passed prefabIndex to generate game objects
        if (prefabIndex == -1)
        {
            go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        }
        else
        {
            go = Instantiate(tilePrefabs[prefabIndex]) as GameObject;
        }

        // makes the spawned object a child of the tile manager in the hierarchy of the editor
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(go);

        
    }

    private void deleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
        if (tilePrefabs.Length <= 1)
            return 0;

        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }
        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
}


