﻿using Audio;
using System;
using UnityEngine;

public class XUiC_WheelRequiredItemStack : XUiC_RequiredItemStack
{

    protected bool IsOver = false;

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

    public override void OnHovered(bool _isOver)
    {
        IsOver = _isOver;
        base.OnHovered(_isOver);
    }

    public override void OnScrolled(float _delta)
    {
        // Process the item transfer
        if (IsLocked || StackLock)
        {
            base.OnScrolled(_delta);
        }
        // Do nothing if the cursor is currently locked
        // Happens when cursors stay over toolbar when backpack is closed
        else if (Cursor.lockState != CursorLockMode.None)
        {
            base.OnScrolled(_delta);
        }
        else if (_delta > 0.0 && !TakeOnly && CanSwap(DnDStack))
        {
            TransferItems(
                (int)(_delta * 10),
                DnDStack, SetDnD,
                itemStack, SetSlot);
        }
        else if (_delta < 0.0 && (DnDStack.IsEmpty() || CanSwap(DnDStack)))
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
