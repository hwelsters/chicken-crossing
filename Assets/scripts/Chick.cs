using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chick : TurnBased
{
    //SLEEP DEPRIVED NOEL HERE
    //HELP
    private void Update()
    {
        bool restartPressed = Input.GetKeyDown(KeyCode.R);

        if (restartPressed) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public override void TurnUpdate()
    {
        Transform playerTransform = base.CheckOverlap<Player>(transform.position);
        if (playerTransform != null) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
