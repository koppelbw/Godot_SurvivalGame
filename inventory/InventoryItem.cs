using Godot;
using System;

[GlobalClass]
public partial class InventoryItem : Resource
{
    [Export]
    public string Name { get; set; } = string.Empty;

    [Export]
    public Texture2D Texture { get; set; }
}
