using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : TurnBased
{
    [Header("Moving Object")]
    
    public LayerMask blockingLayer;

    protected float moveTime = 0.1f;
    protected float inverseMoveTime;
    
    private BoxCollider2D boxCollider;

    [Header("Sounds")]

    [SerializeField] protected AudioClip moveSound;
    [SerializeField] protected AudioClip destroySound; 

    public static bool soundMade = false;
    private AudioSource audioSource;

    protected override void Start()
    {
        base.Start();
        boxCollider = GetComponent<BoxCollider2D>();
        inverseMoveTime = 1f / moveTime;

        audioSource = GetComponent<AudioSource>();
    }

    public virtual bool AttemptMove(int xDir, int yDir)
    {
        RaycastHit2D hit;
        return Move(xDir, yDir, out hit);
    }

    protected virtual bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

        LineCast(out hit, start, end);

        //OKAY, MIGHT BE A BIG FUCK UP
        //BUT IT'S MORE ELEGANT THAN USING AN ENUM
        if (hit.transform == null || Push (hit, xDir, yDir))
        {
            StopAllCoroutines();
            StartCoroutine(SmoothMovement(end));
            if (!soundMade)
            {
                PlaySound(moveSound);
                soundMade = true;
            }
            return true;
        }
        return false;
    }

    protected IEnumerator SmoothMovement(Vector3 end)
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, end, inverseMoveTime * Time.deltaTime);
            transform.position = newPosition;
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            yield return null;
        }

        transform.position = new Vector3((int) end.x, (int) end.y, 0);
    }

    protected virtual bool Push (RaycastHit2D hit, int xDir, int yDir)
    {
        if (hit.transform != null)
        {
            MovingObject movingObject = hit.transform.GetComponent<MovingObject>();
            if (movingObject != null)
            {
                return movingObject.AttemptMove(xDir, yDir);
            }

            //VERY VERY VERY VERY QUESTIONABLE CODE
            if (hit.transform.GetComponent<Fire>() != null) return true;

            return false;
        }
        return false;
    }

    protected void LineCast (out RaycastHit2D hit, Vector2 start, Vector2 end)
    {
        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end);
        boxCollider.enabled = true;
    }

    protected void PlaySound(AudioClip audioClip)
    {
        if (audioClip == null) return;
        if (soundMade) return;

        audioSource.clip = audioClip;
        audioSource.Play();

        soundMade = true;
    }

    public override void DestroySelf()
    {
        PlaySound(destroySound);
        base.DestroySelf();
    }
}
