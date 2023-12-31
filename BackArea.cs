using Godot;
using System;
using System.Linq;

public partial class BackArea : Area2D
{
    static StringName BoatFrontArea = "FrontArea";
    static StringName RockFrontArea = "RockFrontArea";
    static StringName WaveFrontArea = "WaveFrontArea";
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        var overlappingAreas = this.GetOverlappingAreas();

        if (overlappingAreas.Any(x => x.Name == BoatFrontArea))
        {
            GetParent<Polygon2D>().ZIndex = -2;
        }

        //if (this.Name == "WaveBackArea" && overlappingAreas.Any(x => x.Name == RockFrontArea))
        //{
        //    GetParent<Polygon2D>().ZIndex = -2;
        //}
        
        //if (this.Name == "RockBackArea" && overlappingAreas.Any(x => x.Name == WaveFrontArea))
        //{
        //    GetParent<Polygon2D>().ZIndex = -2;
        //}
    }
}
