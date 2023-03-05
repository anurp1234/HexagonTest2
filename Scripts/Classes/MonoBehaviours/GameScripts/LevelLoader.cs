using System.Collections.Generic;
using UnityEngine;
public struct GridPosition
{
    public int xPos;
    public int zPos;
    public GridPosition(int xPos, int zPos)
    {
        this.xPos = xPos;
        this.zPos = zPos;
    }
}
public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    List<GameObject> gemsPrefabs;

    [SerializeField]
    List<GameObject> obstaclePrefabs;

    //All these parameters can be pushed to a scriptable object
    [SerializeField]
    int minGemCount = 5 ;

    [SerializeField]
    int maxGemCount = 10;

    [SerializeField]
    int minObstaclesCount = 10;

    [SerializeField]
    int maxObstaclesCount = 20;

    [SerializeField]
    int spawnRangeInX = 10;

    [SerializeField]
    int spawnRangeInZ = 10;

    [SerializeField]
    int zScaler = 1;

    [SerializeField]
    int xScaler = 1;

    List<GridPosition> gridPositions = new List<GridPosition>();
    void Start()
    {
        PopulatePositionGrid(spawnRangeInX, spawnRangeInZ);
        SpawnJewels();
        SpawnObstacles();
    }

    //This populates a set of x and y grid positions, which will be selected and removed at random from the list
    //This is done to avoid collisions among the obstacles and the gems
    void PopulatePositionGrid(int maxX, int maxZ)
    {
        for (int x = 0; x < maxX; x++)
        {
            for (int z = 0; z < maxZ; z++)
            {
                gridPositions.Add(new GridPosition(x, z));
            }
        }
    }
    void SpawnJewels()
    {
        GameObject jewelsParentGO = new GameObject("JewelsParent");
        SpawnObjects(jewelsParentGO.transform, gemsPrefabs, gridPositions, minGemCount, maxGemCount);
    }

    void SpawnObstacles()
    {
        GameObject obstaclesParentGO = new GameObject("ObstaclesParent");
        SpawnObjects(obstaclesParentGO.transform, obstaclePrefabs, gridPositions, minObstaclesCount, maxObstaclesCount);
    }
    void SpawnObjects(Transform parent, List<GameObject> spawnPrefabs, List<GridPosition> gridPosList, int minCount, int maxCount)
    {
        int spawnCount = Random.Range(minCount, maxCount);
        int maxIdx = spawnPrefabs.Count - 1;
        for (int i = 0; i < spawnCount; i++)
        {
            int gridPos = Random.Range(0, gridPosList.Count);
            GridPosition pos = gridPosList[gridPos];
            gridPosList.RemoveAt(gridPos);
            float spawnXPos = pos.xPos * xScaler;
            float spawnZPos = pos.zPos * zScaler;

            int spawnIdx = Random.Range(0, maxIdx);
            GameObject prefabToSpawn = spawnPrefabs[spawnIdx];
            float spawnYPos = prefabToSpawn.transform.position.y;

            Quaternion orginalRotation = prefabToSpawn.transform.rotation;

            GameObject.Instantiate(spawnPrefabs[spawnIdx], new Vector3(spawnXPos, spawnYPos, spawnZPos), orginalRotation, parent);
        }
    }
}
