using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] GameObject Floor;
    [SerializeField] GameObject Wall;
    [SerializeField] GameObject Character;
    [SerializeField] GameObject AStart;
    public int Rows=8;
    public int Columns=8;

    MazeCell[,] Grid;
    int currentRow=0;
    int currentColumn=0;


    void Start()
    {
        
        //Lets create Grids for maze
        CreateGrids();
        //lets use hunt and kill algorithm, I found it from here=>  http://weblog.jamisbuck.org/2011/1/24/maze-generation-hunt-and-kill-algorithm
        HuntAndKill();

    }
   
    void CreateGrids(){
        Grid = new MazeCell[Rows,Columns];

        AStart.SetActive(false);
        for(int i=0;i<Rows;i++){
            for(int j=0;j<Columns;j++){
                

                //Instantiate Floors
                GameObject currentFloor = Instantiate(Floor,new Vector2(j*Floor.transform.localScale.x,-i*Floor.transform.localScale.x),Quaternion.identity);
                currentFloor.transform.localScale=new Vector2(0.2f,0.2f);
                currentFloor.transform.parent=transform;
                /*START CREATING WALLS*/

                //Instantiate Up Walls
                GameObject currentUpWall = Instantiate(Wall,new Vector2(j*Wall.transform.localScale.x,-i*Wall.transform.localScale.x + Floor.transform.localScale.x/2),Quaternion.identity);
                currentUpWall.transform.localScale=new Vector2(0.2f,0.2f);
                currentUpWall.name="UpWall";
                currentUpWall.transform.parent=transform;
                
                //Instantiate Bottom Walls
                GameObject currentBottomWall = Instantiate(Wall,new Vector2(j*Wall.transform.localScale.x,-i*Wall.transform.localScale.x - Floor.transform.localScale.x/2),Quaternion.identity);
                currentBottomWall.transform.localScale=new Vector2(0.2f,0.2f);
                currentBottomWall.name="BottomWall";
                currentBottomWall.transform.parent=transform;

                //Instantiate Right Walls
                GameObject currentRightWall = Instantiate(Wall,new Vector2(j*Wall.transform.localScale.x+ Floor.transform.localScale.x/2,-i*Wall.transform.localScale.x ),Quaternion.Euler(0, 0, 90));
                currentRightWall.transform.localScale=new Vector2(0.2f,0.2f);
                currentRightWall.name="RightWall";
                currentRightWall.transform.parent=transform;

                //Instantiate Left Walls
                GameObject currentLeftWall = Instantiate(Wall,new Vector2(j*Wall.transform.localScale.x- Floor.transform.localScale.x/2,-i*Wall.transform.localScale.x ),Quaternion.Euler(0, 0, 90));
                currentLeftWall.transform.localScale=new Vector2(0.2f,0.2f);
                currentLeftWall.name="LeftWall";
                currentLeftWall.transform.parent=transform;

                Grid[i,j] = new MazeCell();
                Grid[i,j].UpWall= currentUpWall;
                Grid[i,j].BottomWall= currentBottomWall;
                Grid[i,j].RightWall= currentRightWall;
                Grid[i,j].LeftWall= currentLeftWall;

                int enterplace = Random.Range(0,10);
                
                if(i==0 && j==0 && enterplace%2==0){
                    Destroy(currentLeftWall);
                }else if(i==0 && j==0){
                    Destroy(currentUpWall);
                }

                if(i==Rows-1 && j==Columns-1 && enterplace%2==0){
                    Destroy(currentRightWall);
                }else if(i==Rows-1 && j==Columns-1){
                    Destroy(currentBottomWall);
                }
                
            

            }

        }
       
    }
    bool isThereUnvisitedNeighbors(){
        //Check up
        if(checkCellVisited(currentRow-1,currentColumn)){
            return true;
        }
        //Check down
        if(checkCellVisited(currentRow+1,currentColumn)){
            return true;
        }
        //Check left
        if(checkCellVisited(currentRow,currentColumn+1)){
            return true;
        }
        //Check right
        if(checkCellVisited(currentRow,currentColumn-1)){
            return true;
        }
        return false;
    }
    bool checkCellVisited(int row,int column){
        if(row>=0 && row <Rows && column>=0 && column<Columns && !Grid[row,column].isVisited){
            return true;
        }
        return false;
    }

    void HuntAndKill(){
        Grid[currentRow,currentColumn].isVisited=true;
        while(isThereUnvisitedNeighbors()){

            int myDirection = Random.Range(0,4);
            switch (myDirection)
            {
                //Up move
                case 0:
                    Debug.Log("Up move");
                    if(checkCellVisited(currentRow-1,currentColumn)){
                        if(Grid[currentRow,currentColumn].UpWall){
                            Destroy(Grid[currentRow,currentColumn].UpWall);
                        }
                        currentRow--;

                        Grid[currentRow,currentColumn].isVisited=true;

                        if(Grid[currentRow,currentColumn].BottomWall){
                            Destroy(Grid[currentRow,currentColumn].BottomWall);
                        }
                    }
                    break;
                //Down move
                case 1:
                    Debug.Log("Down move");
                    if(checkCellVisited(currentRow+1,currentColumn)){
                        if(Grid[currentRow,currentColumn].BottomWall){
                            Destroy(Grid[currentRow,currentColumn].BottomWall);
                        }

                        currentRow++;
                        Grid[currentRow,currentColumn].isVisited=true;

                        if(Grid[currentRow,currentColumn].UpWall){
                            Destroy(Grid[currentRow,currentColumn].UpWall);
                        }
                    }
                    break;
                //Left move
                case 2:
                    Debug.Log("Left move");
                    if(checkCellVisited(currentRow,currentColumn-1)){
                        if(Grid[currentRow,currentColumn].LeftWall){
                            Destroy(Grid[currentRow,currentColumn].LeftWall);
                        }

                        currentColumn--;
                        Grid[currentRow,currentColumn].isVisited=true;

                        if(Grid[currentRow,currentColumn].RightWall){
                            Destroy(Grid[currentRow,currentColumn].RightWall);
                        }
                    }
                    break;
                //Right move
                case 3:
                    Debug.Log("Right move");
                    if(checkCellVisited(currentRow,currentColumn+1)){
                        if(Grid[currentRow,currentColumn].RightWall){
                            Destroy(Grid[currentRow,currentColumn].RightWall);
                        }

                        currentColumn++;
                        Grid[currentRow,currentColumn].isVisited=true;

                        if(Grid[currentRow,currentColumn].LeftWall){
                            Destroy(Grid[currentRow,currentColumn].LeftWall);
                        }
                    }
                    break;

                default:
                    break;
            }
        }
        AStart.SetActive(true);
     
        
        
        
    }
    void Update()
    {
        

    }
}

