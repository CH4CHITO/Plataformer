using Godot;

public partial class PlayerStateIdle : PlayerStateGravityBase
{
    public override void Start()
    {
        Player.body.Color = new Color("BLUE");
        Player.movementStats.canJump = true;
        Player.movementStats.canAirJump = true;
        Player.movementStats.horizontalMoving = false;
    }

    public override void OnPhysicsProcess(double delta)
    {
        // Cambia al estado Falling si empieza a caer
        if (Player.Velocity.Y > 0f)
        {
            StateMachine.ChangeTo(PlayerStateNames.Falling);
            return;
        }

        // Suaviza el movimiento hacia velocidad cero
        Player.Velocity = new Vector2(Mathf.MoveToward(Player.Velocity.X, 0f, Player.movementStats.RunningDeceleration * (float)delta), Player.Velocity.Y);

        base.OnPhysicsProcess(delta); // Llama al método base para aplicar gravedad
    }

    public void OnInput(InputEvent @event)
    {
        // Transición a Jumping si se pulsa "Jump"
        if (@event.IsActionPressed("Jump"))
        {
            StateMachine.ChangeTo(PlayerStateNames.Jumping);
        }
        else if (@event.IsActionPressed("Crouch"))
        {
            StateMachine.ChangeTo(PlayerStateNames.Crouched);
        }
        // Cambia a Running si hay movimiento horizontal
        else if (Input.GetAxis("Left", "Right") != 0)
        {
            StateMachine.ChangeTo(PlayerStateNames.Running);
        }
    }
}
