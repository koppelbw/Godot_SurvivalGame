using Godot;
using System;

public partial class inventory_ui_control : Control
{
    public bool IsOpen { get; set; }
	public Inventory Inventory { get; set; }
	public Godot.Collections.Array<Node> Slots { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{	
        Close();
		
		Inventory = (Inventory)GD.Load<Resource>("res://inventory/PlayerInventory.tres");
		Slots = GetNode<GridContainer>("NinePatchRect/GridContainer").GetChildren();


		Inventory.AddUserSignal("Update");
        UpdateSlots();
	}

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
		if (Input.IsActionJustPressed("i"))
		{
			if(IsOpen) 
			{
                Close();
            }
            else
            {
				UpdateSlots();
                Open();
            }
        }
	}

	private void Open()
	{
		Visible = true;
		IsOpen = true;
	}

	private void Close() 
	{
		Visible = false;
		IsOpen = false;
	}

	private void UpdateSlots() 
	{
		var i = 0;
		foreach(var slot in Slots)
		{
			inventory_ui_slot_panel invSlot = (inventory_ui_slot_panel)slot;
			invSlot.Update(Inventory.SlotContainers[i]);
			
			i++;
		}
	}
}
