using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;
    [SerializeField] float buildDelay = 1f;
    
    void Start() {
        StartCoroutine(Build());
    }

    public bool CreateTower(Vector3 position) {
        Bank bank = FindFirstObjectByType<Bank>();

        if (bank == null) return false;

        if(bank.CurrentBalance >= cost) {
            Instantiate(gameObject, position, Quaternion.identity);
            bank.ChangeBalance(-cost);

            return true;
        } 

        return false;
    }

    IEnumerator Build() {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);

            foreach(Transform grandchild in child) {
                grandchild.gameObject.SetActive(false);
            }
        }
        

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);

            foreach(Transform grandchild in child) {
                grandchild.gameObject.SetActive(true);
            }
        }
    }
}
