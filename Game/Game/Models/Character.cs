namespace Game.Models;

public class Character
{
    public string Name { get; }
    private int _maxHealth;
    public int CurrentHealth { get; private set; }
    private int MinDmg { get; set; }
    private int MaxDmg { get; set; }

    protected Character(
        string name,
        int maxHealth,
        int minDmg,
        int maxDmg
    )
    {
        Name = name;
        _maxHealth = CurrentHealth = maxHealth;
        MinDmg = minDmg;
        MaxDmg = maxDmg;
    }

    public bool IsAlive() => CurrentHealth >= 1;

    public void ReceiveDamage(int incomingDmg)
    {
        var amount = CurrentHealth - incomingDmg;
        CurrentHealth = amount <= 0 ? 0 : amount;
    }

    // TODO: add accuracy system, chance to miss attack
    public int DealDamage()
    {
        var r = new Random();
        return r.Next(MinDmg, MaxDmg + 1);
    }
}