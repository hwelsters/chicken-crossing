using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBased : MonoBehaviour
{
    [Header("Turn Based")]
    [SerializeField] 
    protected GameObject destroyAnimation;

    //QUESTION MARK????
    protected virtual void Start()
    {
        RandomiseOffset();
    }

    public virtual void TurnUpdate() {}

    //QUESTIONABLE CODE, TO REVISE
    public virtual void DestroySelf() 
    {
        //SUS CODE! BEEP BEEP!
        if (destroyAnimation != null)
        {
            GameObject toDestroy = Instantiate(destroyAnimation, transform.position, Quaternion.identity);
            Destroy(toDestroy, 0.5f);
        }
        Destroy(gameObject);
    }

    //MIGHT NOT BE BEST PLACE TO PUT THIS FUNCTION
    protected virtual Transform CheckOverlap<T>(Vector2 position)
    {
        Collider2D[] overlappingObject = Physics2D.OverlapCircleAll(position, 0.1f);
        
        foreach(Collider2D collider2D in overlappingObject)
        {
            T objectScript = collider2D.GetComponent<T>();
            if (objectScript != null)
            {
                return collider2D.transform;
            }
        }
        return null;
    } 

    //QUESTION MARK????
    protected void RandomiseOffset()
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null) animator.SetFloat("Offset", Random.Range(0.0f, 1.0f));
    }
}
