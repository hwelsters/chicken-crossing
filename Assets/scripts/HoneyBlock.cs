using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyBlock : MovingObject
{
    public static HashSet<HoneyBlock> allHoneyBlocks = new HashSet<HoneyBlock>();
    
    public static Vector2[] pushPositions = new Vector2[] 
    {
        new Vector2(1, 0), new Vector2(-1, 0), 
        new Vector2(0, 1), new Vector2(0, -1)
    };

    //A TOTAL MESS
    //CLEAN UP THIS MESS NOEL OF THE FUTURE
    protected override bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        if (base.Move(xDir, yDir, out hit))
        {
            allHoneyBlocks.Add(this);
            foreach (Vector2 position in pushPositions)
            {
                if ((position.x == xDir && position.y == yDir) || (position.x == -xDir && position.y == -yDir)) continue;
                Collider2D collider2D = Physics2D.OverlapCircle((Vector2)transform.position + position, 0.1f);
                if (collider2D != null && !collider2D.CompareTag("Other") && !collider2D.CompareTag("Untagged"))
                {
                    HoneyBlock honeyBlock = collider2D.GetComponent<HoneyBlock>();
                    if (honeyBlock != null) 
                    {
                        if (allHoneyBlocks.Contains(honeyBlock)) continue;
                    }
                    MovingObject movingObject = collider2D.GetComponent<MovingObject>();
                    movingObject.AttemptMove(xDir, yDir);
                }
            }
            return true;
        }
        return false;
    }
}
