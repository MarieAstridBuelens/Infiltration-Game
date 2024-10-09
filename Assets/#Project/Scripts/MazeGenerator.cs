using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    //tableau d'entiers pour ne pas faire que vides ou murs
    //aussi portes, murs fancy etc.
    private int[,] cells;
    [SerializeField] private int size_x = 10;
    [SerializeField] private int size_y = 8;

    //Vector2Int : coordonnées sont des nombres entiers
    private List<Vector2Int> walls = new ();//=new List<Vector2Int>();

    //se passe avant Start. Awake : initialisations internes au script
    // ><Start : chercher un composant sur un autre objet après son initialisation
    void Awake(){
        //1. Start with a grid full of walls
        cells = new int[size_x, size_y];
        for(int i = 0; i < size_x; i++){
            for(int j = 0; j < size_y; j++){
                cells[i, j] = 0;
            }
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    int NumberOfVisitedNeighbours(Vector2Int pos){
        int count = 0;

        if (pos.x < size_x - 1 && cells[pos.x + 1, pos.y] > 0) count++;
        if (pos.x > 0 && cells[pos.x - 1, pos.y] > 0) count++;

        if (pos.y < size_y - 1 && cells[pos.x, pos.y + 1] > 0) count++;
        if (pos.y > 0 && cells[pos.x, pos.y - 1] > 0) count++;

        return count; //compte de nombres cellules alentours qui ne sont plus un mur
    }

    void AddNeighbouringWallsToList(Vector2Int pos){
        if (pos.x < size_x - 1 && cells[pos.x + 1, pos.y] <= 0){
            Vector2Int newWall = new Vector2Int(pos.x + 1, pos.y);
            if (!walls.Contains(newWall)){
                walls.Add(newWall);
            }
            
        }
        if (pos.x > 0 && cells[pos.x - 1, pos.y] <= 0){
            Vector2Int newWall = new Vector2Int(pos.x - 1, pos.y);
            if (!walls.Contains(newWall)){
                walls.Add(newWall);
            }
        }

        if (pos.y < size_y - 1 && cells[pos.x, pos.y + 1] <= 0){
            Vector2Int newWall = new Vector2Int(pos.x, pos.y + 1);
            if (!walls.Contains(newWall)){
                walls.Add(newWall);
            }
        }
        if (pos.y > 0 && cells[pos.x, pos.y - 1] <= 0){
            Vector2Int newWall = new Vector2Int(pos.x, pos.y - 1);
            if (!walls.Contains(newWall)){
                walls.Add(newWall);
            }
        }
    }

    void GenerateMaze(){
        //2a. Pick a cell, mark it as part of the maze
        cells[0, 0] = 1;

        //2b. Add the walls of the cell to the wall list
        //dans les vecteurs, ce sont nombres, donc entre parenthèses, pas crochets !
        walls.Add(new Vector2Int(1, 0));
        walls.Add(new Vector2Int(0, 1));

        //3. While there are walls in the list:
        while (walls.Count > 0){
            //3.1a. Pick a random wall from the list
            int wall_index = Random.Range(0, walls.Count);
            Vector2Int wall = walls[wall_index];

            //3.1b. If only one of the cells that the wall divides is visited, then:
            if(NumberOfVisitedNeighbours(wall) == 1){
                //3.1.1. Make the wall a passage and mark the unvisited cell as part of the maze
                // rappel : wall est un Vector2, prendre .x et .y pour en refaire un index
                cells[wall.x, wall.y] = 1;

                //3.1.2. Add the neighboring walls of the cell to the wall list
                AddNeighbouringWallsToList(wall);
            }
        }
    }
}
