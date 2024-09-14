using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // there is no kitchen object here
            if (player.HasKitchenObject()) {
                // the player is carrying something, drop the object here
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else {
                // player has nothing, do nothing
            }
        } else {
            // there is a kitchen object here
            if (player.HasKitchenObject()) {
                // player is carrying something

            } else {
                // player has nothing, do something
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

}
