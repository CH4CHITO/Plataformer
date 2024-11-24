using Godot;

public partial class PlayerStateCrouched : PlayerStateGravityBase
{
    public override void Start()
    {
        Player.body.Color = new Color("BROWN");
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
        // Transición a Idle si se suelta "Crouch"
        if (Input.IsActionJustReleased("Crouch"))
        {
            StateMachine.ChangeTo(Input.GetAxis("Left", "Right") != 0 ? PlayerStateNames.Running : PlayerStateNames.Idle);
        }
    }
}
