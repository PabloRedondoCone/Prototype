using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{

    public static Action<float> OnLaunchPiece;
    public static Action<GameObject> OnBuildingFinished;
    public static Action<Monument> OnMonumentFinished;

}
