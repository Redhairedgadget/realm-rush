using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem projectileParticles;
    Transform target;
    [SerializeField] float range = 15f;

    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    // TODO: this shouldn't be fired on Update, but only when enemy goes out of range or dies
    void FindClosestTarget() {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach(Enemy enemy in enemies) {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (targetDistance < maxDistance) {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }
        target = closestTarget;
    }

    void AimWeapon() {
        float targetDistance = Vector3.Distance(transform.position, target.position);
        weapon.LookAt(target);

        if (targetDistance < range) {
            Attack(true);
        } else {
            Attack(false);
        }
    }

    void Attack(bool isActive) {
        projectileParticles.enableEmission = isActive;
    }
}
