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
using Microsoft.IdentityModel.Tokens;

namespace Lab3.WpfApplication.ViewModels
{
    public class MainWindowViewModel : ObservableObject, INotifyPropertyChanged
    {
        private RecipeDbContext _db;

        /// Implemented commands
        public ICommand SelectRecipeCommand { get; }

        public ICommand SearchCommand { get; }

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
        public Recipe SelectedRecipe {get; set; }

        public void LoadIngredients()
        {
            if (SelectedRecipe == null) 
            {
                return;
            }
            Ingredients = _db.Ingredients.Where(m => m.Recipe.Id == SelectedRecipe.Id).ToList();
        }


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

        private ObservableCollection<string> _existingCategories = new ObservableCollection<string>();

        public ObservableCollection<string> ExistingCategories
        {
            get { return _existingCategories; }
            set
            {
                _existingCategories = value;

            }
        }



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

        public void Load()
        {
            _recipes = _db.Recipes.ToArray();
        }
        
    }
}
