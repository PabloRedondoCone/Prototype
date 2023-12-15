using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JointMovement : MonoBehaviour
{

    public Vector3 Target { get; set; }
    public Vector3 InitialTarget { get; set; }


    private void Awake()
    {
        Events.OnLaunchPiece += SetJointHeightTarget;
        Events.OnBuildingFinished += ResetPosition;
        Target = transform.position;
        InitialTarget = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Target, Time.deltaTime / 0.2f);
    }

    private void SetJointHeightTarget(float pieceHeight)
    {
        Target += Vector3.up * pieceHeight;
    }

    private void ResetPosition(GameObject building)
    {
        Target = InitialTarget;
    }

}
