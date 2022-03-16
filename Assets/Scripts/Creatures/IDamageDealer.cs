using Creatures;

public interface IDamageDealer
{
    float Damage { get; set; }
    void DealDamage(IDamageReceiver damageReceiver);
}