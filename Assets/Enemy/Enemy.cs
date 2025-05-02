using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField][Range(0f, 1000)]  int goldReward = 25;
    [SerializeField][Range(-1000, 0f)] int goldPenalty = -25;


    Bank bank;

    void Start()
    {
        bank = FindAnyObjectByType<Bank>();
    }

    public void RewardGold() {
        IssueBalanceChange(goldReward);
    }

    public void StealGold() {
        IssueBalanceChange(goldPenalty);
    }

    void IssueBalanceChange(int change) {
        if(bank == null) { return; }
        bank.ChangeBalance(change);
    }
}
