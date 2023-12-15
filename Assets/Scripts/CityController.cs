using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityController : MonoBehaviour
{
    public bool cuadricula = true;
    public List<Transform> buildingPositions = new List<Transform>();

    private List<Vector3> _positionsAvailable = new List<Vector3>();
    private int _distance = 2;
    private int _radius = 3;

    private Monument[,] monuments = new Monument[3, 3];

    public static CityController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (cuadricula)
        {
            Events.OnBuildingFinished += SetBuilding;
            GeneratePositions();
        }
        else
        {
            Events.OnMonumentFinished += SetNewMonument;
        }
    }

    #region Cuadricula

    private void GeneratePositions()
    {
        for (int i = 0; i <= _radius * 2; i++)
        {
            for (int j = 0; j <= _radius * 2; j++)
            {
                _positionsAvailable.Add(buildingPositions[0].position + Vector3.right * _distance * (-3 + i) + Vector3.forward * (-3 + j));
            }
        }


    }

    public void SetBuilding(GameObject building)
    {
        int randomIndex = Random.Range(0, _positionsAvailable.Count);
        building.transform.position = _positionsAvailable[randomIndex];
        _positionsAvailable.RemoveAt(randomIndex);
    }

    #endregion

    public void SetNewMonument(Monument monument)
    {
        monument.transform.position = NextPosition();
    }

    private Vector3 NextPosition()
    {
        int iIndex = 0;
        int jIndex = 0;

        bool foundEmpty = false;

        for (int i = 0; i < monuments.GetLength(1); i++)
        {
            for (int j = 0; j < monuments.GetLength(0); j++)
            {
                if (monuments[i, j] != null)
                {
                    continue;
                }
                else
                {
                    iIndex = i;
                    jIndex = j;
                    foundEmpty = true;
                    break;
                }
            }

            if (foundEmpty)
            {
                break;
            }
        }

        float iDistance = 0;
        float jDistance = 0;

        for (int i = 0; i < iIndex; i++)
        {
            iDistance += monuments[i, jIndex].ground.localScale.z;
        }
        for (int j = 0; j < jIndex; j++)
        {
            jDistance += monuments[iIndex, j].ground.localScale.x;
        }

        return new Vector3(iDistance, 0, jDistance);

    }
}
