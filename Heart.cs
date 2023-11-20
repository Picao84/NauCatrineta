using Godot;
using System;
using System.Threading.Tasks;

public partial class Heart : StaticBody2D
{
	Polygon2D heartPolygon;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		heartPolygon = GetNode<Polygon2D>("HeartPolygon");
        heartPolygon.Color = Colors.Red;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public async void Hit()
	{
		heartPolygon.Color = Colors.White;

		await Task.Delay(500);

		heartPolygon.Color = Colors.Red;

		((Adamastor)GetParent()).Hit();
	}
}
