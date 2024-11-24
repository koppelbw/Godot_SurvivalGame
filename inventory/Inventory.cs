using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Inventory : Resource
{
    [Export]
    public Godot.Collections.Array<InventorySlotContainer> SlotContainers { get; set; }

    public Signal Update = new();

    public void Insert(InventoryItem item)
    {
        var itemSlots = SlotContainers.Where(s => s.Item == item);

        if (itemSlots.Any())
        {
            itemSlots.FirstOrDefault().Amount += 1;
        }
        else
        {
            var emptySlots = SlotContainers.Where(s => s.Item is null);

            if (emptySlots.Any())
            {
                emptySlots.FirstOrDefault().Item = item;
                emptySlots.FirstOrDefault().Amount = 1;
            }
        }

        EmitSignal("Update");
    }
}
