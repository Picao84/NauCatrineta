using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
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
        farrightCollisionArea = GetParent().GetNode<AdamastorWaveCollision>("Wave Far Right Collision");
        rightCollisionArea = GetParent().GetNode<AdamastorWaveCollision>("Wave Right Collision");
        rightCenterCollisionArea = GetParent().GetNode<AdamastorWaveCollision>("Wave Right Center Collision");
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
		farleftCollisionArea.registerAreas = true;
        leftCollisionArea.registerAreas = true;
        leftCenterCollisionArea.registerAreas = true;
        farrightCollisionArea.registerAreas = true;
        rightCollisionArea.registerAreas = true;
        rightCenterCollisionArea.registerAreas = true;

        //      if (animName.ToString().Contains("Left"))
        //{
        //          var transformedpoositionY = lowerleft.GetGlobalTransform().BasisXform(lowerleft.Position).Y;
        //          ((Main)GetParent().GetParent()).CreatWaveAt(new Vector2(((Bone2D)lowerleft.GetParent()).GlobalPosition.X + Math.Abs(GetLowestX(lowerleft).X), ((Bone2D)lowerleft.GetParent()).GlobalPosition.Y + Math.Abs(GetHighestY(lowerleft).Y + transformedpoositionY)));
        //}
        //else
        //{
        //	var bone = ((Bone2D)lowerright.GetParent()).GlobalPosition.Y;
        //          var transformedpoositionX = lowerright.GetGlobalTransform().BasisXform(lowerright.Position).X;
        //          var transformedpoositionY = lowerright.GetGlobalTransform().BasisXform(lowerright.Position).Y;
        //          ((Main)GetParent().GetParent()).CreatWaveAt(new Vector2(((Bone2D)lowerright.GetParent()).GlobalPosition.X + Math.Abs(GetLowestX(lowerright).X) + transformedpoositionX, bone + Math.Abs(GetHighestY(lowerright).Y + transformedpoositionY)))s;

        //      }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}

	public void PlayAnimation(int index)
	{
		player.Play("Create Wave Far Right");
	}
}
