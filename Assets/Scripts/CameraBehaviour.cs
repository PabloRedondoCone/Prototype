using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraBehaviour : MonoBehaviour
{

    public Transform target;

    private Vector3 selfieStick;

    private void Awake()
    {
        selfieStick = transform.position - target.position;

    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + selfieStick, Time.deltaTime / 0.8f);
    }

}
