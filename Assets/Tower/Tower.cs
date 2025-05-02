using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;
    public bool CreateTower(Vector3 position) {
        Bank bank = FindFirstObjectByType<Bank>();

        if (bank == null) {
            return false;
        }

        if(bank.CurrentBalance >= cost) {
            Instantiate(gameObject, position, Quaternion.identity);
            bank.ChangeBalance(-cost);
            return true;
        } 

        return false;
    }
}
