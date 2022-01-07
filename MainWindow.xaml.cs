using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AutoLotModel;
using System.Data.Entity;
using System.Data;

namespace AplicatiaWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    enum ActionState
    {
        New,
        Edit,
        Delete,
        Nothing
    }
    public partial class MainWindow : Window
    {
        ActionState action = ActionState.Nothing;
        AutoLotEntitiesModel ctx = new AutoLotEntitiesModel();
        CollectionViewSource doctorAppointmentVSource;
        CollectionViewSource doctorScheduleVSource;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            doctorAppointmentVSource =((System.Windows.Data.CollectionViewSource)(this.FindResource("doctorAppointmentViewSource")));
            doctorAppointmentVSource.Source = ctx.DoctorAppointments.Local;
            ctx.DoctorAppointments.Load();

            doctorScheduleVSource =((System.Windows.Data.CollectionViewSource)(this.FindResource("doctorScheduleViewSource")));
            doctorScheduleVSource.Source = ctx.DoctorSchedules.Local;
            ctx.DoctorSchedules.Load();


        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
            BindingOperations.ClearBinding(nume_pacientTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(nume_MedicTextBox, TextBox.TextProperty);
            SetValidationBinding();
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            doctorAppointmentVSource.View.MoveCurrentToNext();
        }
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            doctorAppointmentVSource.View.MoveCurrentToPrevious();
        }
        private void SaveAppointments()
        {
            DoctorAppointment doctorAppointment = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Customer entity
                    doctorAppointment = new DoctorAppointment()
                    {
                        Detalii = detaliiTextBox.Text.Trim(),
                        Nume_pacient = nume_pacientTextBox.Text.Trim(),
                        Data_Ora = data_OraDatePicker.SelectedDate
                    };
                    //adaugam entitatea nou creata in context
                    ctx.DoctorAppointments.Add(doctorAppointment);
                    doctorAppointmentVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                try
                {
                    doctorAppointment = (DoctorAppointment)doctorAppointmentDataGrid.SelectedItem;
                    doctorAppointment.Detalii = detaliiTextBox.Text.Trim();
                    doctorAppointment.Nume_pacient = nume_pacientTextBox.Text.Trim();
                    doctorAppointment.Data_Ora = data_OraDatePicker.SelectedDate;
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    doctorAppointment = (DoctorAppointment)doctorAppointmentDataGrid.SelectedItem;
                    ctx.DoctorAppointments.Remove(doctorAppointment);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                doctorAppointmentVSource.View.Refresh();
            }
        }
        private void btnNextI_Click(object sender, RoutedEventArgs e)
        {
            doctorScheduleVSource.View.MoveCurrentToNext();
        }

        private void btnPreviousI_Click(object sender, RoutedEventArgs e)
        {
            doctorScheduleVSource.View.MoveCurrentToPrevious();
        }
        private void SaveDoctorSchedule()
        {
            DoctorSchedule doctorSchedule = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Inventory entity 
                    doctorSchedule = new DoctorSchedule()
                    {
                        Nume_Medic = nume_MedicTextBox.Text.Trim(),
                        Data_Ora_sosirii = data_Ora_sosiriiDatePicker.SelectedDate,
                        Data_Ora_plecarii = data_Ora_plecariiDatePicker.SelectedDate
                    };
                    //adaugam entitatea nou creata in context 
                    ctx.DoctorSchedules.Add(doctorSchedule);
                    doctorScheduleVSource.View.Refresh();
                    //salvam modificarile 
                    ctx.SaveChanges();
                }
                //using System.Data; 
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            else if (action == ActionState.Edit)
            {
                try
                {
                    doctorSchedule = (DoctorSchedule)doctorScheduleDataGrid.SelectedItem;
                    doctorSchedule.Nume_Medic = nume_MedicTextBox.Text.Trim();
                    doctorSchedule.Data_Ora_sosirii = data_Ora_sosiriiDatePicker.SelectedDate;
                    doctorSchedule.Data_Ora_plecarii = data_Ora_plecariiDatePicker.SelectedDate;
                    //salvam modificarile 
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            else if (action == ActionState.Delete)
            {
                try
                {
                    doctorSchedule = (DoctorSchedule)doctorScheduleDataGrid.SelectedItem;
                    ctx.DoctorSchedules.Remove(doctorSchedule);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                doctorScheduleVSource.View.Refresh();
            }
        }
        private void gbOperations_Click(object sender, RoutedEventArgs e)
        {

            Button SelectedButton = (Button)e.OriginalSource;
            Panel panel = (Panel)SelectedButton.Parent;

            foreach (Button B in panel.Children.OfType<Button>())
            {
                if (B != SelectedButton)
                    B.IsEnabled = false;
            }

            gbActions.IsEnabled = true;
        }
        private void ReInitialize()
        {

            Panel panel = gbOperations.Content as Panel;

            foreach (Button B in panel.Children.OfType<Button>())
            {


                B.IsEnabled = true;
            }

            gbActions.IsEnabled = false;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ReInitialize();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = tbCtrlAutoLot.SelectedItem as TabItem;

            switch (ti.Header)
            {
                case "Appointments":
                    SaveAppointments();
                    break;
                case "DoctorSchedule":
                    SaveDoctorSchedule();
                    break;
            }
            ReInitialize();
        }
        private void SetValidationBinding()
        {
            Binding numePacientValidationBinding = new Binding();
            numePacientValidationBinding.Source = doctorAppointmentVSource;
            numePacientValidationBinding.Path = new PropertyPath("FirstName");
            numePacientValidationBinding.NotifyOnValidationError = true;
            numePacientValidationBinding.Mode = BindingMode.TwoWay;
            numePacientValidationBinding.UpdateSourceTrigger =
UpdateSourceTrigger.PropertyChanged;
            //string required 
            numePacientValidationBinding.ValidationRules.Add(new StringNotEmpty());
            nume_pacientTextBox.SetBinding(TextBox.TextProperty,numePacientValidationBinding);

            Binding numeDoctorValidationBinding = new Binding();
            numeDoctorValidationBinding.Source = doctorScheduleVSource;
            numeDoctorValidationBinding.Path = new PropertyPath("LastName");
            numeDoctorValidationBinding.NotifyOnValidationError = true;
            numeDoctorValidationBinding.Mode = BindingMode.TwoWay;
            numeDoctorValidationBinding.UpdateSourceTrigger =
UpdateSourceTrigger.PropertyChanged;
            //string min length validator 
            numeDoctorValidationBinding.ValidationRules.Add(new StringMinLengthValidator());
            nume_MedicTextBox.SetBinding(TextBox.TextProperty,numeDoctorValidationBinding); //setare binding nou 

        }
    }
}