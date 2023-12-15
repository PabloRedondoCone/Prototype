using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityController : MonoBehaviour
{

    public List<Transform> buildingPositions = new List<Transform>();

    private List<Vector3> _positionsAvailable = new List<Vector3>();
    private int _distance = 2;
    private int _radius = 3;

    public static CityController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        Events.OnBuildingFinished += SetBuilding;
        GeneratePositions();
        //InitializePositionsAvailable();
    }

    private void GeneratePositions()
    {
        List<Vector3> columnPositions = new List<Vector3>();
        for (int i = 0; i <= _radius * 2; i++)
        {
            for (int j = 0; j <= _radius * 2; j++)
            {
                _positionsAvailable.Add(buildingPositions[0].position + Vector3.right * _distance * (-3 + i) + Vector3.forward * (-3 + j));
            }
        }


    }

    private void InitializePositionsAvailable()
    {
        for (int i = 0; i < buildingPositions.Count; i++)
        {
            _positionsAvailable.Add(buildingPositions[i].position);
        }
    }

    public void SetBuilding(GameObject building)
    {
        int randomIndex = Random.Range(0, _positionsAvailable.Count);
        building.transform.position = _positionsAvailable[randomIndex];
        _positionsAvailable.RemoveAt(randomIndex);
    }
}
