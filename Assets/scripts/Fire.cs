using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : TurnBased
{
    //QUESTIONABLE CODE, TO REVISE
    //I'M CODING OUT OF MY ASS HERE
    //ACTUALLY. NOT THAT QUESTIONABLE
    public override void TurnUpdate()
    {
        Transform overlapTransform = CheckOverlap<MovingObject>(transform.position);
        if (overlapTransform != null) 
        {
            overlapTransform.GetComponent<MovingObject>().DestroySelf();
            if (overlapTransform.GetComponent<Ice>() != null) 
            {
                DestroySelf();
            }
        }
    }
}
