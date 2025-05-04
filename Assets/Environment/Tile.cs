using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] bool isPlaceable = true;
    public bool IsPlaceable => isPlaceable;
    [SerializeField] Tower TowerPrefab;

    GridManager gridManager;
    Pathfinder pathfinder;
    Vector2Int coordinates = new Vector2Int();

    void Awake() {
        gridManager = FindFirstObjectByType<GridManager>();
        pathfinder = FindFirstObjectByType<Pathfinder>();
    }

    void Start() {
        if(gridManager != null) {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable) {
                gridManager.BlockNode(coordinates);
            }
        }
    }


    // TODO: there is a bug where the node gets blocked without tower being created if bank is less than cost
    void OnMouseDown()
    {
        if (gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates)) {
            bool isSuccessful = TowerPrefab.CreateTower(transform.position);
            if (isSuccessful) {
                // isPlaceable = false;
                gridManager.BlockNode(coordinates);
                pathfinder.NotifyReceivers();
            }
        }
    }
}
