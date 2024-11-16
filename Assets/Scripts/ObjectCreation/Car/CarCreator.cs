using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CarCreator : MonoBehaviour, IState
{
    [SerializeField]private Texture2D[] carTextures;
    [SerializeField]private float speed;
    [SerializeField]private readonly float[] availableSpawnX = new float[3]{-2.15f, 0f, 2.15f};
    private Transform player;
    [SerializeField]private float spawnFrequency = 1f;
    private float nextSpawn;
    private bool canSpawn;
    private int previousSpawnIndex;
    private Transform latestVehicle;

    public bool IsGameOver { get; set; }

    void Start()
    {
        CacheEvents();
    }

    private void Update()
    {
        if(IsGameOver) return;
        if(!canSpawn) return;
        if(nextSpawn > Time.time) return;

        SpawnNextVehicleBatch();

        nextSpawn = Time.time + spawnFrequency;
    }

    private void SpawnNextVehicleBatch()
    {
        previousSpawnIndex = 0;
        var vehiclesToSpawn = Random.Range(1,3);
        Transform lastSpawn = null;
        for(int i = 0; i < vehiclesToSpawn; i++)
        {
            var car = CarPool.instance.GetPooledObject() as AICar;
            car.SetPlayer(player);
            car.SetTexture(carTextures[Random.Range(0, carTextures.Length)]);
            car.SetSpeed(speed);
            
            var spawnXIndex = Random.Range(0, availableSpawnX.Length);
            if(i > 0)
            {
                spawnXIndex = spawnXIndex == previousSpawnIndex ? spawnXIndex + 1 == availableSpawnX.Length ? 0 : spawnXIndex + 1 : spawnXIndex;
            }
            previousSpawnIndex = spawnXIndex;
            var spawnX = availableSpawnX[spawnXIndex];
            var spawnPositionZ = latestVehicle == null ? player.position.z + 50f : latestVehicle.position.z + 10f;
            var spawnPosition = new Vector3(spawnX, 0, spawnPositionZ + 15f);
            car.Spawn(spawnPosition);
            lastSpawn = car.transform;
            
        }
        latestVehicle = lastSpawn;

    }

    private void SpawnInitialVehicles()
    {
        for(int i = 0; i < 5; i++)
        {
            SpawnNextVehicleBatch();
        }
    }

    void SetPlayer(Transform playerTransform)
    {
        player = playerTransform;
        canSpawn = true;
        SpawnInitialVehicles();
        nextSpawn = Time.time + spawnFrequency;
    }

    public void CacheEvents()
    {
        Managers.EventManager.Instance.OnSendPlayerData += SetPlayer;
        Managers.EventManager.Instance.ONLevelEnd += GameOver;
    }

    public void GameOver(bool isSuccess)
    {
        IsGameOver = true;
    }
}
