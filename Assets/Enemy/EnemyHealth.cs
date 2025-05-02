using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health = 5;
    [Tooltip("Adds amount to maxHitPoints when enemy dies.")]
    [SerializeField] int difficultyRamp = 1;
    int currentHealth;
    Enemy enemy;

    void Start() {
        enemy = GetComponent<Enemy>();
    }

    void OnEnable() {
        currentHealth = health;
    }

    void OnParticleCollision(GameObject other)
    {
        currentHealth--;

        Debug.Log("Tower got hit");

        if (currentHealth <= 0) {
            KillEnemy();
            health += difficultyRamp;
            enemy.RewardGold();
        }
    }

    void KillEnemy() {
        gameObject.SetActive(false);
    }
}
