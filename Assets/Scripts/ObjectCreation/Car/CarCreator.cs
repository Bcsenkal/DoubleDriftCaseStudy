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
    public bool IsGameStarted { get; set; }

    void Start()
    {
        CacheEvents();
    }

    private void Update()
    {
        if(!IsGameStarted) return;
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
            var spawnPositionZ = latestVehicle == null ? player.position.z + 65f : latestVehicle.position.z + 25f;
            var spawnPosition = new Vector3(spawnX, 0, spawnPositionZ);
            car.Spawn(spawnPosition, IsGameStarted);
            lastSpawn = car.transform;
            
            
        }
        var coinSpawnRoll = Random.Range(0,10);
        if(coinSpawnRoll <7)
        {
            latestVehicle = lastSpawn;
            return;
        }

        var coin = CoinPool.instance.GetPooledObject() as Collectible;
        var coinSpawnX = availableSpawnX[Random.Range(0, availableSpawnX.Length)];
        var coinSpawnZ = latestVehicle == null ? player.position.z + 65f : latestVehicle.position.z + 25f;
        coin.transform.position = new Vector3(coinSpawnX, 0, coinSpawnZ);
        coin.Spawn(player);
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
        SpawnInitialVehicles();
        
    }

    public void CacheEvents()
    {
        Managers.EventManager.Instance.OnSendPlayerData += SetPlayer;
        Managers.EventManager.Instance.ONLevelEnd += GameOver;
        Managers.EventManager.Instance.ONLevelStart += GameStart;
    }

    public void GameOver(bool isSuccess)
    {
        IsGameOver = true;
    }

    public void GameStart()
    {
        if(IsGameStarted) return;
        canSpawn = true;
        nextSpawn = Time.time + spawnFrequency;
        IsGameStarted = true;
    }
}
