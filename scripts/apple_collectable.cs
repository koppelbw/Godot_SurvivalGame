using Godot;
using System;
using System.Threading.Tasks;

public partial class apple_collectable : StaticBody2D
{
	public AnimationPlayer appleAnimationPlayer;

	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		appleAnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

		await FallFromTree();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private async Task FallFromTree()
	{
		appleAnimationPlayer.Play("FallingFromTree");
		await Task.Delay(1500);

		appleAnimationPlayer.Play("fade");
		GD.Print("+1 Apples");
        await Task.Delay(300);

		QueueFree();
	}
}
