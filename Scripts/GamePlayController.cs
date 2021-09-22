using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayController : MonoBehaviour
{
    private int moveCount;

    public static GamePlayController instanse;

    public BoxSpawner boxSpawner;
    public CameraFollow cameraScript;

    [HideInInspector]
    public BoxScript currentBox;//захуярил знач



    private void Awake()
    {
        if (instanse == null)
        {
            instanse = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        boxSpawner.SpawnBox();
    }

    // Update is called once per frame
    void Update()
    {
        DetectInput();
    }
    void DetectInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentBox.DropBox();
        }
    }

    void NexBox()
    {
        boxSpawner.SpawnBox();
    }
    public void SpawnNewBox()
    {
        Invoke(nameof(NexBox), 2f);
    }
  

    public void MoveCamera()
    {
        moveCount++;
        if (moveCount == 3)
        {
            moveCount = 0;
          //cameraScript.targetPos.y+=2f;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
