using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] roomPrefabs;
    [SerializeField] private Transform player;
    [SerializeField] private float roomWidth = 16.17f;
    [SerializeField] private int roomsAhead = 2;

    private float nextSpawnX;
    private float lastDespawnX;

    private void Start()
    {
        nextSpawnX = 47.91f;
        lastDespawnX = -roomWidth;
    }

    private void Update()
    {
        if (player.position.x + (roomsAhead * roomWidth) > nextSpawnX)
        {
            SpawnRoom();
        }

        if (player.position.x - (roomsAhead * roomWidth) > lastDespawnX + roomWidth)
        {
            lastDespawnX += roomWidth;
        }
    }

    private void SpawnRoom()
    {
        int randomIndex = Random.Range(0, roomPrefabs.Length);
        Instantiate(
            roomPrefabs[randomIndex],
            new Vector3(nextSpawnX, -1.7f, 0),
            Quaternion.identity
        );
        nextSpawnX += roomWidth;
    }
}