using checkpoint.Helpers;
using checkpoint.Views.Catalogs.Departments.Presenters;
using checkpoint.Views.Catalogs.Departments.Services;
using checkpoint.Views.Catalogs.Departments.Models;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace checkpoint.Views.Products.Views
{
    /// <summary>
    /// Interaction logic for Departments.xaml
    /// </summary>
    public partial class Departments : UserControl
    {
        #region Properties
        //**************************************************
        //*             PROPERTIES
        //**************************************************
        private DepartmentsPresenter _departmentsPresenter;
        BindingList<Departamento> departamentoList = new BindingList<Departamento>();
        Departamento departamentoUpdate = new Departamento();
        #endregion
        #region Constructor
        //**************************************************
        //*             CONSTRUCTOR
        //**************************************************
        public Departments()
        {
            InitializeComponent();
            InitializeFormWithData();

        }
        #endregion
        #region Fill data on start
        //**************************************************
        //*             FILL DATA ON START
        //**************************************************
        private void InitializeFormWithData()
        {
            _departmentsPresenter = new DepartmentsPresenter(new DepartmentsServices());
            departmentSearchTextBox.PreviewTextInput += departmentSearchTextBox.OnlyLettersValidationTextBox;
            newDepartmentNameTextbox.PreviewTextInput += newDepartmentNameTextbox.OnlyLettersValidationTextBox;
            departmentChkBox.IsChecked = true;
            departmentGrid.ItemsSource = departamentoList;
            getAllDepartments();
        }
        #endregion
        #region Write data
        //**************************************************
        //*             WRITE DATA
        //**************************************************
        private void saveDepartmentBtn_Click(object sender, RoutedEventArgs e)
        {
            if (departamentoUpdate.idDepartamento.Equals(0))
            {
                createNewDepartment();
                getAllDepartments();
            }
            else
            {
                updateDepartment();
            }
        }
        private void departmentGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            departamentoUpdate = (sender as DataGrid).SelectedValue as Departamento;
            if (departamentoUpdate != null)
            {
                newDepartmentNameTextbox.Text = departamentoUpdate.Descripcion;
                departmentChkBox.IsChecked = departamentoUpdate.Estatus == true ? true : false;
            }
        }
        private void deleteDepartmentBtn_Click(object sender, RoutedEventArgs e)
        {
            departamentoUpdate = departmentGrid.SelectedItem as Departamento;
            if (departamentoUpdate != null)
            {
                departamentoUpdate.Estatus = false;
                _departmentsPresenter.saveDepartment(departamentoUpdate);
                departamentoList.Remove(departamentoUpdate);
                cleanView();
                getAllDepartments();
            }
        }
        #endregion
        #region Read data
        //**************************************************
        //*             READ DATA
        //**************************************************
        private void getAllDepartments()
        {
            departamentoList.Clear();
            departamentoList.AddRange(_departmentsPresenter.GetAllDepartamentos());
            departmentGrid.ItemsSource = departamentoList;
        }
        private void departmentSearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            string departmentTextBox = (sender as TextBox).Text;
            if (e.Key == Key.Enter && !string.IsNullOrWhiteSpace(departmentTextBox))
            {
                departamentoList = new BindingList<Departamento>(_departmentsPresenter.getDepartmenByName(departmentTextBox));
                departmentGrid.ItemsSource = departamentoList;
            }
        }
        private void departmentSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTxtBox = (sender as TextBox).Text;
            if (searchTxtBox == string.Empty)
                getAllDepartments();
        }
        #endregion
        #region Methods form
        //**************************************************
        //*             METHODS FORM
        //**************************************************
        private void updateDepartment()
        {
            if (!string.IsNullOrWhiteSpace(newDepartmentNameTextbox.Text))
            {
                departamentoUpdate.Descripcion = newDepartmentNameTextbox.Text;
                departamentoUpdate.Estatus = departmentChkBox.IsChecked == true ? true : false;
                _departmentsPresenter.saveDepartment(departamentoUpdate);
                if (departamentoUpdate != null && departamentoUpdate.Estatus == false)
                {
                    departamentoList.Remove(departamentoUpdate);
                }
                cleanView();
                departamentoList.ResetBindings();
            }
        }
        private void createNewDepartment()
        {
            if (!string.IsNullOrWhiteSpace(newDepartmentNameTextbox.Text))
            {
                Departamento departamento = new Departamento
                {
                    Descripcion = newDepartmentNameTextbox.Text,
                    Estatus = true
                };
                Departamento addDepartamento = _departmentsPresenter.saveDepartment(departamento).Result;
                cleanView();
                departamentoList.Add(departamento);
            }
        }
        private void cleanView()
        {
            newDepartmentNameTextbox.Text = string.Empty;
            departmentSearchTextBox.Text = string.Empty;
            departamentoUpdate = new Departamento();
        }
        private void activateDepartmentBtn_Click(object sender, RoutedEventArgs e)
        {
            departamentoUpdate = departmentGrid.SelectedItem as Departamento;
            if (departamentoUpdate != null)
            {
                departamentoUpdate.Estatus = true;
                _departmentsPresenter.saveDepartment(departamentoUpdate);
                cleanView();
                getAllDepartments();
            }
        }
        private void cancelDepartmentBtn_Click(object sender, RoutedEventArgs e)
        {
            cleanView();
            getAllDepartments();
        }
        #endregion
    }
}
