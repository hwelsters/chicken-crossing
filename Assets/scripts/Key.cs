using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MovingObject
{
    public override void TurnUpdate()
    {
        Transform overlapTransform = CheckOverlap<Player>(transform.position);
        if (overlapTransform != null) DestroySelf();
    }
}
