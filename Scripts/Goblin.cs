using UnityEngine;
using UnityEngine.AI;

public class Goblin : Monster
{
    protected override float Health { get; set; } = 5f;
    protected override float Speed { get; set; } = 3f;
    protected override float MagicPoints { get; set; } = 0f;
    protected override float AtkReach { get; set; } = 1.1f;
    protected override float HealingPower { get; set; }
    protected override float AtkSpeed { get; set; } = 0.7f;
    protected override float Strength { get; set; }

    private NavMeshAgent _goblinNavMesh;

    private void Awake()
    {
        _goblinNavMesh = GetComponent<NavMeshAgent>();
        
        print("MonsterType: " + name + " Hp: " + Health + " Mp: " + MagicPoints);

        Strength = Random.Range(1f, 3.7f); 
        HealingPower = (Health / 4) * Random.Range(0.125f, 0.75f);
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
        
        _goblinNavMesh.speed = speed;

        _goblinNavMesh.destination = PlayerAttributes.PlayerPos.position;
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