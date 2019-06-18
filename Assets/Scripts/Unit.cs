﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string name = "Inmate";
    public int tileX;
    public int tileY;
    public int directionX;
    public int directionY;
    public int moveRate=5;
    public TileMap map;

    public List<Node> currentPath = null;

    void OnMouseUp()
    {
        Debug.Log("Clicked on "+name);
        map.setSelectedUnit(transform.gameObject);
    }

    void Update()
    {
        if (currentPath !=null)
        {
            int moveCounter = moveRate;
            int currNode = 0;
            while(currNode < currentPath.Count-1)
            {
                Vector3 start = map.TileCoordToWorldCoord(currentPath[currNode].x, currentPath[currNode].y);
                Vector3 end = map.TileCoordToWorldCoord(currentPath[currNode + 1].x, currentPath[currNode + 1].y);
                Debug.DrawLine(start, end);
                currNode = currNode + 1;
            }
        MoveNextTile();
        }
    }

    public void setPath(List<Node> newPath) 
    {
        currentPath = newPath;
    }

    public void MoveNextTile()
    {
        //If there is no path to follow, return.
        if (currentPath == null)
            return;
        //If the unit is close enough to its next destination
        if (Vector3.Distance(transform.position, map.TileCoordToWorldCoord(currentPath[0].x, currentPath[0].y)) < 0.1f) {
            //If the unit has hit a node but has reached the end of its path
            if (currentPath.Count == 1)
            {
                transform.position = map.TileCoordToWorldCoord(currentPath[0].x, currentPath[0].y);
                tileX = currentPath[0].x;
                tileY = currentPath[0].y;
                currentPath = null;
                directionX = 0;
                directionY = 0;
            }
            //If the unit has hit a node but has more nodes to visit
            else
            {
                tileX = currentPath[0].x;
                tileY = currentPath[0].y;
                currentPath.RemoveAt(0);
                int oldDirectionX = directionX;
                int oldDirectionY = directionY;
                directionX = currentPath[0].x - tileX;
                directionY = currentPath[0].y - tileY;
                if (oldDirectionX != directionX || oldDirectionY != directionY) {
                    setRotation();
                }
            }
        }
        //Move as long as there are nodes in the path
        if (currentPath!=null) {
            transform.position = incrementPosition(transform.position);
        }
    }

    public Vector3 incrementPosition(Vector3 position)
    {
        return map.TileCoordToWorldCoord(position.x + directionX*2.5f*Time.deltaTime, position.y + directionY *2.5f* Time.deltaTime);
    }

    public void setRotation()
    {
        if (directionX == 1 && directionY == -1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 45);
        }
        else if (directionX == 1 && directionY == 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (directionX == 1 && directionY == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 135);
        }
        else if (directionX == 0 && directionY == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (directionX == -1 && directionY == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, -135);
        }
        else if (directionX == -1 && directionY == 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (directionX == -1 && directionY == -1)
        {
            transform.rotation = Quaternion.Euler(0, 0, -45);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
