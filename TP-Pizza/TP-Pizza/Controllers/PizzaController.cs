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
                Pizza pizza = pVM.Pizza;
                pizza.Pate = toutesLesPates.FirstOrDefault(p => p.Id == pVM.SelectedPate);
                foreach (int ingredientId in pVM.SelectedIngrdients)
                {
                    pizza.Ingredients.Add(tousLesIngredients.FirstOrDefault(i => i.Id == ingredientId));
                }
                pizza.Id = toutesLesPizzas.Count() + 1;
                toutesLesPizzas.Add(pizza);
                return RedirectToAction("Index");
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
    }
}
