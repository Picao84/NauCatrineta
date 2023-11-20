using Godot;
using System;
using System.Linq;

public partial class FrontArea : Area2D
{
	static StringName BoatBackArea = "BackArea";
    static StringName RockBackArea = "RockBackArea";
    static StringName WaveBackArea = "WaveBackArea";
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

        //if (this.Name == "WaveFrontArea" && overlappingAreas.Any(x => x.Name == RockBackArea))
        //{
        //    GetParent<Polygon2D>().ZIndex = 0;
        //}
        
        //if (this.Name == "RockFrontArea" && overlappingAreas.Any(x => x.Name == WaveBackArea))
        //{
        //    GetParent<Polygon2D>().ZIndex = 0;
        //}
    }
}
