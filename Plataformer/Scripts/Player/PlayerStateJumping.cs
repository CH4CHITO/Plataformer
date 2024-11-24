using Godot;

public partial class PlayerStateJumping : PlayerStateGravityBase
{
    public override void Start()
    {
        Player.body.Color = new Color("ORANGE");

        // Detenemos el temporizador de coyote si está activo
        if (!Player.coyoteTimer.IsStopped())
        {
            Player.coyoteTimer.Stop();
        }

        // Si estamos en el estado Jumping desde Falling y podemos hacer un salto en el aire
        if (Player.movementStats.canJump)
        {
            ApplyJump();
            Player.movementStats.canJump = false; // Consume el primer salto
            GD.Print("Jump!");
        }
        else if (Player.movementStats.canAirJump && !Player.movementStats.canJump)
        {
            ApplyJump();
            Player.movementStats.canAirJump = false; // Consume el doble salto
            GD.Print("Air jump!");
        }
    }

    public override void OnPhysicsProcess(double delta)
    {
        // Evita volver a Falling si ya estamos en el estado de salto en el aire
        if (Player.Velocity.Y > 0 && !Player.IsOnFloor())
        {
            StateMachine.ChangeTo(PlayerStateNames.Falling);
            return;
        }

        // Movimiento horizontal en el aire
        float targetSpeed = Input.GetAxis("Left", "Right") * (Player.movementStats.horizontalMoving ? Player.movementStats.RunningSpeed : Player.movementStats.InAirSpeed);
        float acceleration = Player.movementStats.horizontalMoving ? Player.movementStats.RunningAcceleration : Player.movementStats.InAirAcceleration;
        Player.Velocity = new Vector2(Mathf.MoveToward(Player.Velocity.X, targetSpeed, acceleration * (float)delta), Player.Velocity.Y);
        //GD.Print("Target Speed: ", targetSpeed, "    ", "Acceleration:", acceleration);

        base.OnPhysicsProcess(delta); // Aplica gravedad
    }

    public void OnInput(InputEvent @event)
    {
        // Solo permite el salto en el aire si `canAirJump` es verdadero
        if (@event.IsActionPressed("Jump") && Player.movementStats.canAirJump)
        {
            ApplyJump();
            Player.movementStats.canAirJump = false; // Consume el doble salto
            GD.Print("Air jump!");
        }
        else if (@event.IsActionPressed("Dash") && Player.movementStats.canDash && Input.GetAxis("Left", "Right") != 0)
        {
            StateMachine.ChangeTo(PlayerStateNames.Dashing);
        }
        else if (Input.GetAxis("Left", "Right") == 0 && Player.movementStats.horizontalMoving)
        {
            Player.movementStats.horizontalMoving = false;
        }
    }

    private void ApplyJump()
    {
        Player.Velocity = new Vector2(Player.Velocity.X, Player.movementStats.JumpSpeed);
    }
}
