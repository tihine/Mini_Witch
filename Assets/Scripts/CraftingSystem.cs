using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    [SerializeField] private RecipeData[] availableRecipes; //faire une liste pour changer en dynamique
    [SerializeField] private GameObject recipeUiPrefab;
    [SerializeField] private Transform recipesParent;

    void Update()
    {
        UpdateDisplayedRecipes();
    }
    private void UpdateDisplayedRecipes()
    {
        foreach(Transform child in recipesParent)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < availableRecipes.Length; i++)
        {
            GameObject recipe = Instantiate(recipeUiPrefab, recipesParent);
            recipe.GetComponent<Recipe>().Configure(availableRecipes[i]);
        }
    }
}
