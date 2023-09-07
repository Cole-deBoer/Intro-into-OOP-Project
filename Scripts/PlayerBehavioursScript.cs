using UnityEngine;


public class PlayerBehavioursScript : MonoBehaviour, IActions
{
    //Player Movement
    public Rigidbody playerRb;
    
    //Player Rotation
    public float mouseSensitivity;
    public float xRotation;

    private void Update()
    {
        RotatePlayerWithMouse();
        Jump(); 
        
        if (Input.GetMouseButtonDown(0))
        {
            Attack(PlayerAttributes.Strength,PlayerAttributes.AtkSpeed,PlayerAttributes.Reach);
        }

        PlayerAttributes.PlayerPos = gameObject.transform;
    }

    private void FixedUpdate()
    {
        Move(PlayerAttributes.Speed, transform.forward);
    }

    private void Jump()
    {
        if (GroundCheck() && Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(0, PlayerAttributes.JumpForce, 0, ForceMode.Impulse);
        }
    }

    private bool GroundCheck()
    {
        var transform1 = transform;
        var grounded = Physics.Raycast(transform1.position, -transform1.up, 1.1f);
        return grounded;
    }

    private void RotatePlayerWithMouse()
    {
        var mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.fixedDeltaTime;

        xRotation -= mouseX;

        transform.localRotation = Quaternion.Euler(0f, -xRotation, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }

    public void Move(float speed, Vector3 destination)
    {
        if (Input.GetKey(KeyCode.W))
        {
            playerRb.AddForce(destination * speed, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerRb.AddForce(transform.right * -speed, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.S))
        {
            playerRb.AddForce(destination * -speed, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerRb.AddForce(transform.right * speed, ForceMode.Acceleration);
        }
    }

    public void Attack(float strength, float atkSpeed, float atkDistance)
    {

        if (!Input.GetMouseButtonDown(0)) return;
        var ray = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, atkDistance);

        if (!ray || !hit.collider.CompareTag("Monster")) return;
        print("YOU HIT AN ENEMY, ENEMY HIT: " + hit.collider.gameObject.name);
        hit.collider.gameObject.GetComponent<Monster>().Die(strength);
    }

    public void Heal(float healAmount)
    {
        
    }

    public void Die(float atkDmg)
    {
        PlayerAttributes.Health -= atkDmg;
        print(PlayerAttributes.Health);
        
        if (PlayerAttributes.Health <= 0f)
        {
            //things that happen when you die
            Destroy(gameObject);
        }
    }
}


