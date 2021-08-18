namespace Prototype02.Zombie
{
    public class ZombieDeathState : ZombieState
    {
        public ZombieDeathState(ZombieController zombieController, ZombieData zombieData, ZombieStateMachine zombieStateMachine) : base(zombieController, zombieData, zombieStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            zombieController.Animator.SetBool(AnimStates.DeathAnimId, true);
            zombieController.ZombieIdleCollider.enabled = false;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            var animating = zombieController.Animator.IsAnimationPlaying(AnimStates.ZombieDeathAnimName);
            // TODO: fix this
            if (!animating)
            {
                Pool.Instance.ReturnToPool(zombieController.gameObject);
            }
        }

        public override void Exit()
        {
            base.Exit();
            zombieController.Animator.SetBool(AnimStates.DeathAnimId, false);
        }
    }
}