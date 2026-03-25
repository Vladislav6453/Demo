using System.Collections.ObjectModel;
using System.Windows;
using Prototip.Models;
using Prototip.Services;

namespace Prototip.VM;

public class CategoriesViewModel : BaseViewModel
{
    private readonly DataService _data = DataService.Instance;
    
    public ObservableCollection<Category> Categories { get; set; } = new();
    public Category? SelectedCategory { get; set; }
    public string NewName { get; set; } = "";
    public string NewDescription { get; set; } = "";

    public CategoriesViewModel()
    {
        LoadData();
    }

    public void LoadData()
    {
        Categories.Clear();
        foreach (var cat in _data.GetCategories())
            Categories.Add(cat);
    }

    public bool Add()
    {
        if (string.IsNullOrWhiteSpace(NewName))
        {
            MessageBox.Show("Введите название категории!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        
        _data.AddCategory(new Category { Name = NewName, Description = NewDescription });
        LoadData();
        Clear();
        return true;
    }

    public bool Update()
    {
        if (SelectedCategory == null)
        {
            MessageBox.Show("Выберите категорию!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        
        if (string.IsNullOrWhiteSpace(NewName))
        {
            MessageBox.Show("Введите название категории!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        
        SelectedCategory.Name = NewName;
        SelectedCategory.Description = NewDescription;
        _data.UpdateCategory(SelectedCategory);
        LoadData();
        Clear();
        return true;
    }

    public bool Delete()
    {
        if (SelectedCategory == null)
        {
            MessageBox.Show("Выберите категорию!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        
        if (MessageBox.Show($"Удалить категорию '{SelectedCategory.Name}'?", "Подтверждение", 
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        {
            _data.DeleteCategory(SelectedCategory.Id);
            LoadData();
            Clear();
            return true;
        }
        
        return false;
    }

    public void Select(Category category)
    {
        SelectedCategory = category;
        NewName = category?.Name ?? "";
        NewDescription = category?.Description ?? "";
    }

    public void Clear()
    {
        NewName = "";
        NewDescription = "";
        SelectedCategory = null;
    }
}