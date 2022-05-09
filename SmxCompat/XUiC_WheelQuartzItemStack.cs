
using Audio;
using System;
using UnityEngine;

public class XUiC_WheelQuartzItemStack : Quartz.ItemStack
{

    protected bool IsOver = false;

    protected UIScrollView ScrollView = null;

    public override void Init()
    {
        base.Init();
        ScrollView = WheelItemStack
            .GetParentScrollView(this);
    }

    private ItemStack DnDStack => xui.dragAndDrop.CurrentStack;

    private void SetSlot(ItemStack stack) => ItemStack = stack;

    private void SetDnD(ItemStack stack) => xui.dragAndDrop.CurrentStack = stack;

    protected virtual int TransferItems(int amount,
        ItemStack src, Action<ItemStack> setSrc,
        ItemStack dst, Action<ItemStack> setDst)
    {
        if (WheelItemStack.TransferItems(amount, src, setSrc, dst, setDst) > 0)
        {
            // Copied from vanilla
            ForceRefreshItemStack();
            IsDirty = true;
            Selected = false;
            HandleClickComplete();
            xui.calloutWindow.UpdateCalloutsForItemStack(
                ViewComponent.UiTransform.gameObject,
                ItemStack, IsOver);
            if (placeSound != null) Manager.
                PlayXUiSound(placeSound, 0.1f);
        }
        // Return how much was transfered
        return amount;
    }

    protected override void OnHovered(bool _isOver)
    {
        IsOver = _isOver;
        base.OnHovered(_isOver);
    }

    protected override void OnScrolled(float _delta)
    {
        // Check for edge case where we are actually inside another
        // Scrollable View (enforce shift key in that situation)
        if (ScrollView != null && !Input.GetKey(KeyCode.LeftShift))
        {
            // Otherwise "bubble" event up
            ScrollView.Scroll(_delta);
            return;
        }
        // Process the item transfer
        if (IsLocked || StackLock)
        {
            base.OnScrolled(_delta);
        }
        else if (_delta > 0.0)
        {
            TransferItems(
                (int)(_delta * 10),
                DnDStack, SetDnD,
                itemStack, SetSlot);
        }
        else if (_delta < 0.0)
        {
            TransferItems(
                (int)(_delta * -10),
                itemStack, SetSlot,
                DnDStack, SetDnD);
        }
        else
        {
            base.OnScrolled(_delta);
        }
    }

}
