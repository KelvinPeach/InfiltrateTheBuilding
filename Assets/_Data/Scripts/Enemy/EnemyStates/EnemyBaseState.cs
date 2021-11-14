using UnityEngine;

namespace Infiltrate
{
    public abstract class EnemyBaseState
    {
        public abstract void EnterState(Enemy enemy);

        public abstract void Update(Enemy enemy);

        // public abstract void ExitState(Enemy enemy);
    }
}