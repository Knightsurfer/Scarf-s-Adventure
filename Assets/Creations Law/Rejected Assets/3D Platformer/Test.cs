using UnityEngine;

public class Test : MonoBehaviour
{
    protected float moveSpeed = 10;
    protected float jumpForce = 10;
    public float gravityScale = 2f;
    protected CharacterController player;
    protected Animator anim;
    protected Vector3 moveDirection;

	void Start ()
    {
        player = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }







    void Update()
    {
        Animation();
        moveDirection = new Vector3(Input.GetAxis("LeftStickX")*moveSpeed, moveDirection.y, Input.GetAxis("LeftStickY")*moveSpeed);
        if (player.isGrounded)
        {
            moveDirection.y = 0;
            if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                moveDirection.y = jumpForce;
            }
        }

        player.Move(moveDirection * Time.deltaTime);
        moveDirection.y = moveDirection.y + Physics.gravity.y * gravityScale * Time.deltaTime;
        
    }


    protected void Animation()
    {
        if (Input.GetAxis("LeftStickX") != 0 || Input.GetAxis("LeftStickY") != 0)
        {
            anim.SetBool("Walking", true);

        }
        else
        {
            anim.SetBool("Walking", false);
        }


    }

}
