using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;
    [SerializeField] int currentBalance;
    public int CurrentBalance => currentBalance;

    [SerializeField] TextMeshProUGUI displayBalance;

    void UpdateDisplay() {
        string messageStart = "Gold: ";
        displayBalance.text =  messageStart + currentBalance;
    }

    void Awake() {
        currentBalance = startingBalance;
        UpdateDisplay();
    }

    public void ChangeBalance(int amount) {
        currentBalance += amount;
        UpdateDisplay();

        if (currentBalance < 0) {
            // Lose the game
            ReloadScene();
        }
    }

    void ReloadScene() {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    // public void Deposit(int amount) {
    //     currentBalance += Mathf.Abs(amount);
    // }

    // public void Withdraw(int amount) {
    //     currentBalance -= Mathf.Abs(amount);
    // }
}
