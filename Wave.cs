using Godot;
using System;

public partial class Wave : StaticBody2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Crash()
	{
		GetNode<CollisionPolygon2D>("WaveCollision").QueueFree();
		GetParent().GetParent<RogueWave>().Crash();
	}
}
