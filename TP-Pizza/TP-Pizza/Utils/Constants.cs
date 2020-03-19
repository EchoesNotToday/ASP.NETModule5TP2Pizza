using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP_Pizza.Utils
{
    public class Constants
    {
        // View names
        public const string _PIZZA_DETAILS = "_PizzaDetails";
        public const string VIEW_INDEX = "Index";
        public const string VIEW_EDIT = "Edit";
        public const string VIEW_DETAILS = "Details";
        public const string VIEW_DELETE = "Delete";
        public const string VIEW_CREATE = "Create";

        //Error messages
        public const string ERROR_ALREADY_EXISTS_NAME = "Une pizza portant ce nom existe déjà.";
        public const string ERROR_INGREDIENTS_COUNT = "Une pizza doit avoir au minimum 2 ingrédients et au maximum 5 ingrédients.";
        public const string ERROR_ALREADY_EXISTS_INGREDIENTS = "Une pizza avec ces ingrédients existe déjà.";
        public const string ERROR_PATE_UNKNOWN = "La pate sélectionnée n'existe pas.";
    }
}