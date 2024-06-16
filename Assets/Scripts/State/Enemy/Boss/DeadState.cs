using LMS.Manager;
using LMS.State;

namespace LMS.Enemy.Boss
{
    public class DeadState : IState<BossMonster>
    {
        public IState<BossMonster> TransState(BossMonster obj)
        {
            if (obj.Hp <= 0) return null;
            if (obj.Hp > 0) return null;
            return null;
        }
        public void Enter(BossMonster obj)
        {
            if (!obj.TransformMode && (GameManager.Instance.ClearCount % 3).Equals(0))
            {
                obj.SetAnimation(MonsterInfo.bossIdleAnimName, false);
                obj.TransformInit();
            }
            else
            {
                obj.SetAnimation(MonsterInfo.bossDeadAnimName);
                obj.Dead();
            }
        }
        public void Action(BossMonster obj)
        {
        }
        public void Exit(BossMonster obj)
        {
        }
    }
}