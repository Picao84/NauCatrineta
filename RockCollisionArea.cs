using Godot;
using System;

public partial class RockCollisionArea : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        BodyEntered += RockCollisionArea_BodyEntered;
	}

    private void RockCollisionArea_BodyEntered(Node2D body)
    {
       if(body is Caravela boat)
        {
            boat.Crash(false);
            QueueFree();
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
