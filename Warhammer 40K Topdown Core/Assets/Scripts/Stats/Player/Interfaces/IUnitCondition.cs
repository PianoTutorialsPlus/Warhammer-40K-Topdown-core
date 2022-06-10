using UnityEngine;

namespace WH40K.Stats.Player
{
    public interface IUnitCondition
    {
        void Activate();
        void Destroy();
        void ResetData();
        void TakeDamage(int damage);
        void Freeze();
        void SetDestination(Vector3 position);
    }
}