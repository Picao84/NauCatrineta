using Godot;
using System;

public partial class AdamastorWaveCollision : Area2D
{
    public bool registerAreas { get; set; } = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        this.BodyEntered += AdamastorWaveCollision_BodyEntered;
	}

    private void AdamastorWaveCollision_BodyEntered(Node2D body)
    {
        ((Main)GetParent().GetParent()).CreatWaveAt(GlobalPosition);


    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
        var overlappingAreas = GetOverlappingAreas();

        if (registerAreas && overlappingAreas != null && overlappingAreas.Count > 0 && (overlappingAreas[0].Name == "BottomLeftArm" || overlappingAreas[0].Name == "BottomRightArm")) 
        { 
            registerAreas = false;
            ((Main)GetParent().GetParent()).CreatWaveAt(GlobalPosition);
        }
	}
}
