using UnityEngine;
using System.Collections.Generic;
public class Pathfinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    public Vector2Int StartCoordinates => startCoordinates;

    [SerializeField] Vector2Int destinationCoordinates;
    public Vector2Int DestinationCoordinates => destinationCoordinates;

    Node startNode;
    Node destinationNode;
    Node currentSearchNode;

    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

    Vector2Int[] directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};
    GridManager gridManager;

    void Awake() {
        gridManager = FindFirstObjectByType<GridManager>();
        if (gridManager != null) {
            startNode = gridManager.Grid[startCoordinates];
            destinationNode = gridManager.Grid[destinationCoordinates];
        }
    }

    void Start()
    {
        GetNewPath();
    }


    public List<Node> GetNewPath() {
        return GetNewPath(StartCoordinates);
    }

    public List<Node> GetNewPath(Vector2Int coordinates) {
        gridManager.ResetNodes();
        BFS(coordinates);
        return buildPath();
    }

    void ExploreNeighbors() {
        List<Node> neighbors = new List<Node>();

        //TODO: make this use one loop
        foreach (Vector2Int direction in directions) {
            Vector2Int neighborCoords = currentSearchNode.coordinates + direction;
            if (gridManager.Grid.ContainsKey(neighborCoords)) {
                neighbors.Add(gridManager.Grid[neighborCoords]);
            }
        }

        foreach (Node neighbor in neighbors) {
            if (!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable) {
                neighbor.prev = currentSearchNode;
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            };
        }
    }

    void BFS(Vector2Int coordinates) {
        startNode.isWalkable = true;
        destinationNode.isWalkable = true;

        // Resetting previous path, if any
        frontier.Clear();
        reached.Clear();

        bool isRunning = true;

        frontier.Enqueue(gridManager.Grid[coordinates]);
        reached.Add(coordinates, gridManager.Grid[coordinates]);

        while (isRunning && frontier.Count > 0) {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();
            if (currentSearchNode == destinationNode) {
                isRunning = false;
            }
        }
    }

    List<Node> buildPath () {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while(currentNode.prev != null) {

            currentNode = currentNode.prev;
            currentNode.isPath = true;
            path.Add(currentNode);
        }

        path.Reverse();
        return path;
    }

    public bool WillBlockPath(Vector2Int coordinates) {
        if(gridManager.Grid.ContainsKey(coordinates)) {
            bool previousState = gridManager.Grid[coordinates].isWalkable;

            gridManager.Grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            gridManager.Grid[coordinates].isWalkable = previousState;

            if(newPath.Count <= 1) {
                GetNewPath();
                return true;
            }

            return false;
        }

        return false;
    }
    
    public void NotifyReceivers() {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }
}
