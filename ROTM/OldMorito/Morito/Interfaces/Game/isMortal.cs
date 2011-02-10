
namespace Morito
{
    public interface isMortal
    {
        float MaxHealth { get; set; }
        float Health { get; set; }

        double TimeOfDeath { get; set; }
        bool Died { get; set; }

        bool IsAnimateDeath { get; set; }
        double AnimateDeathTime { get; set; }

        bool IsDead();
        bool IsAlive();
        void TakeDamage(float damage);
    }
}
