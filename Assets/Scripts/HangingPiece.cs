using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingPiece : MonoBehaviour
{

    public HingeJoint hingeJoint;
    public AnimationClip clip;

    public IEnumerator WaitForAnimation()
    {
        float timer = clip.length;

        while (timer >= 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
    }

}
