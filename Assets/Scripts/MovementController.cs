using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovementController : MonoBehaviour
{

    [SerializeField]
    public Tilemap groundMap;
    public Tilemap collisionMap;
    private Vector3 movePoint;
    public float moveSpeed = 5.0f;
    private enum Direction
    {
        left,
        right,
        up,
        down
    }
    // Start is called before the first frame update
    void Start()
    {
        //Move player to center 
        Vector3Int positionInGrid = groundMap.WorldToCell(transform.position);
        transform.position = groundMap.CellToWorld(positionInGrid) + groundMap.cellSize / 2;

        //set MoveTo to zero
        movePoint = transform.position;


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W))
            Move(Direction.up);
        if (Input.GetKey(KeyCode.S))
            Move(Direction.down);
        if (Input.GetKey(KeyCode.A))
            Move(Direction.left);
        if (Input.GetKey(KeyCode.D))
            Move(Direction.right);

        transform.position = Vector3.MoveTowards(transform.position, movePoint, Time.deltaTime * moveSpeed);



    }
    void Move(Direction direction)
    {
        if (Vector3.Distance(transform.position, movePoint) < 0.05f)
        {
            Vector3Int positionInGrid = groundMap.WorldToCell(transform.position);
            if (direction == Direction.right)
                positionInGrid += new Vector3Int(1, 0, 0);

            if (direction == Direction.left)
                positionInGrid += new Vector3Int(-1, 0, 0);

            if (direction == Direction.up)
                positionInGrid += new Vector3Int(0, 1, 0);

            if (direction == Direction.down)
                positionInGrid += new Vector3Int(0, -1, 0);
            if (groundMap.HasTile(positionInGrid) && !collisionMap.HasTile(positionInGrid))
            {
                movePoint = groundMap.CellToWorld(positionInGrid) + groundMap.cellSize / 2;
            }

        }
    }
}
