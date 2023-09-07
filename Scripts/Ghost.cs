using UnityEngine;
using UnityEngine.AI;

public class Ghost : Monster
{
    protected override float Health { get; set; } = 3f;
    protected override float Speed { get; set; } = 1.2f;
    protected override float MagicPoints { get; set; } = 15f;
    protected override float AtkReach { get; set; } = 1f;
    protected override float HealingPower { get; set; }
    protected override float AtkSpeed { get; set; } = 1f;
    protected override float Strength { get; set; }

    private NavMeshAgent _ghostNavMesh;

    private void Awake()
    {
        _ghostNavMesh = GetComponent<NavMeshAgent>();

        print("MonsterType: " + name + " Hp: " + Health + " Mp: " + MagicPoints);

        Strength = Random.Range(0.6f, 2.2f); 
        HealingPower = Health / 4 * Random.Range(0.125f, 0.75f);
    }

    private void FixedUpdate()
    {
        //Move(Speed);
        //Attack(Strength, AtkSpeed, AtkReach);
        //Heal(HealingPower);
    }

    public override void Move(float speed, Vector3 destination)
    {
        //print(name + " speed: " + speed);
        
        _ghostNavMesh.speed = speed;
        
        _ghostNavMesh.destination = destination;
    }

    public override void Attack(float strength, float atkSpeed, float atkDistance)
    {
        //print(name + " strength: " + strength + " " + name + " attack speed: " + atkSpeed + " attack reach: " + atkDistance);

        var origin = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        
        var attackReach = Physics.Raycast(origin, transform.forward, out var hit,atkDistance );

        //var timeToAttack = atkSpeed;
        //timeToAttack -= timeToAttack * Time.deltaTime;

        //if (!(timeToAttack <= 0)) return;
        if (attackReach && hit.collider.CompareTag("Player"))
        {
            //hit.collider.gameObject.GetComponent<PlayerBehavioursScript>().Die(strength);
            print("YOU'RE DYING");
        }

        //timeToAttack = atkSpeed;
    }

    public override void Heal(float healAmount)
    {
        //print(name + " amount healed by: " + healAmount);
    }

    public override void Die(float atkDmg)
    {
        Health -= atkDmg;
        
        if (Health <= 0f)
        {
            GameController.enemiesDefeated.Add(gameObject);
            Destroy(gameObject);
        }
    }

    public override void UpdateMonster(Transform playerObj)
    {
        throw new System.NotImplementedException();
    }
}