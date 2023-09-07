using System;

using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Skeleton : Monster
{
    protected override float Health { get; set; } = 4f;
    protected override float Speed { get; set; } = 2.2f;
    protected override float MagicPoints { get; set; } = 10f;
    protected override float AtkReach { get; set; } = 1f;
    protected override float HealingPower { get; set; }
    protected override float AtkSpeed { get; set; } = 3f;
    protected override float Strength { get; set; }

    private NavMeshAgent _skeletonNavMesh;

    private States _skeletonState = States.Idling;
    
    private void Awake()
    {
        _skeletonNavMesh = GetComponent<NavMeshAgent>();

        print("MonsterType: " + name + " Hp: " + Health + " Mp: " + MagicPoints);

        Strength = Random.Range(0.6f, 2.2f); 
        HealingPower = (Health / 4) * Random.Range(0.125f, 0.75f);
    }

    private void FixedUpdate()
    {
        UpdateMonster(PlayerAttributes.PlayerPos);
    }

    public override void Move(float speed, Vector3 destination)
    {
        _skeletonNavMesh.speed = speed;
        
        _skeletonNavMesh.destination = destination;
    }

    public override void Attack(float strength, float atkSpeed, float atkDistance)
    {
        //print(name + " strength: " + strength + " " + name + " attack speed: " + atkSpeed + " attack reach: " + atkDistance);

        var skeletonTransform = transform;
        var skeletonPosition = skeletonTransform.position;
        
        var origin = new Vector3(skeletonPosition.x, skeletonPosition.y + 1f, skeletonPosition.z);
        
        var attackReach = Physics.Raycast(origin, skeletonTransform.forward, out var hit,atkDistance );

        if (!attackReach || !hit.collider.CompareTag("Player")) return;
        //hit.collider.gameObject.SetActive(false);
        print("YOU'RE DYING");
    }

    public override void Heal(float healAmount)
    {
        Mathf.RoundToInt(healAmount);
        if (!(MagicPoints > 0)) return;
        Health += healAmount;
        MagicPoints--;
    }

    public override void Die(float atkDmg)
    {
        Health -= atkDmg;

        if (!(Health <= 0f)) return;
        GameController.enemiesDefeated.Add(gameObject);
        Destroy(gameObject);
    }
    
    public override void UpdateMonster(Transform playerObj)
    {
        var distance = (transform.position - playerObj.position).magnitude;

        switch (_skeletonState)
        {
            case States.Attacking:
                if (Health < 1f) _skeletonState = States.Fleeing;
                break;
            
            case States.Fleeing:
                if (distance > 6f) _skeletonState = States.Healing;
                break;
            
            case States.Idling:
                if (distance <= 7f) _skeletonState = States.Patrolling;
                break;
            
            case States.Patrolling:
                if (distance <= AtkReach+3f) _skeletonState = States.Attacking;
                break;
            
            case States.UnderControl:
                break;

            case States.Healing:
                if (Health > 2f && MagicPoints > 0f) _skeletonState = States.Patrolling;
                else _skeletonState = States.Healing;
                break;
            
            default:
                _skeletonState = States.Idling;
                break;
        }
        DoAction(PlayerAttributes.PlayerPos, transform, _skeletonState, this);
    }
}