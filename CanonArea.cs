using Godot;
using System;

public partial class CanonArea : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        this.BodyEntered += CanonArea_BodyEntered;
	}

    private void CanonArea_BodyEntered(Node2D body)
    {
        if(body is Heart heart)
        {
            ((Canon)GetParent().GetParent()).Crashed();
            heart.Hit();

        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
