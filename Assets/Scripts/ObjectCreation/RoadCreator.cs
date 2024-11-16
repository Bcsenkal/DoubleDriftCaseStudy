using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class RoadCreator : MonoBehaviour
{
    private float roadLength;
    private float nextPlacementZ;
    private bool canCreateRoad;
    private float nextRoadCreation;

    private Transform player;
    private List<Road> roads;
    [SerializeField]private int initialRoadsToCreate;
    private void Awake()
    {
        CreateInitialRoads();
    }
    void Start()
    {
        EventManager.Instance.OnCreateRoad += CreateRoad;
        EventManager.Instance.OnSendPlayerData += OnSetPlayer;
        canCreateRoad = true;
        nextRoadCreation = roads[2].transform.position.z;
    }

    private void Update() 
    {
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
        oldestRoad.Place(new Vector3(0, 0, nextPlacementZ));
        nextPlacementZ += oldestRoad.Bounds.z;
        roads.RemoveAt(0);
        roads.Add(oldestRoad);
        nextRoadCreation = roads[2].transform.position.z;
        canCreateRoad = true;
    }

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

    private void OnSetPlayer(Transform p)
    {
        player = p;
    }
    
}
