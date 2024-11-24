using Godot;

public partial class PlayerStateFalling : PlayerStateGravityBase
{
    public override void Start()
    {
        Player.body.Color = new Color("RED");
        Player.movementStats.canJump = false;
    }

    public override void OnPhysicsProcess(double delta)
    {
        // Cambia a Idle o Running al tocar el suelo
        if (Player.IsOnFloor())
        {
            StateMachine.ChangeTo(Input.GetAxis("Left", "Right") != 0 ? PlayerStateNames.Running : PlayerStateNames.Idle);
            return;
        }

        // Control de movimiento horizontal en el aire
        float targetSpeed = Input.GetAxis("Left", "Right") * (Player.movementStats.horizontalMoving ? Player.movementStats.RunningSpeed : Player.movementStats.InAirSpeed);
        float acceleration = Player.movementStats.horizontalMoving ? Player.movementStats.RunningAcceleration : Player.movementStats.InAirAcceleration;
        Player.Velocity = new Vector2(Mathf.MoveToward(Player.Velocity.X, targetSpeed, acceleration * (float)delta), Player.Velocity.Y);
        //GD.Print("Target Speed: ", targetSpeed, "    ", "Acceleration:", acceleration);

        base.OnPhysicsProcess(delta); // Aplica gravedad
    }

    public void OnInput(InputEvent @event)
    {
        // Permite doble salto en el aire
        if (@event.IsActionPressed("Jump") && Player.movementStats.canAirJump)
        {
            StateMachine.ChangeTo(PlayerStateNames.Jumping);
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
}


