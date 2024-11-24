using Godot;
using System;

[GlobalClass]
public partial class InventorySlotContainer : Resource
{
    [Export]
    public InventoryItem Item { get; set; }

    [Export]
    public int Amount { get; set; }
}
