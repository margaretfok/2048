using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputDirection
{
    Left, Right, Up, Down
}
public class InputManager : MonoBehaviour
{
    public GameManager gameManager;
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    
    private void Update()
    {
        if (gameManager.isInputAllowed)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                gameManager.Move(InputDirection.Left);
            else if (Input.GetKeyDown(KeyCode.UpArrow))
                gameManager.Move(InputDirection.Up);
            else if (Input.GetKeyDown(KeyCode.DownArrow))
                gameManager.Move(InputDirection.Down);
            else if (Input.GetKeyDown(KeyCode.RightArrow))
                gameManager.Move(InputDirection.Right);
            else if (Input.GetKeyDown(KeyCode.A))
                gameManager.SpawnTwo1024Tiles();
            else if (Input.GetKeyDown(KeyCode.D))
                gameManager.SpawnRandomTiles();
            /*else if(Input.touches.Length > 0)
            {
                Touch t = Input.GetTouch(0);
                if(t.phase == TouchPhase.Began)
                {
                    //save began touch 2d point
                    firstPressPos = new Vector2(t.position.x,t.position.y);
                }
                if(t.phase == TouchPhase.Ended)
                {
                    //save ended touch 2d point
                    secondPressPos = new Vector2(t.position.x,t.position.y);
                           
                    //create vector from the two points
                    currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
               
                    //normalize the 2d vector
                    currentSwipe.Normalize();
 
                    //swipe upwards
                    if(currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                    {
                        Debug.Log("up swipe");
                    }
                    //swipe down
                    if(currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                    {
                        Debug.Log("down swipe");
                    }
                    //swipe left
                    if(currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                    {
                        Debug.Log("left swipe");
                    }
                    //swipe right
                    if(currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                    {
                        Debug.Log("right swipe");
                    }
                }
            }*/
        }
    }
}
