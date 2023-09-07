using System.Collections;
using UnityEngine;

public static class PlayerAttributes 
{
    public static float Health {get; set;} = 20f;
    public static float Strength { get; private set; } = 0.5f;
    public static float AtkSpeed { get; private set; } = 0.5f;
    public static float Speed { get; private set; } = 9f;
    public static float JumpForce { get; private set; } = 32f;
    public static float Reach { get; private set; } = 1.75f;
    public static Transform PlayerPos;

}
