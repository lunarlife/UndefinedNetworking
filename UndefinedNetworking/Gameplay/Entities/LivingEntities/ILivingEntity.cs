namespace UndefinedNetworking.Gameplay.Entities.LivingEntities
{
    public interface ILivingEntity : IDamager, IEntity
    {
        public float Health { get; set; }
        public float MaxHealth { get; set; }
        public IDamager LastDamager { get; }
        public void Kill();
        public void ApplyDamage(float damage,  IDamager damager);
    }
}