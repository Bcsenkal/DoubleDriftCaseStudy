using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class RoadCreator : MonoBehaviour,IState
{
    private float nextPlacementZ;
    private bool canCreateRoad;
    private float nextRoadCreation;

    private Transform player;
    private List<Road> roads;
    [SerializeField]private int initialRoadsToCreate;

    public bool IsGameOver{get; set;}
    public bool IsGameStarted{get; set;}


    private void Awake()
    {
        CreateInitialRoads();
        
    }
    void Start()
    {
        CacheEvents();
        nextRoadCreation = roads[2].transform.position.z;
    }

    private void Update() 
    {
        if(!IsGameStarted) return;
        if(IsGameOver) return;
        if(player == null) return;
        if(!canCreateRoad) return;
        if (player.position.z > nextRoadCreation)
        {
            CreateRoad();
        }
    }

    // Place oldest road at the end for endless road
    private void CreateRoad()
    {
        canCreateRoad = false;
        var oldestRoad = roads[0];
        oldestRoad.Place(new Vector3(-0.18f, 0, nextPlacementZ));
        nextPlacementZ += oldestRoad.Bounds.z;
        roads.RemoveAt(0);
        roads.Add(oldestRoad);
        nextRoadCreation = roads[2].transform.position.z;
        canCreateRoad = true;
    }
    //Create initial roads for the start of the game
    private void CreateInitialRoads()
    {
        roads = new List<Road>();
        for(int i = 0; i < initialRoadsToCreate; i++)
        {
            var road = RoadPool.instance.GetPooledObject() as Road;
            road.Place(new Vector3(0, 0, nextPlacementZ));
            nextPlacementZ += road.Bounds.z;
            roads.Add(road);
        }
    }

    private void SetPlayer(Transform p)
    {
        player = p;
    }

    public void CacheEvents()
    {
        EventManager.Instance.ONLevelEnd += GameOver;
        EventManager.Instance.ONLevelStart += GameStart;
        EventManager.Instance.OnCreateRoad += CreateRoad;
        EventManager.Instance.OnSendPlayerData += SetPlayer;
    }
    
    public void GameOver(bool isSuccess)
    {
        IsGameOver = true;
    }

    public void GameStart()
    {
        if(IsGameStarted) return;
        IsGameStarted = true;
        canCreateRoad = true;
    }
    
}
