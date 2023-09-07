using UnityEngine;

public interface IActions
{ 
    void Move(float speed, Vector3 destination);
    void Attack(float strength, float atkSpeed, float atkDistance);
    void Heal(float healAmount);
    void Die(float atkDmg);
}

