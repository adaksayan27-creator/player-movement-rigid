using System;
using System.Collections;
using UnityEngine;

public class health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] public float startinghealth;
    public float currenthealth { get; private set; }

    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iframeDuration;
    [SerializeField] private float numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Death Sound")]
    [SerializeField]private AudioClip Deathsound;
    [SerializeField]private AudioClip hurtsound;

    private void Awake()
    {
        currenthealth = startinghealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        SoundManager.instance.PlaySound(hurtsound);
    }

    public void takedamage(float damage)
    {
        if (dead) return;

        currenthealth = Mathf.Clamp(currenthealth - damage, 0, startinghealth);

        if (currenthealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");

                if (GetComponent<jumping>() != null)
                    GetComponent<jumping>().enabled = false;

                if (GetComponentInParent<EnemyPatrol>() != null)
                    GetComponentInParent<EnemyPatrol>().enabled = false;

                if (GetComponent<MeeleEnemy>() != null)
                    GetComponent<MeeleEnemy>().enabled = false;

                dead = true;
                SoundManager.instance.PlaySound(Deathsound);
            }
        }
    }

    public void AddHealth(float amount)
    {
        currenthealth = Mathf.Clamp(currenthealth + amount, 0, startinghealth);
    }

    public bool getdead()
    {
        return dead;
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iframeDuration / (numberOfFlashes * 2));

            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iframeDuration / (numberOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(10, 11, false);
    }
    private void diactivate()
    {
        gameObject.SetActive(false);
    }
}