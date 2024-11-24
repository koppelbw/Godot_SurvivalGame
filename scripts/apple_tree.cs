using Godot;
using System;
using System.Threading.Tasks;

public partial class apple_tree : Node2D
{
	string state = "no_apples";
	bool isPlayerInArea = false;
		
	public Timer _growthTimer;
	public AnimatedSprite2D _treeSprite;
    public Marker2D AppleMarker;
	public player Player;

    [Export]
    public InventoryItem AppleItem { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		_growthTimer = GetNode<Timer>("Grow_Timer");
		_treeSprite = GetNode<AnimatedSprite2D>("TreeSprite");
		AppleMarker = GetNode<Marker2D>("Marker2D");

		if(state.Equals("no_apples"))
		{
			_growthTimer.Start();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override async void _Process(double delta)
	{
		if(state.Equals("no_apples"))
		{
			_treeSprite.Play("no_apples");
		}
		else
		{
			_treeSprite.Play("apples");
			
			if(isPlayerInArea && Input.IsActionJustPressed("e"))
			{
				state = "no_apples";
				await DropApple();
			}
		}
	}
	
	private void _on_pickable_area_body_entered(Node2D body)
	{		
        if (body.Name.Equals("player"))
        {
			isPlayerInArea = true;
			Player = (player)body;
		}
	}

	private void _on_pickable_area_body_exited(Node2D body)
	{
        if (body.Name.Equals("player"))
        {
			isPlayerInArea = false;
		}
	}
	
	private void _on_grow_timer_timeout()
	{
		state = "apples";
	}

	private async Task DropApple()
	{		
		var appleInstance = (Node2D) GD.Load<PackedScene>("res://scenes/apple_collectable.tscn").Instantiate();
		appleInstance.GlobalPosition = AppleMarker.GlobalPosition;
		GetParent().AddChild(appleInstance);
		
		Player.CollectItem(AppleItem);

        await Task.Delay(3000);
        _growthTimer.Start();
    }
}
