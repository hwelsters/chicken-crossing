using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MovingObject
{
    [Header("Player")]

    [SerializeField] protected bool isActive = true;

    [SerializeField] private GameObject dustObject;
    
    private bool playersTurn = true;

    //Bonk
    protected override void Start()
    {
        base.Start();
        Reset();
    }

    protected void Update()
    {
        if (!playersTurn || !isActive) return;

        int horizontal = (int) Input.GetAxisRaw("Horizontal");
        int vertical = (int) Input.GetAxisRaw("Vertical");
        
        if (horizontal != 0) 
        {
            vertical = 0;
            transform.localScale = new Vector2 (-horizontal, transform.localScale.y);
        }

        if (horizontal != 0 || vertical != 0)
        {
            if(base.AttemptMove(horizontal, vertical)) 
            {
                //RENAME!!!!!!
                GameObject instantiatedObject = Instantiate (dustObject, transform.position, Quaternion.identity);
                Destroy(instantiatedObject, 0.3f);
            }

            //DELETE
            playersTurn = false;
            Invoke("Reset", 0.150f);
        }
    }

    protected override bool Push(RaycastHit2D hit, int xDir, int yDir)
    {
        if (hit.transform != null && (hit.transform.GetComponent<Chick>() != null || hit.transform.GetComponent<Key>() != null)) return true;
        return base.Push(hit, xDir, yDir);
    }

    //DELETE
    public override void TurnUpdate()
    {
        base.TurnUpdate();
        
        GameObject[] other = GameObject.FindGameObjectsWithTag("Other");
        foreach(GameObject g in other)
        {
            g.GetComponent<TurnBased>().TurnUpdate();
        }

        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Blocks");
        foreach(GameObject g in blocks)
        {
            g.GetComponent<TurnBased>().TurnUpdate();
        }
    }

    //DELETE THIS!!!
    void Reset()
    {
        HoneyBlock.allHoneyBlocks.Clear();
        MovingObject.soundMade = false;
        TurnUpdate();
        playersTurn = true;
    }

    public override void DestroySelf()
    {
        GameOver();
        base.DestroySelf();
    }
    
    //CODE REVIEW
    protected void SwitchPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in players)
        {
            Player playerScript = player.GetComponent<Player>();
            playerScript.isActive = true;
        } 
        this.isActive = true;
    }

    public void GameOver()
    {

    }
}