using UnityEngine;

namespace Prototype02
{
    public static class AnimStates
    {
        public const string Attack1AnimName = "Attack1";
        public const string Attack2AnimName = "Attack2";
        public const string Attack3AnimName = "Attack3";
        public const string HurtAnimName = "Hurt";
        public const string BlockAnimName = "Block";
        public const string DeathAnimName = "Death";
        public const string SpawnAnimName = "Spawn";
        public const string ZombieAttackAnimName = "Attack";
        public const string ZombieDeathAnimName = "Zombie_Death";

        // Hero
        public static readonly int GroundedAnimId = Animator.StringToHash("Grounded");
        public static readonly int AirSpeedYAnimId = Animator.StringToHash("AirSpeedY");
        public static readonly int Attack1StateId = Animator.StringToHash(Attack1AnimName);
        public static readonly int Attack2StateId = Animator.StringToHash(Attack2AnimName);
        public static readonly int Attack3StateId = Animator.StringToHash(Attack3AnimName);
        public static readonly int JumpStateId = Animator.StringToHash("Jump");
        public static readonly int IdleBlockId = Animator.StringToHash("IdleBlock");
        public static readonly int IdleBlockTriggerId = Animator.StringToHash("IdleBlockTrigger");
        public static readonly int BlockId = Animator.StringToHash(BlockAnimName);
        
        // Common
        public static readonly int HurtAnimId = Animator.StringToHash(HurtAnimName);
        public static readonly int AnimStateId = Animator.StringToHash("AnimState");
        public static readonly int DeathAnimId = Animator.StringToHash("Death");
        
        // Zombie
        public static readonly int SpawnAnimId = Animator.StringToHash(SpawnAnimName);
        public static readonly int ZombieAttackAnimId = Animator.StringToHash(ZombieAttackAnimName);
    }
}