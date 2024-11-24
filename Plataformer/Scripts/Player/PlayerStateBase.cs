using Godot;

public partial class PlayerStateBase : StateBase
{
    public Player Player 
    {
        get => (Player)ControlledNode;
        set => ControlledNode = value;
    }
}