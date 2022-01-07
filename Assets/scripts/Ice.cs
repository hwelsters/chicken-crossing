using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MovingObject
{
    //OPTIMIZE
    //MAKE ICE BLOCK PUSHABLE?
    protected override bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector3 start = transform.position;
        Vector3 end = start + new Vector3(xDir, yDir, 0);

        LineCast(out hit, start, end);

        //OKAY, THIS IS A FUCKING MESS
        //BEEP BEEP I'M A BEEP
        if (Push(hit, xDir, yDir)) 
        {
            inverseMoveTime = 1f / moveTime;
            StartCoroutine(SmoothMovement(end));
            
            PlaySound(moveSound);
            
            return true;
        }

        end = start + new Vector3(xDir, yDir, 0) * 10;

        LineCast(out hit, start, end);

        int distance = 10;

        if (hit.transform != null)
        {
            Vector2 hitPosition = new Vector2((int) Math.Round(hit.point.x), (int) Math.Round(hit.point.y));

            end = hitPosition;
            
            distance = (int)(end - transform.position).magnitude;
        }
        
        inverseMoveTime = 1f / moveTime * (distance);
        
        PlaySound(moveSound);

        StartCoroutine(SmoothMovement(end));

        return false;
    }
}
