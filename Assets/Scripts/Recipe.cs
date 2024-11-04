using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recipe : MonoBehaviour
{
    private RecipeData currentRecipe;
    [SerializeField] private Image craftableItemImage;
    [SerializeField] private GameObject ingredientPrefab;
    [SerializeField] private Transform ingredientsParent;
    [SerializeField] private Button craftButton;
    [SerializeField] private Sprite canBuildIcon;
    [SerializeField] private Sprite cannotBuildIcon;

    public void Configure(RecipeData recipe)
    {
        currentRecipe = recipe;
        craftableItemImage.sprite = recipe.craftableItem.visual;
        bool canCraft = true;

        for (int i = 0; i < recipe.ingredients.Length; i++)
        {
            ItemData ingredient = recipe.ingredients[i];
            if (!Inventory.instance.HasItem(ingredient))
            {
                canCraft = false;
            }
            GameObject ingredientGO = Instantiate(ingredientPrefab, ingredientsParent);
            ingredientGO.transform.GetChild(0).GetComponent<Image>().sprite = recipe.ingredients[i].visual;
        }
        //Management of button visual
        craftButton.image.sprite = canCraft ? canBuildIcon : cannotBuildIcon;
        craftButton.enabled = canCraft;

        ResizeIngredientsParent();
    }
    private void ResizeIngredientsParent()
    {
        Canvas.ForceUpdateCanvases();
        ingredientsParent.GetComponent<ContentSizeFitter>().enabled = false;
        ingredientsParent.GetComponent<ContentSizeFitter>().enabled = true;
    }
    public void CraftItem()
    {
        Inventory.instance.AddItem(currentRecipe.craftableItem);
    }
}
