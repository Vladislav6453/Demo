using System.Windows;
using Prototip.Models;
using Prototip.VM;

namespace Prototip.Views;

public partial class SuppliersWindow : Window
{
    private readonly SuppliersViewModel _viewModel;

    public SuppliersWindow()
    {
        InitializeComponent();
        _viewModel = new SuppliersViewModel();
        LstSuppliers.ItemsSource = _viewModel.Suppliers;
    }

    private void LstSuppliers_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (LstSuppliers.SelectedItem is Supplier selected)
        {
            _viewModel.Select(selected);
            TxtName.Text = _viewModel.NewName;
            TxtContact.Text = _viewModel.NewContactPerson;
            TxtPhone.Text = _viewModel.NewPhone;
            TxtAddress.Text = _viewModel.NewAddress;
            TxtInn.Text = _viewModel.NewInn;
        }
    }

    private void BtnAdd_Click(object sender, RoutedEventArgs e)
    {
        _viewModel.NewName = TxtName.Text;
        _viewModel.NewContactPerson = TxtContact.Text;
        _viewModel.NewPhone = TxtPhone.Text;
        _viewModel.NewAddress = TxtAddress.Text;
        _viewModel.NewInn = TxtInn.Text;
        
        if (_viewModel.Add())
        {
            ClearForm();
            LstSuppliers.ItemsSource = null;
            LstSuppliers.ItemsSource = _viewModel.Suppliers;
        }
    }

    private void BtnUpdate_Click(object sender, RoutedEventArgs e)
    {
        _viewModel.NewName = TxtName.Text;
        _viewModel.NewContactPerson = TxtContact.Text;
        _viewModel.NewPhone = TxtPhone.Text;
        _viewModel.NewAddress = TxtAddress.Text;
        _viewModel.NewInn = TxtInn.Text;
        
        if (_viewModel.Update())
        {
            ClearForm();
            LstSuppliers.ItemsSource = null;
            LstSuppliers.ItemsSource = _viewModel.Suppliers;
        }
    }

    private void BtnDelete_Click(object sender, RoutedEventArgs e)
    {
        if (_viewModel.Delete())
        {
            ClearForm();
            LstSuppliers.ItemsSource = null;
            LstSuppliers.ItemsSource = _viewModel.Suppliers;
        }
    }

    private void ClearForm()
    {
        TxtName.Clear();
        TxtContact.Clear();
        TxtPhone.Clear();
        TxtAddress.Clear();
        TxtInn.Clear();
    }
}