using Godot;

public partial class PlayerStateDashing : PlayerStateBase
{
    private Vector2 dashDirection;
    private float dashTime; // Tiempo acumulado en el dash

    public override void Start()
    {
        Player.body.Color = new Color("YELLOW"); // Cambia de color para identificar el estado
        if (Input.GetAxis("Left", "Right") != 0) //Comprueba que el vector de movimiento no sea cero
        {
            dashDirection = new Vector2(Input.GetAxis("Left", "Right"), 0).Normalized();
        }
        else
        {
            dashDirection = Vector2.Right;
        }
        Player.Velocity = dashDirection * Player.movementStats.dashSpeed;
        dashTime = 0;
    } 

    public override void OnPhysicsProcess(double delta)
    {
        // Incrementa el tiempo de dash y verifica la condición para salir del estado Dashing
        dashTime += (float)delta;
        if (dashTime >= Player.movementStats.dashDuration || Player.IsOnWall()) // Termina al alcanzar la duración o chocar con una pared
        {
			if (Player.IsOnFloor())
			{
            	StateMachine.ChangeTo(Input.GetAxis("Left", "Right") != 0 ? PlayerStateNames.Running : PlayerStateNames.Idle);
			}
			else
			{
                Player.movementStats.horizontalMoving = false;
				StateMachine.ChangeTo(PlayerStateNames.Falling);
			}

			Player.dashTimer.Start();
			Player.movementStats.canDash = false;
        }

		// Mantiene el desplazamiento en la dirección del dash
        Player.MoveAndSlide();
    }
}
