using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monument : MonoBehaviour
{

    public Transform ground;

    public void SetGroundSize(Vector2 size)
    {
        float max = size.x;
        if (max < size.y)
        {
            max = size.y;
        }

        ground.transform.localScale = new Vector3(1.5f + max, 1, 1.5f + max);
    }

}
