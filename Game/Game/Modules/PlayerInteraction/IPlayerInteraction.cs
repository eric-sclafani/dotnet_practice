namespace Game.Modules.PlayerInteraction;

public interface IPlayerInteraction
{
    public bool Resolved { get; set; }
    
    public void Begin();
}