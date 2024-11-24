using Godot;

public partial class PlayerStateRunning : PlayerStateGravityBase
{
    public override void Start()
    {
        Player.body.Color = new Color("GREEN");
        Player.movementStats.canJump = true;
        Player.movementStats.canAirJump = true;
        Player.movementStats.horizontalMoving = true;
    }

    public override void OnPhysicsProcess(double delta)
    {
        // Control de tiempo de coyote
        if (!Player.IsOnFloor() && Player.coyoteTimer.IsStopped())
        {
            Player.coyoteTimer.Start();
        }

        // Transición a Falling si está en el aire sin coyote time
        if (Player.Velocity.Y > 0 && !Player.movementStats.canJump)
        {
            StateMachine.ChangeTo(PlayerStateNames.Falling);
        }

        // Mueve hacia el objetivo de velocidad horizontal
        float targetSpeed = Input.GetAxis("Left", "Right") * Player.movementStats.RunningSpeed;
        Player.Velocity = new Vector2(Mathf.MoveToward(Player.Velocity.X, targetSpeed, Player.movementStats.RunningAcceleration * (float)delta), Player.Velocity.Y);
        //GD.Print("Target Speed: ", targetSpeed, "    ", "Acceleration:", Player.movementStats.RunningAcceleration);

        base.OnPhysicsProcess(delta); // Llama al método base para aplicar gravedad
    }

    public void OnInput(InputEvent @event)
    {
        // Transición a Jumping si se presiona "Jump"
        if (@event.IsActionPressed("Jump"))
        {
            StateMachine.ChangeTo(PlayerStateNames.Jumping);
        }
        else if (@event.IsActionPressed("Dash") && Player.movementStats.canDash && Input.GetAxis("Left", "Right") != 0)
        {
            StateMachine.ChangeTo(PlayerStateNames.Dashing);
        }
        else if (@event.IsActionPressed("Crouch"))
        {
            StateMachine.ChangeTo(PlayerStateNames.Crouched);
        }
        // Transición a Idle si no hay movimiento y está en el suelo
        else if (Input.GetAxis("Left", "Right") == 0 && Player.IsOnFloor())
        {
            StateMachine.ChangeTo(PlayerStateNames.Idle);
        }
    }
}
