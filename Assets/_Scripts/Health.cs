using System;
using Unity.VisualScripting;
public class Health 
{
    float _maxHealth;
    float _currentHealth;

    public event EventHandler<OnDamageEventArgs> OnDamange;
    public event EventHandler onDeath;

    public class OnDamageEventArgs : EventArgs
    {
        public float damage;
    }
    public Health(float health)
    {
        _maxHealth = health;
        _currentHealth = health;
    }
    public float GetHealth()
    {
        return _currentHealth;
    }
    public float GetHealthNormalized()
    {
        return (float)_currentHealth / _maxHealth;
    }
    public void Heal(float healAmount)
    {
        _currentHealth += healAmount;
        if( _currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }
    public void TakeDamage(float damageAmount)
    {
        _currentHealth -= damageAmount;
        OnDamange?.Invoke(this, new OnDamageEventArgs
        {
            damage = damageAmount,
        });
        if (_currentHealth <= 0)
        {
            //this died
            onDeath?.Invoke(this, EventArgs.Empty);
        }
    }
    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
    }
}
