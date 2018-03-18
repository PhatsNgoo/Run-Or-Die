using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Controller2D))]
[RequireComponent(typeof(InputController))]
[RequireComponent(typeof(Animator))]

public class PlayerController : MonoBehaviour {
	public float movementSpeed;
	public float jumpSpeed;
    public int playerTag;
    public float distanceToDieX ;
    public float distanceToDieY ;
    public Text playerName;
    

	[Space]
	public float dashMultiplier;
	public float dashTime;
    public CharacterSkill characterSkill;
    
	public bool isDashing { get; private set; }
    public bool canDashing;
    public bool canDoubleJump;
    public bool doubleJumpSkill;

    public GameObject player;
    private Animator anim;

    private bool isSlowing;
    private float slowTime;
    private HealthScript healthScript;
	private InputController inputController;
	private Controller2D controller2D;
	private Vector2 velocity;
	private int faceDirection;

	public PlayerStatus playerStatus { get; private set; }

	private void Awake()
	{
        if (playerTag == 2)
        {
            //playerName.text = PlayerPrefs.GetString("Player2Name");
        }
        canDashing = false;
        canDoubleJump = false;
        inputController = GetComponent<InputController>();
		controller2D = GetComponent<Controller2D>();
        anim = GetComponent<Animator>();
        characterSkill = GetComponent<CharacterSkill>();
        healthScript = (HealthScript)FindObjectOfType(typeof(HealthScript));
        isSlowing = false;
		inputController.OnMovePressed += Move;
		inputController.OnJumpPressed += JumpIfPossible;
		inputController.OnDashPressed += DashIfPossible;
        anim.SetBool("Jump", false);
        anim.SetBool("Idle", false);
        characterSkill.Skill1();
        characterSkill.Skill2();
    }
    private void Start()
    {
        distanceToDieX = 200;
        distanceToDieY = 120;
        if (playerTag == 1 && PlayerPrefs.GetInt("Player1Character") == 1)
        {
            canDashing = true;
        }
        if (playerTag == 1 && PlayerPrefs.GetInt("Player1Character") == 2)
        {
            canDoubleJump = true;
        }
        if (playerTag == 1 && PlayerPrefs.GetInt("Player1Character") == 3)
        {
            healthScript.player1HealthScale = 0.00085f;
        }
        if (playerTag == 2 && PlayerPrefs.GetInt("Player2Character") == 1)
        {
            canDashing = true;
        }
        if (playerTag == 2 && PlayerPrefs.GetInt("Player2Character") == 2)
        {
            canDoubleJump = true;
        }
        if (playerTag == 2 && PlayerPrefs.GetInt("Player2Character") == 3)
        {
            healthScript.player2HealthScale = 0.00085f;
        }
        if (playerTag == 1)
        {
            //if (PlayerPrefs.GetString("Player1Name") != null)
            //{ playerName.text += PlayerPrefs.GetString("Player1Name"); }
            //else 
            playerName.text = "Player1";
        }
    }
    private void OnDestroy()
	{
		inputController.OnMovePressed -= Move;
		inputController.OnJumpPressed -= JumpIfPossible;
		inputController.OnDashPressed -= DashIfPossible;
	}

	public void FixedUpdate()
	{
        if (!healthScript.isPausing)
        {
            if (slowTime < 3 && isSlowing)
            {
                slowTime += Time.fixedDeltaTime;
            }
            Debug.Log(slowTime);
            if (isSlowing && slowTime>=3)
            {
                movementSpeed += movementSpeed;
                isSlowing = false;
            }
            anim.SetBool("Jump", false);
            anim.SetBool("Idle", false);

            velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

            playerStatus = controller2D.Move(velocity * Time.fixedDeltaTime);

            transform.position += (Vector3)playerStatus.velocity;

            if (playerStatus.isCollidingBottom || playerStatus.isCollidingTop)
            {
                velocity.y = 0;
            }
            if (Input.GetAxisRaw(inputController.nameHorizontal) == 0 || !playerStatus.isCollidingBottom)
            {
                anim.SetFloat("MoveRight", 0);
                anim.SetFloat("MoveLeft", 0);

            }
            //anim.SetFloat("MoveHorizontal", Input.GetAxisRaw(nameHorizontal));
            if (playerStatus.isCollidingBottom && Input.GetAxisRaw(inputController.nameHorizontal) != 0)
            {
                if (Input.GetAxisRaw(inputController.nameHorizontal) > 0)
                {
                    anim.SetFloat("MoveRight", Input.GetAxisRaw(inputController.nameHorizontal));
                    anim.SetFloat("MoveLeft", 0);
                    

                }
                if (Input.GetAxisRaw(inputController.nameHorizontal) < 0)
                {
                    anim.SetFloat("MoveRight", 0);
                    anim.SetFloat("MoveLeft", Input.GetAxisRaw(inputController.nameHorizontal));

                }
            }
            else if (anim.GetFloat("MoveRight") == 0 && anim.GetFloat("MoveLeft") == 0 && playerStatus.isCollidingBottom)
            {
                anim.SetBool("Idle", playerStatus.isCollidingBottom);
            }
            else if (!playerStatus.isCollidingBottom)
            {
                anim.SetBool("Jump", !playerStatus.isCollidingBottom);
            }

            if (canDoubleJump && playerStatus.isCollidingBottom)
            { doubleJumpSkill = true; }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Poison")
        {
            if (playerTag == 1) { healthScript.healthBarPlayer1.transform.localScale -= new Vector3(0.05f, 0, 0); }
            else { healthScript.healthBarPlayer2.transform.localScale -= new Vector3(0.05f, 0, 0); }
            healthScript.trapSound.Play();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Trap")
        {
            if (playerTag == 1) { healthScript.healthBarPlayer1.transform.localScale -= new Vector3(0.05f, 0, 0); }
            else { healthScript.healthBarPlayer2.transform.localScale -= new Vector3(0.05f, 0, 0); }
            healthScript.trapSound.Play();
        }
        if (collision.gameObject.tag == "SlowTrap")
        {
            isSlowing = true;
            slowTime = 0;
            movementSpeed /= 2;
            healthScript.trapSound.Play();
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Heal")
        {
            if (playerTag == 1) { healthScript.healthBarPlayer1.transform.localScale += new Vector3(0.03f, 0, 0); }
            else { healthScript.healthBarPlayer2.transform.localScale += new Vector3(0.03f, 0, 0); }
            healthScript.collideSound.Play();
            Destroy(collision.gameObject);
        }
    }

    private void LateUpdate()
    {
        //Debug.Log(player.transform.position.x);
        //Debug.Log(player.transform.position.y);
        if (player.transform.position.x < Camera.main.transform.position.x - distanceToDieX|| player.transform.position.y < Camera.main.transform.position.y - distanceToDieY)
        {
            PlayerPrefs.SetInt("PlayerDie", playerTag);
            if (playerTag == 1) { PlayerPrefs.SetInt("PlayerWin", 2); }
            else { PlayerPrefs.SetInt("PlayerWin", 1); }
            SceneManager.LoadScene("EndScene");
        }
    }
    public void activeDoubleJump()
    {
        doubleJumpSkill = false;
    }
    public void JumpIfPossible()
	{
		if (playerStatus.isCollidingBottom && !healthScript.isPausing)
		{
			Jump();
		}
	}
	public void Jump()
	{
		velocity.y = jumpSpeed;
	}


	private void DashIfPossible()
	{
		if (!isDashing && playerStatus.isCollidingBottom && canDashing)
		{
			Dash();
		}
	}
	public void Dash()
	{
		velocity.x = faceDirection * movementSpeed * dashMultiplier;
		isDashing = true;

		StartCoroutine(DashCoroutine());
	}

	private IEnumerator DashCoroutine()
	{
		yield return new WaitForSeconds(dashTime);
		isDashing = false;
	}

	private void Move(float direction)
	{
		if (!isDashing)
		{
			if (direction != 0)
			{
				faceDirection = (int)Mathf.Sign(direction);
			}

			velocity.x = direction * movementSpeed;
        }
	}
}
