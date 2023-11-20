using Godot;
using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

public partial class Canon : Path2D
{
    Godot.Timer timer;
	PathFollow2D path;
	GpuParticles2D particles;
	Polygon2D ball;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        path = GetNode<PathFollow2D>("PathFollow2D");
		ball = path.GetNode<Polygon2D>("CanonArea/Polygon2D");
		ball.Visible = false;
        particles = path.GetNode<GpuParticles2D>("CanonArea/Crash Particles");
		timer = new Godot.Timer();
		timer.WaitTime = 1;
        timer.Autostart = false;
        timer.Timeout += Timer_Timeout;
        AddChild(timer);
    }

    private void Timer_Timeout()
    {
        CanShoot = true;
        path.ProgressRatio = 0;
        timer.Stop();
    }

    public bool CanShoot { get; private set; } = true;
    private bool isShooting;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(isShooting)
        {
            path.Progress = (float)(path.Progress + delta * 1000);
			if(path.ProgressRatio >= 0.95f)
			{
                isShooting = false;
                particles.Emitting = true;
                ball.Visible = false;
                timer.Start();
            }
        }

    }


	public void Crashed()
	{
        particles.Emitting = true;
        ball.Visible = false;
        timer.Start();
    }

    public void Shoot()
    {
        ball.Visible = true;
        isShooting = true;
        CanShoot = false;
	}
}
