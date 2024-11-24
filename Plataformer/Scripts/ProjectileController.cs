using Godot;

public partial class ProjectileController : Area2D
{
	private const float ProjectileVelocity = 700f;

	public override void _PhysicsProcess(double delta)
	{
		Position += Transform.X * ProjectileVelocity * (float)delta;
	}

	private void OnBodyEntered(PhysicsBody2D body){
		GD.Print("Hit!");
		QueueFree();
	}
}
