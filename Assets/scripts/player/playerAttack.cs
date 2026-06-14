using UnityEngine;

public class playerAttack : MonoBehaviour
{
    [SerializeField] private float attackcooldown;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField]private AudioClip fireballsound;
    private Animator anim;
    private jumping jump;

    private float cooldowntimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        jump = GetComponent<jumping>();
    }

    private void Update()
    {
        cooldowntimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
            Attack();
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballsound);
        anim.SetTrigger("attack");

        cooldowntimer = 0;

        fireballs[findFireball()].transform.position = firepoint.position;

        fireballs[findFireball()]
            .GetComponent<projectile>()
            .SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int findFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }

        return 0;
    }
}