using UnityEngine;

public class MeeleEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackcooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;

    private float cooldowntimer = Mathf.Infinity;
    [Header("Fireball Sound")]
    [SerializeField]private AudioClip attacksound;

    private Animator anim;
    private health playerHealth;
    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        cooldowntimer += Time.deltaTime;

        if (playerinSight())
        {
            if (playerHealth != null && playerHealth.getdead())
                return;

            if (cooldowntimer >= attackcooldown)
            {
                cooldowntimer = 0;
                anim.SetTrigger("MeeleAttack");
                SoundManager.instance.PlaySound(attacksound);
            }
        }

        if (enemyPatrol != null)
            enemyPatrol.enabled = !playerinSight();
    }

    private bool playerinSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0,
            Vector2.left,
            0,
            playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<health>();

        return hit.collider != null;
    }

    private void DamagePlayer()
    {
        if (playerHealth != null && !playerHealth.getdead())
            playerHealth.takedamage(damage);
    }

    private void OnDrawGizmos()
    {
        if (boxCollider == null) return;

        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(
            boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}   