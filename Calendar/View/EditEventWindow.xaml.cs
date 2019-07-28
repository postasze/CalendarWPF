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
    /// Interaction logic for EditEventWindow.xaml
    /// </summary>
    public partial class EditEventWindow : Window
    {
        private DayEvent chosenDayEvent;

        public EditEventWindow(DayEvent chosenDayEvent)
        {
            InitializeComponent();
            this.chosenDayEvent = chosenDayEvent;

            for (int i = 0; i < 24; i++)
            {
                this.startHourComboBox.Items.Add(i.ToString());
                this.endHourComboBox.Items.Add(i.ToString());
            }

            for (int i = 0; i < 60; i++)
            {
                this.startMinuteComboBox.Items.Add(i.ToString());
                this.endMinuteComboBox.Items.Add(i.ToString());
            }

            this.descriptionTextBox.Text = chosenDayEvent.GetDescription();
            this.startHourComboBox.SelectedIndex = chosenDayEvent.GetStartTime().Hour;
            this.startMinuteComboBox.SelectedIndex = chosenDayEvent.GetStartTime().Minute;
            this.endHourComboBox.SelectedIndex = chosenDayEvent.GetEndTime().Hour;
            this.endMinuteComboBox.SelectedIndex = chosenDayEvent.GetEndTime().Minute;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            int index = Calendar.Model.Calendar.getInstance().dayEventsList.BinarySearch(chosenDayEvent, new DayEventsComparer());

            ((DayEvent)Calendar.Model.Calendar.getInstance().dayEventsList[index]).SetDescription(this.descriptionTextBox.Text);
            ((DayEvent)Calendar.Model.Calendar.getInstance().dayEventsList[index]).SetStartTime(new DateTime(chosenDayEvent.GetStartTime().Year, chosenDayEvent.GetStartTime().Month, chosenDayEvent.GetStartTime().Day, this.startHourComboBox.SelectedIndex, this.startMinuteComboBox.SelectedIndex, 1));
            ((DayEvent)Calendar.Model.Calendar.getInstance().dayEventsList[index]).SetEndTime(new DateTime(chosenDayEvent.GetEndTime().Year, chosenDayEvent.GetEndTime().Month, chosenDayEvent.GetEndTime().Day, this.endHourComboBox.SelectedIndex, this.endMinuteComboBox.SelectedIndex, 1));
            Calendar.ViewModel.MainWindowViewModel.getInstance().DisplayEvents();
            DiskInputOutput.DiskManager.getInstance().WriteEventsToFile();
            this.Close();
        }

        private void eraseButton_Click(object sender, RoutedEventArgs e)
        {
            int index = Calendar.Model.Calendar.getInstance().dayEventsList.BinarySearch(chosenDayEvent, new DayEventsComparer());
            Calendar.Model.Calendar.getInstance().dayEventsList.RemoveAt(index);
            Calendar.ViewModel.MainWindowViewModel.getInstance().DisplayEvents();
            DiskInputOutput.DiskManager.getInstance().WriteEventsToFile();
            this.Close();
        }
    }
}
