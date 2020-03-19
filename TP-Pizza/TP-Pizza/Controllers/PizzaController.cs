using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP_Pizza.Models.ViewModels;
using TP_Pizza.Utils;

namespace TP_Pizza.Controllers
{
    public class PizzaController : Controller
    {
        private static List<Ingredient> tousLesIngredients = Pizza.IngredientsDisponibles;
        private static List<Pate> toutesLesPates = Pizza.PatesDisponibles;
        private static List<Pizza> toutesLesPizzas = new List<Pizza>();

        private static Pizza GetPizzaById(int id)
        {
            return toutesLesPizzas.FirstOrDefault(p => p.Id == id);
        }

        // GET: Pizza
        public ActionResult Index()
        {
            return View(toutesLesPizzas);
        }

        // GET: Pizza/Details/5
        public ActionResult Details(int id)
        {
            Pizza pizza = GetPizzaById(id);
            if(pizza != null)
            {
                return View(pizza);
            }
            return RedirectToAction(Constants.VIEW_INDEX);
        }

        // GET: Pizza/Create
        public ActionResult Create()
        {
            PizzaVM pVM = new PizzaVM();
            pVM.setIngredients(tousLesIngredients);
            pVM.setPates(toutesLesPates);
            return View(pVM);
        }

        // POST: Pizza/Create
        [HttpPost]
        public ActionResult Create(PizzaVM pVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (IsPizzaValid(pVM))
                    {
                        Pizza pizza = pVM.Pizza;
                        pizza.Pate = toutesLesPates.FirstOrDefault(p => p.Id == pVM.SelectedPate);
                        foreach (int ingredientId in pVM.SelectedIngrdients)
                        {
                            pizza.Ingredients.Add(tousLesIngredients.FirstOrDefault(i => i.Id == ingredientId));
                        }
                        pizza.Ingredients = pizza.Ingredients.OrderBy(x => x.Id).ToList();
                        pizza.Id = toutesLesPizzas.Count() + 1;
                        toutesLesPizzas.Add(pizza);
                        return RedirectToAction(Constants.VIEW_INDEX);
                    }
                }
                pVM.ingredients = tousLesIngredients;
                pVM.pates = toutesLesPates;
                return View(pVM);

            }
            catch
            {
                return View();
            }
        }

        // GET: Pizza/Edit/5
        public ActionResult Edit(int id)
        {
            PizzaVM pVM = new PizzaVM();
            pVM.setIngredients(tousLesIngredients);
            pVM.setPates(toutesLesPates);
            pVM.Pizza = toutesLesPizzas.FirstOrDefault(p => p.Id == id);
            pVM.SelectedIngrdients = pVM.Pizza.Ingredients.Select(i => i.Id).ToList();
            return View(pVM);
        }

        // POST: Pizza/Edit/5
        [HttpPost]
        public ActionResult Edit(PizzaVM pizzaVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (IsPizzaValid(pizzaVM))
                    {
                        Pizza pizza = toutesLesPizzas.FirstOrDefault(p => p.Id == pizzaVM.Pizza.Id);
                        pizza.Nom = pizzaVM.Pizza.Nom;
                        pizza.Pate = toutesLesPates.FirstOrDefault(p => p.Id == pizzaVM.SelectedPate);
                        pizza.Ingredients.Clear();
                        foreach (int ingredientId in pizzaVM.SelectedIngrdients)
                        {
                            Ingredient ingredient = tousLesIngredients.FirstOrDefault(i => i.Id == ingredientId);
                            if (ingredient != null)
                            {
                                pizza.Ingredients.Add(ingredient);
                            }
                        }
                        pizza.Ingredients = pizza.Ingredients.OrderBy(x => x.Id).ToList();
                        return RedirectToAction(Constants.VIEW_INDEX);
                    }

                }
                pizzaVM.ingredients = tousLesIngredients;
                pizzaVM.pates = toutesLesPates;
                return View(pizzaVM);
                
            }
            catch
            {
                return View();
            }
        }

        // GET: Pizza/Delete/5
        public ActionResult Delete(int id)
        {
            Pizza p = GetPizzaById(id);
            if(p != null)
            {
                return View(p);
            }
            return RedirectToAction(Constants.VIEW_INDEX);
        }

        // POST: Pizza/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Pizza pizza = GetPizzaById(id);
                toutesLesPizzas.Remove(pizza);
                return RedirectToAction(Constants.VIEW_INDEX);
            }
            catch
            {
                return View();
            }
        }

        private bool IsPizzaValid(PizzaVM pizzaVM)
        {
            bool isValid = true;

            // Name already exists 
            if (toutesLesPizzas.Any(p => p.Nom.ToUpper() == pizzaVM.Pizza.Nom.ToUpper()))
            {
                isValid = false;
                ModelState.AddModelError("", Constants.ERROR_ALREADY_EXISTS_NAME);
            }
            
            // Too much or not enough ingredients
            if (pizzaVM.SelectedIngrdients.Count() < 2 || pizzaVM.SelectedIngrdients.Count() > 5 )
            {
                isValid = false;
                ModelState.AddModelError("", Constants.ERROR_INGREDIENTS_COUNT);
            }

            // Same ingredients 
            foreach (Pizza pizza in toutesLesPizzas)
            {
                if (pizza.Ingredients.Select(i => i.Id).SequenceEqual(pizzaVM.SelectedIngrdients))
                {
                    isValid = false;
                    ModelState.AddModelError("", Constants.ERROR_ALREADY_EXISTS_INGREDIENTS);
                }
            }

            // No pate or pate does not exists
            if (!toutesLesPates.Any(p => p.Id == pizzaVM.SelectedPate))
            {
                isValid = false;
                ModelState.AddModelError("", Constants.ERROR_PATE_UNKNOWN);
            }
            return isValid;
        }
    }
}
