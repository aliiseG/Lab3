using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.DataAccess;
using CommunityToolkit.Mvvm.Input;
using System.Runtime.CompilerServices;
using System.Windows.Input;

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

        public void FilterData()
        {

            if (string.IsNullOrWhiteSpace(SearchedRecipe))
            {
                _recipes = _db.Recipes.ToArray();
            }
            else
            {
                _recipes = _db.Recipes.Where(r => r.Category.Contains(SearchedRecipe)).ToArray();
                OnPropertyChanged(nameof(Recipes));
            }
        }

        public void Load()
        {
            _recipes = _db.Recipes.ToArray();
        }
        
    }
}
