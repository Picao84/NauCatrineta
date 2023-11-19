using Godot;
using System;
using System.Linq;

public partial class Main : Node2D
{
	PackedScene Boat;
	Timer waveTimer = new Timer();
    Timer adamastorTimer = new Timer();
    Node2D rogueWave;
    Camera2D camera;
    PackedScene AdamastorScene;
    Adamastor Adamastor;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		waveTimer.WaitTime = 2;
        waveTimer.Timeout += WaveTimer_Timeout;
        AddChild(waveTimer);
        waveTimer.Start();

        adamastorTimer.WaitTime = 5;
        adamastorTimer.Timeout += Adamastor_Timeout;
        AddChild(adamastorTimer);

        camera = GetNode<Camera2D>("Camera2D");
		Boat = (PackedScene)ResourceLoader.Load("res://boat.tscn");
        AdamastorScene = (PackedScene)ResourceLoader.Load("res://Adamastor.tscn");
        AddChild(Boat.Instantiate());
        AddChild(AdamastorScene.Instantiate());

        Adamastor = GetNode<Adamastor>("AdamastorRoot/Adamastor");

        var rogueWaveScene = (PackedScene)ResourceLoader.Load("res://roguewave.tscn");
		rogueWave = (Node2D) rogueWaveScene.Instantiate();

        var newWave = (RogueWave)rogueWave.Duplicate();
        newWave.camera = camera;
        var randomY = new Random().Next(0, (int)(camera.GetViewportRect().End.Y - newWave.GetNode<Polygon2D>("PathFollow2D/Wave/Body").Polygon.Max(x => x.Y)));
        newWave.GlobalPosition = new Vector2(camera.GlobalPosition.X + 500, randomY);
        AddChild(newWave);
    }

    private void Adamastor_Timeout()
    {
        Random random = new Random();
        Adamastor.PlayAnimation(random.Next(0, Adamastor.AnimationCount - 1));

    }

    private void WaveTimer_Timeout()
    {
        var newWave = (RogueWave)rogueWave.Duplicate();
        newWave.camera = camera;
        var randomY = new Random().Next(0, (int)(camera.GetViewportRect().End.Y - newWave.GetNode<Polygon2D>("PathFollow2D/Wave/Body").Polygon.Max(x => x.Y)));
        newWave.GlobalPosition = new Vector2(camera.GlobalPosition.X + 750, randomY);
        AddChild(newWave);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
       
    }

    public void ArrivedAtEnd()
    {
        adamastorTimer.Start();
    }

    public void StopWaves()
    {
        waveTimer.Stop();
    }

    public void CreatWaveAt(Vector2 position)
    {
        var newWave = (RogueWave)rogueWave.Duplicate();
        newWave.camera = camera;

        var waveHeight = newWave.GetNode<Polygon2D>("PathFollow2D/Wave/Body").Polygon.Max(x => x.Y);

        newWave.GlobalPosition = new Vector2(position.X - newWave.Curve.GetBakedLength() + newWave.GetNode<Polygon2D>("PathFollow2D/Wave/Body").Polygon.Max(x => x.X), position.Y);

        AddChild(newWave);
        newWave.Visible = true;
    }
}
