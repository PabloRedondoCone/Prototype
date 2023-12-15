using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PieceController : MonoBehaviour
{
    public Rigidbody joint;
    public Transform hangingPosition;
    public List<HangingPiece> blockPrefabs = new List<HangingPiece>();
    public List<HangingPiece> roofPrefabs = new List<HangingPiece>();
    public GameObject emptyPrefab;
    public int buildingMaxHeight = 3;
    public int buildingMinHeight = 3;

    private HangingPiece _newHangingPiece;

    private int _currentHeight;
    private int _currentMaxHeight;
    private List<GameObject> _generatedPieces = new List<GameObject>();

    private void Awake()
    {
        SetCurrentMaxHeight();
        NextPiece();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            NextPiece();
        }
    }

    #region Building-related

    private void ResetBuilding()
    {
        _currentHeight = 0;
        SetCurrentMaxHeight();
        _generatedPieces.Clear();

    }

    private void SetCurrentMaxHeight()
    {
        _currentMaxHeight = Random.Range(buildingMinHeight, buildingMaxHeight + 1);
        if (_currentMaxHeight >= 10)
        {
            _currentMaxHeight = 2;
        }
    }

    #endregion

    public void NextPiece()
    {
        if (_currentHeight >= _currentMaxHeight)
        {
            FinishBulding();
            return;
        }


        if (_currentHeight == _currentMaxHeight - 1)
        {
            _newHangingPiece = Instantiate(roofPrefabs[Random.Range(0, roofPrefabs.Count)]);
        }
        else
        {
            _newHangingPiece = Instantiate(blockPrefabs[Random.Range(0, blockPrefabs.Count)]);
        }

        _newHangingPiece.hingeJoint.connectedBody = joint;
        _newHangingPiece.transform.position = hangingPosition.position;
        _newHangingPiece.transform.localRotation = hangingPosition.rotation;

        _currentHeight++;
    }

    //public void LaunchPiece()
    //{
    //    Debug.Log("Launched");
    //    _newHangingPiece.hingeJoint.gameObject.SetActive(false);
    //}
    public void LaunchPiece(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Launched");
            _newHangingPiece.hingeJoint.breakTorque = 0;
            Events.OnLaunchPiece?.Invoke(_newHangingPiece.GetComponentInChildren<MeshRenderer>().bounds.size.y);
            _generatedPieces.Add(_newHangingPiece.gameObject);
        }
    }

    public void FinishBulding()
    {
        GameObject newAsset = Instantiate(emptyPrefab);

        for (int i = 0; i < _generatedPieces.Count; i++)
        {
            _generatedPieces[i].transform.SetParent(newAsset.transform);
        }

        PrefabUtility.SaveAsPrefabAsset(newAsset, "Assets/Resources/StoredBuildingColliders.prefab");

        for (int i = 0; i < _generatedPieces.Count; i++)
        {
            Collider[] colliders = _generatedPieces[i].GetComponentsInChildren<Collider>();
            for (int j = 0; j < colliders.Length; j++)
            {
                Destroy(colliders[j]);
            }

            Rigidbody[] rigidbodies = _generatedPieces[i].GetComponentsInChildren<Rigidbody>();
            for (int j = 0; j < rigidbodies.Length; j++)
            {
                Destroy(rigidbodies[j]);
            }
        }

        StartCoroutine(SaveNoColliderAsset(newAsset));
    }

    /// <summary>
    /// Destroy Component Is Not Instantaneous
    /// </summary>
    /// <returns></returns>
    private IEnumerator SaveNoColliderAsset(GameObject newAsset)
    {
        yield return new WaitForFixedUpdate();
        PrefabUtility.SaveAsPrefabAsset(newAsset, "Assets/Resources/StoredBuilding.prefab");

        yield return new WaitForFixedUpdate();

        Events.OnBuildingFinished(Instantiate(Resources.Load<GameObject>("StoredBuilding")));

        Destroy(newAsset);

        ResetBuilding();
    }

}
