using Godot;
using System;

public partial class inventory_ui_slot_panel : Panel
{
	Sprite2D ItemVisual;
    public Label AmountText { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		ItemVisual = GetNode<Sprite2D>("CenterContainer/Panel/ItemDisplay_Sprite2D");
		AmountText = GetNode<Label>("CenterContainer/Panel/Label");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Update(InventorySlotContainer container)
	{
		if(container.Item is null) 
		{
			ItemVisual.Visible = false;
			AmountText.Visible = false;
		}
		else
		{
			ItemVisual.Visible = true;
			ItemVisual.Texture = container.Item.Texture;

			if(container.Amount > 1)
			{
				AmountText.Visible = true;
			}
			AmountText.Text = container.Amount.ToString();
		}
	}
}
