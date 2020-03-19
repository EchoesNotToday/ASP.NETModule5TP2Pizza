using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP_Pizza.Models.ViewModels
{
    public class PizzaVM
    {
        public Pizza Pizza { get; set; }

        public List<Ingredient> ingredients = new List<Ingredient>();

        public List<Pate> pates = new List<Pate>();

        public int SelectedPate { get; set; }

        public List<int> SelectedIngrdients { get; set; }

        public void setIngredients(List<Ingredient> ingredients)
        {
            this.ingredients = ingredients;
        }

        public void setPates(List<Pate> pates)
        {
            this.pates = pates;
        }
    }
}