using Godot;

public partial class Player : CharacterBody2D
{
    [Export]
    public CharacterMovementStats movementStats { get; set; } 
    public Timer coyoteTimer;
    public Timer dashTimer;
    public Polygon2D body;

    public override void _Ready()
    {
        body = GetNode<Polygon2D>("Body");
        coyoteTimer = GetNode<Timer>("Timers/CoyoteTimer");
        dashTimer = GetNode<Timer>("Timers/DashTimer");
    }

    public void OnCoyoteTimerTimeout() => movementStats.canJump = false; // Se desactiva el salto después del coyote time
    public void OnDashTimerTimeout() => movementStats.canDash = true; // Se activa el dash después del cooldown
}