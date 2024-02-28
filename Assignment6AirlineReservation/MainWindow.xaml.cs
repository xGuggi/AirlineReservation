using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        wndAddPassenger wndAddPass;
        clsPassengerManager passengerManager;
        clsFlightManager flightManager;
        List<clsPassenger> passengers;
        /// <summary>
        /// This variable means we are in Add PassengerMode;
        /// </summary>
        bool bAddPassengerMode;
        /// <summary>
        /// this Variable means we are in ChangeSeatMode;
        /// </summary>
        bool bChangeSeatMode;
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

                flightManager = new clsFlightManager();

                cbChooseFlight.ItemsSource = clsFlightManager.GetFlights();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// This Method fills up Passengers base on Flight
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbChooseFlight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                clsFlight clsSelectedFlight = (clsFlight)cbChooseFlight.SelectedItem;
                if (clsSelectedFlight.sFlightID == "1")
                {
                    CanvasA380.Visibility = Visibility.Visible;
                    Canvas767.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Canvas767.Visibility = Visibility.Visible;
                    CanvasA380.Visibility = Visibility.Collapsed;
                }
                clsPassengerManager.GetPassengers(clsSelectedFlight.sFlightID);
                cbChoosePassenger.ItemsSource = clsPassengerManager.GetPassengers(clsSelectedFlight.sFlightID);
                gPassengerCommands.IsEnabled = true;
                lblPassengersSeatNumber.Content = " ";
                FillPassengerSeats(clsSelectedFlight, clsPassengerManager.GetPassengers(clsSelectedFlight.sFlightID));
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// add passenger click method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAddPassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wndAddPass = new wndAddPassenger();
                wndAddPass.ShowDialog();

                if (wndAddPass.isSaved == true)
                {
                    gbPassengerInformation.IsEnabled = false;
                    gPassengerCommands.IsEnabled = false;
                    bAddPassengerMode = true;
                    lblPassengersSeatNumber.Content = " ";
                }

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// This method allows the user to change Seats.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdChangeSeat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbChoosePassenger.SelectedItem != null)
                {
                    gbPassengerInformation.IsEnabled = false;
                    gPassengerCommands.IsEnabled = false;
                    bChangeSeatMode = true;
                    lblPassengersSeatNumber.Content = " ";
                }
                
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);

            }
        }
        /// <summary>
        /// This method deletes a passenger from the list of passengers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDeletePassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //selected passenger
                if (cbChoosePassenger.SelectedItem != null)
                {
                    //delete the currently selected passenger
                    clsPassenger passenger = new clsPassenger();
                    passenger = (clsPassenger)cbChoosePassenger.SelectedItem;
                    string sFlightID = (cbChooseFlight.SelectedIndex + 1).ToString();
                    string selectedPassengerName = cbChoosePassenger.SelectedItem.ToString();
                    string[] parts = selectedPassengerName.Split(' ');
                    if (parts.Length == 3)
                    {
                        passenger.sFirstName = parts[1];
                        passenger.sLastName = parts[2];
                        clsPassengerManager.DeletePassenger(sFlightID, passenger.sFirstName, passenger.sLastName);
                    }
                    //reload the passengers into the combo box
                    cbChoosePassenger.ItemsSource = clsPassengerManager.GetPassengers(sFlightID);
                    // reload the taken seats
                    clsFlight clsSelectedFlight = (clsFlight)cbChooseFlight.SelectedItem;
                    FillPassengerSeats(clsSelectedFlight, clsPassengerManager.GetPassengers(clsSelectedFlight.sFlightID));
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);

            }
        }
        /// <summary>
        /// This method will get called when a user clicks on any seat.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Seat_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Label clickedSeat = (Label)sender;
                //AddPassengerMode
                //Insert a new passenger into the database, then insert a record into the link table
                if (bAddPassengerMode == true && clickedSeat.Background != Brushes.Red)
                {
                    clickedSeat.Background = Brushes.Red;
                    clsPassenger passenger = new clsPassenger();
                    passenger.sFirstName = wndAddPass.txtFirstName.Text;
                    passenger.sLastName = wndAddPass.txtLastName.Text;
                    string sSeatNumber = clickedSeat.Content.ToString();
                    string sFlightID = (cbChooseFlight.SelectedIndex + 1).ToString();
                    clsPassengerManager.InsertNewPassenger(passenger.sFirstName, passenger.sLastName, sFlightID, sSeatNumber);
                    cbChoosePassenger.ItemsSource = clsPassengerManager.GetPassengers(sFlightID);
                    bAddPassengerMode = false;
                    gbPassengerInformation.IsEnabled = true;
                    gPassengerCommands.IsEnabled = true;
                }
                //bChangeSeatMode
                //Only change the seat if the seat is empty (blue)
                // If its empty, then update the link table to update the user's new seat
                else if (bChangeSeatMode == true && clickedSeat.Background == Brushes.Blue)
                {
                    /*clsPassenger passenger = new clsPassenger();
                    string sSeatNumber = clickedSeat.Content.ToString();
                    string sFlightID = (cbChooseFlight.SelectedIndex + 1).ToString();
                    string selectedPassengerName = cbChoosePassenger.SelectedItem.ToString();
                    string[] parts = selectedPassengerName.Split(' ');
                    if (parts.Length == 3)
                    {
                        passenger.sFirstName = parts[1];
                        passenger.sLastName = parts[2];
                        clsPassengerManager.UpdateSeat(sFlightID, sSeatNumber, passenger.sFirstName, passenger.sLastName);
                    }*/
                    string sSeatToChangeToNumber = clickedSeat.Content.ToString();
                    clsFlight SelectedFlight = (clsFlight)cbChooseFlight.SelectedItem;
                    string selectedPassengerName = cbChoosePassenger.SelectedItem.ToString();
                    foreach (clsPassenger Passenger in cbChoosePassenger.Items)
                    {
                        string[] parts = selectedPassengerName.Split(' ');
                        if (parts.Length == 3)
                        {
                            clickedSeat.Background = Brushes.Red;
                            Passenger.sFirstName = parts[1];
                            Passenger.sLastName = parts[2];
                            clsPassengerManager.UpdateSeat(SelectedFlight.sFlightID, sSeatToChangeToNumber, Passenger.sFirstName, Passenger.sLastName);
                            break;
                        }
                    }
                    cbChoosePassenger.ItemsSource = clsPassengerManager.GetPassengers(SelectedFlight.sFlightID);
                    FillPassengerSeats(SelectedFlight, clsPassengerManager.GetPassengers(SelectedFlight.sFlightID));
                    bChangeSeatMode = false;
                    gbPassengerInformation.IsEnabled = true;
                    gPassengerCommands.IsEnabled = true;
                    lblPassengersSeatNumber.Content = " ";
                }
                // if a seat is taken (red), then loop through the passengers in the combo box,
                // and keep looping until the seat that was clicked, its number matches a passenger's seat number,
                // then selected that combo box index or selected item and put the passenger's seat in the label
                else if (clickedSeat.Background == Brushes.Red)
                {
                    clsFlight SelectedFlight = (clsFlight)cbChooseFlight.SelectedItem;
                    string sSeatNumber = clickedSeat.Content.ToString();
                    foreach (var passenger in cbChoosePassenger.Items)
                    {
                        string[] parts = passenger.ToString().Split(' ');
                        if (parts.Length == 3 && parts[0] == sSeatNumber)
                        {
                            cbChoosePassenger.SelectedItem = passenger;
                            lblPassengersSeatNumber.Content = parts[0];
                            FillPassengerSeats(SelectedFlight, clsPassengerManager.GetPassengers(SelectedFlight.sFlightID));
                            clickedSeat.Background = Brushes.Green;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                   MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
            
        }
        /// <summary>
        /// This method Fills all the PassengerSeats with people
        /// </summary>
        private void FillPassengerSeats(clsFlight selectedFlight, List<clsPassenger> passengers)
        {
            try
            {
                //FillPassengerSeats
                //Reset all seats in the selected flight to blue.
                //Loop through each passenger in the list
                //Then Loop through each seat in the selected flight, like "c767_Seats.Children"
                //Then compare the passengers seat to the label's content and if they match, then change the background to red because the seat is taken.
                if (selectedFlight.sFlightID == "1")
                {
                    ResetSeatsToBlueA380();
                }
                else
                {
                    ResetSeatsToBlue767();
                }
                foreach (clsPassenger passenger in passengers)
                {
                    if (selectedFlight.sFlightID == "1")
                    {
                        foreach (Label myLabel in cA380_Seats.Children)
                        {
                            if (myLabel.Content.ToString() == passenger.sSeatNumber)
                            {
                                myLabel.Background = Brushes.Red;
                                break;
                            }
                        }
                    }
                    else
                    {
                        foreach (Label myLabel in c767_Seats.Children)
                        {
                            if (myLabel.Content.ToString() == passenger.sSeatNumber)
                            {
                                myLabel.Background = Brushes.Red;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                   MethodInfo.GetCurrentMethod().Name, ex.Message);

            }
           
        }
        /// <summary>
        /// Resets Seats to Blue in 767 Flight
        /// </summary>
        private void ResetSeatsToBlue767()
        {
            foreach (Label myLabel in c767_Seats.Children)
            {
                myLabel.Background = Brushes.Blue;
            }
        }
        /// <summary>
        /// Reset Seats to Blue in A380 Flight
        /// </summary>
        private void ResetSeatsToBlueA380()
        {
            foreach (Label myLabel in cA380_Seats.Children)
            {
                myLabel.Background = Brushes.Blue;
            }
        }
        /// <summary>
        /// This method handles all the Errors
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }
    }
}
