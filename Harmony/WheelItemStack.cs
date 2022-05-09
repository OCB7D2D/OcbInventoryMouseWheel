using System;

public static class WheelItemStack
{

    public static UIScrollView GetParentScrollView(XUiController ctr)
    {
        var parent = ctr.Parent;
        while (parent != null)
        {
            var vp = parent.ViewComponent;
            if (vp != null && vp.EventOnScroll)
            {
                var ui = vp.UiTransform?.GetComponent<UIScrollView>();
                if (ui != null) return ui;
            }
            parent = parent.Parent;
        }
        return null;
    }

    public static int TransferItems(int amount,
        ItemStack src, Action<ItemStack> setSrc,
        ItemStack dst, Action<ItemStack> setDst)
    {
        // Return if nothing to get
        if (src.IsEmpty()) return 0;
        // Only move as many as are available
        amount = MathUtils.Min(src.count, amount);
        // Put into empty slot?
        if (dst.IsEmpty())
        {
            // Decrement the requested amount to maximum free in chosen destination slot
            amount = MathUtils.Min(src.itemValue.ItemClass.Stacknumber.Value, amount);
            // Move the sanitized amount around
            dst = src.Clone();
            dst.count = amount; // New count
            src.count -= amount; // Decrease
            // Reset source if empty by now
            if (src.IsEmpty()) src.Clear();
        }
        // Putting into a compatible slot?
        else if (src.itemValue.type == dst.itemValue.type)
        {
            // Decrement requested amount to maximum free in chosen destination slot
            amount = MathUtils.Min(dst.itemValue.ItemClass.Stacknumber.Value - dst.count, amount);
            // Move the sanitized amount around
            dst = dst.Clone();
            dst.count += amount; // increment
            src.count -= amount; // decrement
            // Reset source if empty by now
            if (src.IsEmpty()) src.Clear();
        }
        else
        {
            amount = 0;
        }
        // Call updates
        if (amount > 0)
        {
            setSrc(src);
            setDst(dst);
        }
        // Return how much was transfered
        return amount;
    }


}
