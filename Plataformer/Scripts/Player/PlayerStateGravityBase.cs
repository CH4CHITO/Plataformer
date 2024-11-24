using Godot;

public partial class PlayerStateGravityBase : PlayerStateBase
{
    private float gravity = (float)ProjectSettings.GetSetting("physics/2d/default_gravity");

    public override void OnPhysicsProcess(double delta)
    {
        // Aplica la gravedad directamente aquí
        Player.Velocity = new Vector2(Player.Velocity.X, Player.Velocity.Y + gravity * (float)delta);
        Player.MoveAndSlide();
    }
}
