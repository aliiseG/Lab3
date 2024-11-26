using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public ICommand SelectRecipeCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public MainWindowViewModel()
        {
            _db = new RecipeDbContext();
            SelectRecipeCommand = new RelayCommand(LoadIngredients);
        }

        private Recipe[] _recipes;
        public Recipe[] Recipes => _recipes;

        public void Load()
        {
            _recipes = _db.Recipes.ToArray();
        }
        

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
    }
}
