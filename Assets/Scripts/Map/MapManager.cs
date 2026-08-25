using UnityEngine;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private float mapBoundarySize = 1000f;
    [SerializeField] private GameObject hazardZone;
    
    private static MapManager instance;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public Vector3 GetRandomSpawnPoint()
    {
        if (spawnPoints.Count == 0)
        {
            Debug.LogError("No spawn points defined!");
            return Vector3.zero;
        }
        
        return spawnPoints[Random.Range(0, spawnPoints.Count)].position;
    }
    
    public bool IsWithinMapBounds(Vector3 position)
    {
        return Mathf.Abs(position.x) < mapBoundarySize && Mathf.Abs(position.z) < mapBoundarySize;
    }
    
    public void ExpandHazardZone(float newSize)
    {
        if (hazardZone != null)
        {
            hazardZone.transform.localScale = new Vector3(newSize, newSize, newSize);
        }
    }
}
