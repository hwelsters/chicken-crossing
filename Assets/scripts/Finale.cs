using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finale : TurnBased
{
    public void Update()
    {
        GameObject[] allBlocks = GameObject.FindGameObjectsWithTag("Blocks");
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (allBlocks.Length == 0 && player == null) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
