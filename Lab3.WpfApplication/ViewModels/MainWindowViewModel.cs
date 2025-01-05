using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.DataAccess;
using CommunityToolkit.Mvvm.Input;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Lab3.WpfApplication.ViewModels
{
    public class MainWindowViewModel : ObservableObject, INotifyPropertyChanged
    {
        private RecipeDbContext _db;

        /// Implemented commands
        public ICommand SelectRecipeCommand { get; }

        public ICommand SearchCommand { get; }
        public ICommand DeleteCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public MainWindowViewModel()
        {
            _db = new RecipeDbContext();
            SelectRecipeCommand = new RelayCommand(LoadIngredients);
            SearchCommand = new RelayCommand(FilterData);
            DeleteCommand = new RelayCommand(DeleteRecipe);
            ExistingCategories = new ObservableCollection<string>();
            LoadExistingCategories();
        }

        /// To display recipes in first datagrid
        private Recipe[] _recipes;
        public Recipe[] Recipes => _recipes;

        
        
        /// To display the ingredients for selected recipe in grid 
        private List<Ingredient> _ingredients = new List<Ingredient>();

        public List<Ingredient> Ingredients
        {
            get { return _ingredients; }
            set
            {
                _ingredients = value;
                OnPropertyChanged();
            }

        }

        // selected recipe from main window 
        public Recipe SelectedRecipe {get; set; }


        // load ingredients of selected recipe
        public void LoadIngredients()
        {
            if (SelectedRecipe == null) 
            {
                return;
            }
            Ingredients = _db.Ingredients.Where(m => m.Recipe.Id == SelectedRecipe.Id).ToList();
        }

        // get input from mainwindow textbox
        private string _searchedRecipe;
        public string SearchedRecipe
        {
            get { return _searchedRecipe; }

            set
            {
                _searchedRecipe = value;
                OnPropertyChanged();
            }
        }

        // get all instances of distinct category values from db
        private ObservableCollection<string> _existingCategories = new ObservableCollection<string>();

        public ObservableCollection<string> ExistingCategories
        {
            get { return _existingCategories; }
            set
            {
                _existingCategories = value;

            }
        }


        // add all categories to combobox dropdown selection + empty value
        private void LoadExistingCategories()
        {
            var allCategories = _db.Recipes.Select(r => r.Category).Distinct().ToList();
            ExistingCategories.Clear();
            foreach (var val in allCategories)
            {
                ExistingCategories.Add(val);
            }
            ExistingCategories.Add("-");
        }

        // get selected value from dropdown menu
        private string _selectedCategory;
        public string SelectedCategory
        {
            get { return _selectedCategory; }

            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
            }
        }

        // function for filtering results, "Search" button in main window 
        public void FilterData()
        {

            if (string.IsNullOrWhiteSpace(SearchedRecipe) && string.IsNullOrWhiteSpace(SelectedCategory))
            {
                _recipes = _db.Recipes.ToArray();
            }
            else if (!string.IsNullOrWhiteSpace(SearchedRecipe) && (string.IsNullOrWhiteSpace(SelectedCategory) || SelectedCategory=="-"))
            {
                _recipes = _db.Recipes.Where(r => r.DishName.Contains(SearchedRecipe)).ToArray();
                OnPropertyChanged(nameof(Recipes));
            }
            else if (string.IsNullOrWhiteSpace(SearchedRecipe) && !string.IsNullOrWhiteSpace(SelectedCategory))
            {
                _recipes = _db.Recipes.Where(r => r.Category == SelectedCategory).ToArray();
                OnPropertyChanged(nameof(Recipes));
            }
            else
            {
                _recipes = _db.Recipes.Where(r => r.DishName.Contains(SearchedRecipe) && r.Category == SelectedCategory).ToArray();
                OnPropertyChanged(nameof(Recipes));
            }
        }

        // function for deleting recipe and ingredients from db, "Delete" button
        public void DeleteRecipe(){
            if (SelectedRecipe == null)
            {
                return;
            }

            var deleteIngredients = _db.Ingredients.Where(m => m.Recipe.Id == SelectedRecipe.Id).ToList();
            foreach (var ingr in deleteIngredients)
            {
                _db.Ingredients.Remove(ingr);
            }
            _db.Recipes.Remove(SelectedRecipe);
            _db.SaveChanges();

            _recipes = _db.Recipes.ToArray();
            OnPropertyChanged(nameof(Recipes));
            OnPropertyChanged(nameof(Ingredients));
        }

        // initial load all recipes in database
        public void Load()
        {
            _recipes = _db.Recipes.ToArray();
        }
        
    }
}
