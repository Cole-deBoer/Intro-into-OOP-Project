using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public abstract class Monster : MonoBehaviour, IActions
    {
        protected enum States
        {
            Attacking,
            Fleeing,
            Patrolling,
            UnderControl,
            Healing,
            Idling
        }
        
        protected abstract float Health { get; set; }
        protected abstract float Speed { get; set; }
        protected abstract float MagicPoints { get; set; }
        protected abstract float AtkReach { get; set; }
        protected abstract float HealingPower { get; set; }
        protected abstract float AtkSpeed { get; set; }
        protected abstract float Strength { get; set; }

        public abstract void Move(float speed, Vector3 destination);
        public abstract void Attack(float strength, float atkSpeed, float atkDistance);
        public abstract void Heal(float healAmount);
        public abstract void Die(float atkDmg);
        public abstract void UpdateMonster(Transform playerObj);

        protected void DoAction(Transform playerObj, Transform enemyObj, States enemyMode, Monster monster)
        {

            switch (enemyMode)
            {
                case States.Attacking:
                    print("attacking");
                    monster.Move(monster.Speed, playerObj.position); 
                    monster.Attack(monster.Strength, monster.AtkSpeed, monster.AtkReach);
                    break;
                
                case States.Fleeing:
                    var mask = LayerMask.GetMask("Environment");
                    print("fleeing");
                    enemyObj.transform.rotation = Quaternion.LookRotation(enemyObj.position - playerObj.position);
                    Physics.Raycast(new Vector3(enemyObj.position.x, enemyObj.position.y + 1, enemyObj.position.z), enemyObj.forward, out var hit, 10, mask); 
                    Debug.DrawRay(new Vector3(enemyObj.position.x, enemyObj.position.y + 1, enemyObj.position.z), enemyObj.forward * 10, Color.magenta);
                    monster.Move(monster.Speed * 2f, hit.point);
                    break;
                
                case States.Healing:
                    print("healing");
                    monster.Move(monster.Speed, enemyObj.position);
                    monster.Heal(monster.HealingPower);
                    break;
                    

                case States.Patrolling:
                    print("patrolling");
                    var randomRotation = Random.Range(0, 360);
                    enemyObj.transform.rotation = Quaternion.LookRotation(new Vector3(randomRotation,0,randomRotation));
                    Physics.Raycast(new Vector3(enemyObj.position.x, enemyObj.position.y + 1, enemyObj.position.z), Vector3.forward, out var randomPoint);
                    Move(monster.Speed, randomPoint.point);
                    break;

                case States.Idling:
                    break;
                
                case States.UnderControl:
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(enemyMode), enemyMode, null);
            }
        }
    }