using System.Windows;
using Prototip.Models;
using Prototip.VM;

namespace Prototip.Views;

public partial class CategoriesWindow : Window
{
    private readonly CategoriesViewModel _viewModel;
    public CategoriesWindow()
    {
        InitializeComponent();
        _viewModel = new CategoriesViewModel();
        LstCategories.ItemsSource = _viewModel.Categories;
    }
    private void LstCategories_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (LstCategories.SelectedItem is Category selected)
        {
            _viewModel.Select(selected);
            TxtName.Text = _viewModel.NewName;
            TxtDescription.Text = _viewModel.NewDescription;
        }
    }

    private void BtnAdd_Click(object sender, RoutedEventArgs e)
    {
        _viewModel.NewName = TxtName.Text;
        _viewModel.NewDescription = TxtDescription.Text;
        
        if (_viewModel.Add())
        {
            TxtName.Clear();
            TxtDescription.Clear();
            LstCategories.ItemsSource = null;
            LstCategories.ItemsSource = _viewModel.Categories;
        }
    }

    private void BtnUpdate_Click(object sender, RoutedEventArgs e)
    {
        _viewModel.NewName = TxtName.Text;
        _viewModel.NewDescription = TxtDescription.Text;
        
        if (_viewModel.Update())
        {
            TxtName.Clear();
            TxtDescription.Clear();
            LstCategories.ItemsSource = null;
            LstCategories.ItemsSource = _viewModel.Categories;
        }
    }

    private void BtnDelete_Click(object sender, RoutedEventArgs e)
    {
        if (_viewModel.Delete())
        {
            TxtName.Clear();
            TxtDescription.Clear();
            LstCategories.ItemsSource = null;
            LstCategories.ItemsSource = _viewModel.Categories;
        }
    }
}