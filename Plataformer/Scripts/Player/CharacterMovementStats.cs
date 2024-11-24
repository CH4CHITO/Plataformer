using Godot;

public partial class CharacterMovementStats : Resource
{
	[Export]
    public float RunningSpeed { get; set; } = 400.0f;

    [Export]
    public float RunningAcceleration { get; set; } = 1500.0f;

    [Export]
    public float RunningDeceleration { get; set; } = 5000.0f;

    [Export]
    public float JumpSpeed { get; set; } = -400.0f;

    [Export]
    public float InAirSpeed { get; set; } = 300.0f;

    [Export]
    public float InAirAcceleration { get; set; } = 1500.0f;

    [Export]
    public float dashSpeed { get; set; } = 1000.0f;

    [Export]
    public float dashDuration { get; set; } = 0.5f;
    
    [Export] 
    public bool canJump { get; set; } = true;

    [Export] 
    public bool canAirJump { get; set; } = true;
    
    [Export] 
    public bool canDash { get; set; } = true;
      
    [Export] 
    public bool horizontalMoving { get; set; } = false;
}