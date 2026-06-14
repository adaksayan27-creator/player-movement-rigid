using UnityEngine;
using System.Collections;

public class firetraps : MonoBehaviour
{
    [SerializeField] private float damage;
    [Header("Firetrap Timer")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeDuration;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered;
    private bool active;

    [Header("SFX")]
    [SerializeField]private AudioClip Firetrapsound;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !triggered)
        {
            StartCoroutine(ActivateFiretrap());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (active && collision.CompareTag("Player"))
        {
            collision.GetComponent<health>().takedamage(damage);
        }
    }

    private IEnumerator ActivateFiretrap()
    {
        //turn the sprite red to indicate activation
        spriteRend.color = Color.red;
        triggered = true;

        //wait for delay,activate the trap,wait for active duration,then reset
        yield return new WaitForSeconds(activationDelay);
        SoundManager.instance.PlaySound(Firetrapsound);

        spriteRend.color = Color.white;
        active = true;
        anim.SetBool("activated", true);

        //wait until x seconds have passed,then reset the trap
        yield return new WaitForSeconds(activeDuration);

        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}