using UnityEngine;
public class jumping : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool jump;
//reference for rigidbody and animation
    private void Awake(){
        body= GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
    }
    private void Update(){
        float horizontalInput=Input.GetAxis("Horizontal");
        body.linearVelocity=new Vector2(horizontalInput*speed,body.linearVelocity.y);
    if(horizontalInput > 0.01f)
        transform.localScale = new Vector3(1,1,1);
    else if(horizontalInput < -0.01f)
        transform.localScale = new Vector3(-1,1,1);

        if(Input.GetKey(KeyCode.Space)&&jump)
         Jump();

         //setting animation parameter-
         anim.SetBool("run",horizontalInput!=0);
         anim.SetBool("jump",jump);
 
    }
    private void Jump()
    {
        body.linearVelocity=new Vector2(body.linearVelocity.x,speed);
        anim.SetTrigger("jumping");
        jump=false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="jump")
        jump =true;
    }
}