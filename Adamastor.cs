using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

public partial class Adamastor : Node2D
{
    // Called when the node enters the scene tree for the first time.

    AnimationPlayer player;
    string[] animationList = new string[0];

    AdamastorWaveCollision farleftCollisionArea;
    AdamastorWaveCollision leftCollisionArea;
    AdamastorWaveCollision leftCenterCollisionArea;
    AdamastorWaveCollision farrightCollisionArea;
    AdamastorWaveCollision rightCollisionArea;
    AdamastorWaveCollision rightCenterCollisionArea;

    const int MAX_LIFE = 10;

    Polygon2D LeftEye;
    Polygon2D RightEye;

    int Life = MAX_LIFE;

    Dictionary<int, Color> HpColors = new Dictionary<int, Color>()
    {
        { 10, Colors.DarkGreen },
        { 9, Colors.Green },
        { 8, Colors.LightGreen },
        { 7, Colors.GreenYellow },
        { 6, Colors.YellowGreen },
        { 5, Colors.Yellow },
        { 4, Colors.Orange },
        { 3, Colors.DarkOrange },
        { 2, Colors.OrangeRed },
        { 1, Colors.DarkRed },
        { 0, Colors.Black },


    };

    public int AnimationCount
	{
		get { return animationList.Length; }
	}

	public override void _Ready()
	{
		player = GetNode<AnimationPlayer>("AdamastorAnimation");
		animationList = player.GetAnimationList();
        player.AnimationFinished += Player_AnimationFinished;

		farleftCollisionArea = GetParent().GetNode<AdamastorWaveCollision>("Wave Far Left Collision");
        leftCollisionArea = GetParent().GetNode<AdamastorWaveCollision>("Wave Left Collision");
        leftCenterCollisionArea = GetParent().GetNode<AdamastorWaveCollision>("Wave Left Center Collision");
        rightCollisionArea = GetParent().GetNode<AdamastorWaveCollision>("Wave Right Collision");
        rightCenterCollisionArea = GetParent().GetNode<AdamastorWaveCollision>("Wave Right Center Collision");

        LeftEye = GetNode<Polygon2D>("LeftEye");
        LeftEye.Color = Colors.Green;
        RightEye = GetNode<Polygon2D>("RightEye");
        RightEye.Color = Colors.Green;
    }

	private Vector2 GetLowestX(CollisionPolygon2D collisionPolygon)
	{
		Vector2 lowestXVector = new Vector2(0,0);

		foreach(Vector2 vector in collisionPolygon.Polygon)
		{
			var transform = collisionPolygon.GetGlobalTransform().BasisXform(vector);

			if(lowestXVector.X == 0 && lowestXVector.Y == 0 || transform.X < lowestXVector.X)
			{
				lowestXVector = transform;
			}
		}

		return lowestXVector;
	}

    private Vector2 GetHighestY(CollisionPolygon2D collisionPolygon)
    {
        Vector2 highestYVector = new Vector2(0, 0);

        foreach (Vector2 vector in collisionPolygon.Polygon)
        {
            var transform = collisionPolygon.GetGlobalTransform().BasisXform(vector);

            if (highestYVector.X == 0 && highestYVector.Y == 0 || transform.Y > highestYVector.Y)
            {
                highestYVector = transform;
            }
        }

        return highestYVector;
    }

    private void Player_AnimationFinished(StringName animName)
    {

        if (animName.ToString().Contains("Left"))
        {
            farleftCollisionArea.registerAreas = true;
            leftCollisionArea.registerAreas = true;
            leftCenterCollisionArea.registerAreas = true;
        }
        else
        {
            rightCollisionArea.registerAreas = true;
            rightCenterCollisionArea.registerAreas = true;
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}

    public void Hit()
    {
        Life--;
        LeftEye.Color = HpColors[Life];
        RightEye.Color = HpColors[Life];

        if(Life <= 0)
        {
            ((Main)GetParent().GetParent()).GameWon();
        }

    }

	public void PlayAnimation(int index)
	{
        farleftCollisionArea.registerAreas = false;
        leftCollisionArea.registerAreas = false;
        leftCenterCollisionArea.registerAreas = false;
        rightCollisionArea.registerAreas = false;
        rightCenterCollisionArea.registerAreas = false;

        player.Play(animationList[index]);
	}
}
