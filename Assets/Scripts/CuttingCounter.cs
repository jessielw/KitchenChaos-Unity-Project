using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter {

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // there is no kitchen object here
            if (player.HasKitchenObject()) {
                // the player is carrying something, drop the object here
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    // player carrying something that can be Cut
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
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

    public override void InteractAlternate(Player player) {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())) {
            // there is a kitchen object here and it can be cut, let's cut it
            KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO()); 
            
            GetKitchenObject().DestroySelf();
            
            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == inputKitchenObjectSO) {
                return true;
            }
        }
        return false;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == inputKitchenObjectSO) {
                return cuttingRecipeSO.output;
            }
        }
        return null;
    }

}
