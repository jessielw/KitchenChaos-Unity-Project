using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter {

    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs: EventArgs {
        public float progressNormalized;
    }

    public event EventHandler OnCut;



    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // there is no kitchen object here
            if (player.HasKitchenObject()) {
                // the player is carrying something, drop the object here
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    // player carrying something that can be Cut
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    
                    cuttingProgress = 0;
                    CuttingRecipeSO cuttingRecipeSo = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSo.cuttingProgressMax
                    });
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
            cuttingProgress++;

            OnCut?.Invoke(this, EventArgs.Empty);
            CuttingRecipeSO cuttingRecipeSo = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSo.cuttingProgressMax
            });

            if (cuttingProgress >= cuttingRecipeSo.cuttingProgressMax) {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        CuttingRecipeSO cuttingRecipeSo = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSo != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        CuttingRecipeSO cuttingRecipeSo = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSo != null) {
            return cuttingRecipeSo.output;
        } else {
            return null;
        }
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == inputKitchenObjectSO) {
                return cuttingRecipeSO;
            }
        }
        return null;
    }

}
