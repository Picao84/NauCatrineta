using Godot;
using System;
using System.Linq;

public partial class Main : Node2D
{
	PackedScene Boat;
	Timer waveTimer = new Timer();
	Node2D rogueWave;
    Camera2D camera;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		waveTimer.WaitTime = 3;
        waveTimer.Timeout += WaveTimer_Timeout;
        AddChild(waveTimer);
        waveTimer.Start();

        camera = GetNode<Camera2D>("Camera2D");
		Boat = (PackedScene)ResourceLoader.Load("res://boat.tscn");
        AddChild(Boat.Instantiate());

        var rogueWaveScene = (PackedScene)ResourceLoader.Load("res://roguewave.tscn");
		rogueWave = (Node2D) rogueWaveScene.Instantiate();

        var newWave = (RogueWave)rogueWave.Duplicate();
        newWave.camera = camera;
        var randomY = new Random().Next(0, (int)(camera.GetViewportRect().End.Y - newWave.GetNode<Polygon2D>("PathFollow2D/Wave/Body").Polygon.Max(x => x.Y)));
        newWave.GlobalPosition = new Vector2(camera.GlobalPosition.X + 200, randomY);
        AddChild(newWave);
    }

    private void WaveTimer_Timeout()
    {
        var newWave = (RogueWave)rogueWave.Duplicate();
        newWave.camera = camera;
        var randomY = new Random().Next(0, (int)(camera.GetViewportRect().End.Y - newWave.GetNode<Polygon2D>("PathFollow2D/Wave/Body").Polygon.Max(x => x.Y)));
        newWave.GlobalPosition = new Vector2(camera.GlobalPosition.X + 200, randomY);
        AddChild(newWave);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
       
    }
}
