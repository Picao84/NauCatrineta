using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Caravela : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	AnimationPlayer player;
	Camera2D camera;
	GpuParticles2D particles;
	List<Node> flags = new List<Node>();

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public int Lives = 3;

    public override void _Ready()
    {
        base._Ready();

		camera = GetParent().GetParent().GetNode<Camera2D>("Camera2D");
		player = GetNode<AnimationPlayer>("Caravela Animation");
		particles = GetNode<GpuParticles2D>("Caravela Particles");
		flags.AddRange(GetChildren().Where(x => x.Name.ToString().Contains("Flag")));
		flags.Reverse();
		player.SpeedScale = 1.5f;
    }
    public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		//if (!IsOnFloor())
		//	velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
            velocity.Y = direction.Y * Speed;

			if (direction.X > 0)
			{
                velocity.X = direction.X * Speed;
			}

			if(direction.Y > 0)
			{
				particles.ZIndex = -1;
			}
			else
			{
				particles.ZIndex = 0;
            }
        }
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 100f, Speed);
            velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
            particles.ZIndex = 0;
			particles.Emitting = false;

        }

        player.Play("Moving Forward");

        Velocity = velocity;
		MoveAndSlide();
		camera.Position = new Vector2(Position.X - 200, camera.Position.Y);
    }

	public void Crash()
	{
		if(Lives > 0)
		{
			flags[Lives-1].QueueFree();
			Lives--;
		}
	}
}
