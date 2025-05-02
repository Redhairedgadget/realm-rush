using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool isPlaceable = true;
    public bool IsPlaceable => isPlaceable;
    [SerializeField] Tower TowerPrefab;

    void OnMouseDown()
    {
        if (isPlaceable) {
            bool isPlaced = TowerPrefab.CreateTower(transform.position);
            if (isPlaced) isPlaceable = false;
        }
    }
}
