using Godot;
using System;

public partial class player : CharacterBody2D
{
	[Export]
	Inventory Inventory { get; set; }

	public const float Speed = 100.0f;
	public string playerState = "idle";
	public const float JumpVelocity = -400.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	public AnimatedSprite2D _playerSprite;
	
	public override void _Ready()
	{
		_playerSprite = GetNode<AnimatedSprite2D>("PlayerSprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		
		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("left", "right", "up", "down");
		if (direction != Vector2.Zero)
		{
			playerState = "walking";
			velocity.X = direction.X * Speed;
			velocity.Y = direction.Y * Speed;
		}
		else
		{
			playerState = "idle";
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
		PlayAnimation(direction);
	}
	
	public void PlayAnimation(Vector2 direction)
	{
		switch(playerState)
		{
			case "idle":
				_playerSprite.Play("idle");
				break;
			case "walking":
				WalkingAnimationHandler(direction);
				break;
		}	
	}
	
	private void WalkingAnimationHandler(Vector2 direction)
	{
		// Handles Y-axis movement
		if(direction.Y != 0)
		{
			if(direction.Y < 0)
			{
				if(direction.X == 0)
				{
					_playerSprite.Play("n-walk");
				}
				else
				{
					if(direction.X < 0)
					{
						_playerSprite.Play("nw-walk");
					}
					else
					{
						_playerSprite.Play("ne-walk");
					}
				}			
			}
			else
			{
				if(direction.X == 0)
				{
					_playerSprite.Play("s-walk");
				}
				else
				{
					if(direction.X < 0)
					{
						_playerSprite.Play("sw-walk");
					}
					else
					{
						_playerSprite.Play("se-walk");
					}
				}
			}
		}
		
		// Handles X-axis movement
		if(direction.X == -1)
		{
			_playerSprite.Play("w-walk");
		}
		else if(direction.X == 1)
		{
			_playerSprite.Play("e-walk");
		}
	}

	public void CollectItem(InventoryItem item)
	{
		Inventory.Insert(item);
	}
}
