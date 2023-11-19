using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

public partial class Caravela : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	AnimationPlayer player;
	Camera2D camera;
	GpuParticles2D particles;
    GpuParticles2D waveCrashParticles;
    GpuParticles2D rockCrashParticles;
    List<Node> flags = new List<Node>();
	Polygon2D Body;
	bool stoppedWaves;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public int Lives = 3;

    public override void _Ready()
    {
        base._Ready();

		camera = GetParent().GetParent().GetNode<Camera2D>("Camera2D");
		player = GetNode<AnimationPlayer>("Caravela Animation");
		particles = GetNode<GpuParticles2D>("Caravela Particles");
        waveCrashParticles = GetNode<GpuParticles2D>("WaveCrashParticles");
        rockCrashParticles = GetNode<GpuParticles2D>("RockCrashParticles");
		Body = GetNode<Polygon2D>("Body");
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

		var maxX = Body.Polygon.Max(x => x.X);

        // Handle Jump.
        if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			
			velocity.Y = direction.Y * Speed;
		

			if (direction.X > 0 && GlobalPosition.X + maxX  <= 8520)
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
			if (GlobalPosition.X + maxX <= 8250)
			{
				velocity.X = Mathf.MoveToward(Velocity.X, 100f, Speed);			
			}

            velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
            particles.ZIndex = 0;

        }

        player.Play("Moving Forward");

		if(GlobalPosition.X + maxX >= 8250)
		{
			Velocity = new Vector2 (0, velocity.Y);
		}
		else
		{
            Velocity = velocity;
        }
       
		MoveAndSlide();
		camera.Position = new Vector2(Position.X - 200, camera.Position.Y);

		if(GlobalPosition.X + maxX > 7000 && !stoppedWaves) 
		{
			stoppedWaves = true;
			((Main)GetParent().GetParent()).StopWaves();
            ((Main)GetParent().GetParent()).ArrivedAtEnd();
        }
    }

	public void Crash(bool isWave = true)
	{
		if(Lives > 0)
		{
			var lastCollision = GetLastSlideCollision();

			if(lastCollision != null)
			if (isWave)
			{
				waveCrashParticles.GlobalPosition = lastCollision.GetPosition();
				waveCrashParticles.Emitting = true;
			}
			else
			{
                rockCrashParticles.GlobalPosition = lastCollision.GetPosition();
                rockCrashParticles.Emitting = true;
            }
			var animation = player.GetAnimation("Moving Forward");
			animation.RemoveTrack(0);
			flags[Lives-1].QueueFree();
			Lives--;
		}
	}
}
