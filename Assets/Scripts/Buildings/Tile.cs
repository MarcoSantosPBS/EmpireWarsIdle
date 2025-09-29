using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Transform buildingSpawnPoint;
    public bool IsEmpty { get; private set; }

    private void Awake()
    {
        IsEmpty = true;
    }

    public void SpawnBuilding(ResourceProducer producer)
    {
        Instantiate(producer, buildingSpawnPoint.transform.position, Quaternion.identity, buildingSpawnPoint.transform);
        IsEmpty = false;
    }

}
