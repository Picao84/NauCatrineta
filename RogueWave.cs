using Godot;
using System;
using System.Linq;

public partial class RogueWave : Path2D
{
    AnimationPlayer player;
    PathFollow2D path;
    Polygon2D wavePolygon;
    public Camera2D camera { get; set; }
    bool shouldUpdatePath = true;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        ZIndex = 2;
        player = GetNode<AnimationPlayer>("Rogue Wave Animation");
        player.Play("Wave Up");
        path = GetNode<PathFollow2D>("PathFollow2D");
        path.Loop = false;
        wavePolygon = path.GetNode<Polygon2D>("Wave/Body");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (shouldUpdatePath)
        {
            path.Progress = (float)(path.Progress + delta * 150);
            var cameraPosition = camera.GlobalPosition.X;
            if (path.GlobalPosition.X + wavePolygon.Polygon.Max(x => x.X) < cameraPosition || path.ProgressRatio == 1.0f)
            {
                QueueFree();
            }
        }
    }

    public void Crash()
    {
        shouldUpdatePath = false;
        ZIndex = 2;
        player.Play("Wave Down");
        player.AnimationFinished += Player_AnimationFinished;
    }

    private void Player_AnimationFinished(StringName animName)
    {
        QueueFree();
    }

    public void GameOver()
    {
        shouldUpdatePath = false;
    }
}
