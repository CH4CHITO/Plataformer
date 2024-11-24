using Godot;
public partial class WeaponController : Node2D
{
	private const float AngularVelocity = 0.5f;
	private PackedScene _projectileScene = GD.Load<PackedScene>("res://Scenes/Projectile.tscn");
	private Polygon2D _weaponSprite;
	private Marker2D _projectileSpawner;

	public override void _Ready()
	{
		_weaponSprite = GetNode<Polygon2D>("WeaponSprite");
		_projectileSpawner = GetNode<Marker2D>("WeaponSprite/ProjectileSpawner");
	}

	public override void _PhysicsProcess(double delta)
	{
		float rotationAngle;
		rotationAngle = GetAngleTo(GetGlobalMousePosition()) + Mathf.Pi / 2;
		_weaponSprite.GlobalRotation = Mathf.LerpAngle(_weaponSprite.GlobalRotation, rotationAngle, AngularVelocity);

		if(Input.IsActionJustPressed("Fire")){
			Fire();
		}
	}
	
	private void Fire(){
		ProjectileController projectile = _projectileScene.Instantiate<ProjectileController>();
		projectile.Transform = _projectileSpawner.GlobalTransform;
		GetTree().Root.GetNode("Main").AddChild(projectile);

	}
}
