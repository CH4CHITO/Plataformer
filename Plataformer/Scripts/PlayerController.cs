using Godot;

public partial class PlayerController : CharacterBody2D
{
	// Variables constantes
	private const float HorizontalVelocity = 400.0f;
	private const float JumpVelocity = -400.0f;
	private const float DashVelocity = 800.0f;
	private const float Acceleration = 0.1f;
	private const float Friction = 0.15f;

	// Gravedad del mundo
	private float _gravity;
	// Estado de dash actual
	private bool _isDashing = false;
	// Disponibilidad de realizar dash
	private bool _dashAvailable = true;
	// Disponibilidad de realizar salto aereo
	private bool _aerialJumpAvailable = false;
	private Timer _dashTimer;
	private Timer _coyoteTimer;
    public override void _Ready()
    {
        _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
		_dashTimer = GetNode<Timer>("Timers/DashTimer");
		_coyoteTimer = GetNode<Timer>("Timers/CoyoteTimer");
    }

    public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		int horizontalInput = 0;

		if (_dashAvailable && Input.IsActionJustPressed("Dash"))
		{
			if (Input.IsActionPressed("Right") && !Input.IsActionPressed("Left"))
			{
				velocity.X = DashVelocity;
				velocity.Y = 0;
				_dashTimer.Start();
				_isDashing = true;
				_dashAvailable = false;
			}
			else if (Input.IsActionPressed("Left") && !Input.IsActionPressed("Right"))
			{
				velocity.X = -DashVelocity;
				velocity.Y = 0;
				_dashTimer.Start();
				_isDashing = true;
				_dashAvailable = false;
			}
		}

		if (!_isDashing)
		{
			if (Input.IsActionPressed("Right"))
				horizontalInput += 1;
			if (Input.IsActionPressed("Left"))
				horizontalInput -= 1;

			if (horizontalInput != 0)
				velocity.X = Mathf.Lerp(velocity.X, horizontalInput * HorizontalVelocity, Acceleration);
			else
				velocity.X = Mathf.Lerp(velocity.X, 0f, Friction);

			if (Input.IsActionJustPressed("Jump") && (IsOnFloor() || !_coyoteTimer.IsStopped()))
			{
				velocity.Y = JumpVelocity;
				_coyoteTimer.Stop();
			}
			else if (Input.IsActionJustPressed("Jump") && _aerialJumpAvailable)
			{
				velocity.Y = JumpVelocity;
				_aerialJumpAvailable = false;
			}
			else if (IsOnFloor())
			{
				velocity.Y = 0f;
				_aerialJumpAvailable = true;
				_dashAvailable = true;
			}
			else
			{
				velocity.Y += _gravity * (float)delta;
			}
		}
		else
		{
			if(IsOnWall()){
				velocity.X = 0f;
				_dashTimer.Stop();
				_isDashing = false;
			}
		}

		bool wasOnFloor = IsOnFloor();
		Velocity = velocity;
		MoveAndSlide();

		if(wasOnFloor && !IsOnFloor()){
			_coyoteTimer.Start();
		}
	}

	private void OnDashTimerTimeout(){
		_isDashing = false;
	}
}
