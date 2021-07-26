using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    public GameObject UpWall;
    public GameObject BottomWall;
    public GameObject RightWall;
    public GameObject LeftWall;

    public bool isVisited = false;
}
