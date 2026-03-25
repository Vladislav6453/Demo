using System.Collections.ObjectModel;
using System.Windows;
using Prototip.Models;
using Prototip.Services;

namespace Prototip.VM;

public class EmployeesViewModel : BaseViewModel
{
    private readonly DataService _data = DataService.Instance;
    
    public ObservableCollection<Employee> Employees { get; set; } = new();
    public Employee? SelectedEmployee { get; set; }
    
    public string NewFullName { get; set; } = "";
    public string NewPosition { get; set; } = "";
    public string NewPhone { get; set; } = "";
    public DateTime NewHireDate { get; set; } = DateTime.Now;
    public string NewLogin { get; set; } = "";
    public string NewPassword { get; set; } = "";
    public string NewRole { get; set; } = "Cashier";

    public EmployeesViewModel()
    {
        LoadData();
    }

    public void LoadData()
    {
        Employees.Clear();
        foreach (var emp in _data.GetEmployees())
            Employees.Add(emp);
    }

    public bool Add()
    {
        if (string.IsNullOrWhiteSpace(NewFullName))
        {
            MessageBox.Show("Введите ФИО сотрудника!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        if (string.IsNullOrWhiteSpace(NewLogin))
        {
            MessageBox.Show("Введите логин!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        
        _data.AddEmployee(new Employee
        {
            FullName = NewFullName,
            Position = NewPosition,
            Phone = NewPhone,
            HireDate = NewHireDate,
            Login = NewLogin,
            Password = NewPassword,
            Role = NewRole
        });
        LoadData();
        Clear();
        return true;
    }

    public bool Update()
    {
        if (SelectedEmployee == null)
        {
            MessageBox.Show("Выберите сотрудника!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        
        SelectedEmployee.FullName = NewFullName;
        SelectedEmployee.Position = NewPosition;
        SelectedEmployee.Phone = NewPhone;
        SelectedEmployee.HireDate = NewHireDate;
        SelectedEmployee.Login = NewLogin;
        SelectedEmployee.Password = NewPassword;
        SelectedEmployee.Role = NewRole;
        
        _data.UpdateEmployee(SelectedEmployee);
        LoadData();
        Clear();
        return true;
    }

    public bool Delete()
    {
        if (SelectedEmployee == null)
        {
            MessageBox.Show("Выберите сотрудника!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }
        
        if (MessageBox.Show($"Удалить сотрудника '{SelectedEmployee.FullName}'?", "Подтверждение", 
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        {
            _data.DeleteEmployee(SelectedEmployee.Id);
            LoadData();
            Clear();
            return true;
        }
        
        return false;
    }

    public void Select(Employee employee)
    {
        SelectedEmployee = employee;
        if (employee != null)
        {
            NewFullName = employee.FullName;
            NewPosition = employee.Position;
            NewPhone = employee.Phone;
            NewHireDate = employee.HireDate;
            NewLogin = employee.Login;
            NewPassword = employee.Password;
            NewRole = employee.Role;
        }
    }

    public void Clear()
    {
        NewFullName = "";
        NewPosition = "";
        NewPhone = "";
        NewHireDate = DateTime.Now;
        NewLogin = "";
        NewPassword = "";
        NewRole = "Cashier";
        SelectedEmployee = null;
    }
}