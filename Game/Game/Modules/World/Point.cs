using Game.Modules.PlayerInteraction;

namespace Game.Modules.World;

public class Point
{
    public int X { get; }
    public int Y { get; }
    public Combat? Combat{ get; set; }
    
    public Point(int x, int y)
    {
        X = x;
        Y = y;
        Combat = null;
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }

    public bool IsEqualTo(Point point)
    {
        return X == point.X && Y == point.Y;
    }
    
    
    
    
    
    
    
}
/* Different types of PlayerInteractions:
 * Combat: player engages with an enemy in combat
 * Riddle: player must solve a riddle to exit room
 * Item: player has option to pick up loot
 
 All interactions will extend an interface:
 - Description
 - Begin method
 */
 
 /* World Events will use PlayerInteractions how they see fit
  
  */