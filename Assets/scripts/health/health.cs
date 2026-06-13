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

    private void Awake()
    {
        currenthealth = startinghealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void takedamage(float damage)
    {
        if (dead) return;

        currenthealth = Mathf.Clamp(currenthealth - damage, 0, startinghealth);

        Debug.Log(currenthealth);

        if (currenthealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
        }
        else
        {
            dead = true;

            anim.SetTrigger("die");

            GetComponent<jumping>().enabled = false;

            GetComponent<playerAttack>().enabled = false;
        }
    }

    public void AddHealth(float amount)
    {
        currenthealth = Mathf.Clamp(currenthealth + amount, 0, startinghealth);
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            takedamage(1);
        }
    }
}