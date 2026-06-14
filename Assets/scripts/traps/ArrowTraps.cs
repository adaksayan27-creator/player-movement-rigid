using UnityEngine;

public class ArrowTraps : MonoBehaviour
{
    [SerializeField] private float attackcooldown;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] arrows;

    private float cooldowntimer;
    [Header("SFX")]
    [SerializeField]private AudioClip arrowSound;

   private void Attack()
{
    cooldowntimer = 0;
    SoundManager.instance.PlaySound(arrowSound);

    int arrowIndex = FindArrow();

    if (arrowIndex != -1)
    {
        arrows[arrowIndex].transform.position = firepoint.position;

        arrows[arrowIndex]
            .GetComponent<EnemyProjectile>()
            .ActiveProjectile();
    }
}

    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }

        return -1;
    }

    private void Update()
    {
        cooldowntimer += Time.deltaTime;

        if (cooldowntimer >= attackcooldown)
            Attack();
    }
}