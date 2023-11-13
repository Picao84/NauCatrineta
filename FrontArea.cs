using Godot;
using System;
using System.Linq;

public partial class FrontArea : Area2D
{
	static StringName BoatBackArea = "BackArea";
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var overlappingAreas = this.GetOverlappingAreas();

        if(overlappingAreas.Any(x => x.Name == BoatBackArea))
		{
			GetParent<Polygon2D>().ZIndex = 0;
		}
    }
}
