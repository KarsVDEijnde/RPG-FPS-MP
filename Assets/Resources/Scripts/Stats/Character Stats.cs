using UnityEngine;

public class CharacterStats : MonoBehaviour {

    public int maxHealth;
    public int currentHealth { get; private set; }

    public Stat damage;
    public Stat armor;

    public void Awake()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        Debug.Log(transform.name + "takes" + damage + " Damage");

        if (currentHealth <= 0)
        {
            CommitDie();
        }
    }
    public virtual void CommitDie()
    {
        Debug.Log(transform.name + "Has Commited Toaster Bath");
    }
}
