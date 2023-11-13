using Godot;
using System;

public partial class WaveCollisionArea : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        BodyEntered += WaveCollisionArea_BodyEntered;
	}

    private void WaveCollisionArea_BodyEntered(Node2D body)
    {
        if(body is Caravela caravela)
        {
            QueueFree();
            caravela.Crash();
            GetParent<Wave>().Crash();
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
