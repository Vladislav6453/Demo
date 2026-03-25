using System.Windows;
using System.Windows.Controls;
using Prototip.Models;
using Prototip.VM;

namespace Prototip.Views;

public partial class EmployeesWindow : Window
{
    private readonly EmployeesViewModel _viewModel;

    public EmployeesWindow()
    {
        InitializeComponent();
        _viewModel = new EmployeesViewModel();
        LstEmployees.ItemsSource = _viewModel.Employees;
        DpHireDate.SelectedDate = DateTime.Now;
    }

    private void LstEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (LstEmployees.SelectedItem is Employee selected)
        {
            _viewModel.Select(selected);
            TxtFullName.Text = _viewModel.NewFullName;
            TxtPosition.Text = _viewModel.NewPosition;
            TxtPhone.Text = _viewModel.NewPhone;
            DpHireDate.SelectedDate = _viewModel.NewHireDate;
            TxtLogin.Text = _viewModel.NewLogin;
            TxtPassword.Text = _viewModel.NewPassword;
            
            foreach (ComboBoxItem item in CmbRole.Items)
            {
                if (item.Tag?.ToString() == _viewModel.NewRole)
                {
                    CmbRole.SelectedItem = item;
                    break;
                }
            }
        }
    }

    private void BtnAdd_Click(object sender, RoutedEventArgs e)
    {
        _viewModel.NewFullName = TxtFullName.Text;
        _viewModel.NewPosition = TxtPosition.Text;
        _viewModel.NewPhone = TxtPhone.Text;
        _viewModel.NewHireDate = DpHireDate.SelectedDate ?? DateTime.Now;
        _viewModel.NewLogin = TxtLogin.Text;
        _viewModel.NewPassword = TxtPassword.Text;
        
        if (CmbRole.SelectedItem is ComboBoxItem roleItem)
            _viewModel.NewRole = roleItem.Tag?.ToString() ?? "Cashier";
        
        if (_viewModel.Add())
        {
            ClearForm();
            LstEmployees.ItemsSource = null;
            LstEmployees.ItemsSource = _viewModel.Employees;
        }
    }

    private void BtnUpdate_Click(object sender, RoutedEventArgs e)
    {
        _viewModel.NewFullName = TxtFullName.Text;
        _viewModel.NewPosition = TxtPosition.Text;
        _viewModel.NewPhone = TxtPhone.Text;
        _viewModel.NewHireDate = DpHireDate.SelectedDate ?? DateTime.Now;
        _viewModel.NewLogin = TxtLogin.Text;
        _viewModel.NewPassword = TxtPassword.Text;
        
        if (CmbRole.SelectedItem is ComboBoxItem roleItem)
            _viewModel.NewRole = roleItem.Tag?.ToString() ?? "Cashier";
        
        if (_viewModel.Update())
        {
            ClearForm();
            LstEmployees.ItemsSource = null;
            LstEmployees.ItemsSource = _viewModel.Employees;
        }
    }

    private void BtnDelete_Click(object sender, RoutedEventArgs e)
    {
        if (_viewModel.Delete())
        {
            ClearForm();
            LstEmployees.ItemsSource = null;
            LstEmployees.ItemsSource = _viewModel.Employees;
        }
    }

    private void ClearForm()
    {
        TxtFullName.Clear();
        TxtPosition.Clear();
        TxtPhone.Clear();
        DpHireDate.SelectedDate = DateTime.Now;
        TxtLogin.Clear();
        TxtPassword.Clear();
        CmbRole.SelectedIndex = 1; // Cashier
        LstEmployees.SelectedItem = null;
    }
}