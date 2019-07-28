using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Calendar.Model;

namespace Calendar.View
{
    /// <summary>
    /// Interaction logic for AddEventWindow.xaml
    /// </summary>
    public partial class AddEventWindow : Window
    {
        private DateTime chosenDate;

        public AddEventWindow(DateTime chosenDate)
        {
            InitializeComponent();
            for (int i = 0; i < 24; i++)
            {
                this.startHourComboBox.Items.Add(i.ToString());
                this.endHourComboBox.Items.Add(i.ToString());
            }
            this.startHourComboBox.SelectedIndex = 0;
            this.endHourComboBox.SelectedIndex = 0;

            for (int i = 0; i < 60; i++)
            {
                this.startMinuteComboBox.Items.Add(i.ToString());
                this.endMinuteComboBox.Items.Add(i.ToString());
            }
            this.startMinuteComboBox.SelectedIndex = 0;
            this.endMinuteComboBox.SelectedIndex = 0;
            this.chosenDate = chosenDate;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            DayEvent dayEvent = new DayEvent(this.descriptionTextBox.Text,
                new DateTime(chosenDate.Year, chosenDate.Month, chosenDate.Day, this.startHourComboBox.SelectedIndex, this.startMinuteComboBox.SelectedIndex, 1),
                new DateTime(chosenDate.Year, chosenDate.Month, chosenDate.Day, this.endHourComboBox.SelectedIndex, this.endMinuteComboBox.SelectedIndex, 1));

            Calendar.Model.Calendar.getInstance().dayEventsList.Add(dayEvent);
            Calendar.Model.Calendar.getInstance().dayEventsList.Sort(new DayEventsComparer());
            Calendar.ViewModel.MainWindowViewModel.getInstance().DisplayEvents();
            DiskInputOutput.DiskManager.getInstance().WriteEventsToFile();
            this.Close();
        }
    }
}
