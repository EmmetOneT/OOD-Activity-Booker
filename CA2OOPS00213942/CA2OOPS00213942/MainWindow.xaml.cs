using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CA2OOPS00213942
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// This aplication lets the user select activities from a list while it checks for conflicts with dates for activities added. It also adds upthe price of the activities to give a total cost
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Activity> Activities = new List<Activity>();
        public ObservableCollection<Activity> chosenActivity = new ObservableCollection<Activity>();
        public ObservableCollection<Activity> typeOfActivity = new ObservableCollection<Activity>();


        public int TotalCost;



        public MainWindow()
        {
            InitializeComponent();
            TotalCost = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Creates activities for the list of activitites from Activity class
            Activity A1 = new Activity() { activityTitle = "Mountain Bike", dateOfActivity = new DateTime(2021, 12, 28), typeOfActivity = "Land", Cost = 160 };
            Activity A2 = new Activity() { activityTitle = "Scuba Dive", dateOfActivity = new DateTime(2021, 12, 27), typeOfActivity = "Water", Cost = 200 };
            Activity A3 = new Activity() { activityTitle = "WingSuit", dateOfActivity = new DateTime(2021, 12, 29), typeOfActivity = "Air", Cost = 500 };
            Activity A4 = new Activity() { activityTitle = "Mountain Climb", dateOfActivity = new DateTime(2021, 12, 28), typeOfActivity = "Land", Cost = 80 };
            Activity A5 = new Activity() { activityTitle = "Kayaking", dateOfActivity = new DateTime(2021, 12, 27), typeOfActivity = "Water", Cost = 80 };
            Activity A6 = new Activity() { activityTitle = "Hang Glide", dateOfActivity = new DateTime(2021, 12, 29), typeOfActivity = "Air", Cost = 420 };
            Activity A7 = new Activity() { activityTitle = "Obstacle Course", dateOfActivity = new DateTime(2021, 12, 28), typeOfActivity = "Land", Cost = 40 };
            Activity A8 = new Activity() { activityTitle = "WindSurf", dateOfActivity = new DateTime(2021, 12, 27), typeOfActivity = "Water", Cost = 60 };
            Activity A9 = new Activity() { activityTitle = "Skydive", dateOfActivity = new DateTime(2021, 12, 29), typeOfActivity = "Air", Cost = 650 };

            
            Activities.Add(A1);
            Activities.Add(A2);
            Activities.Add(A3);
            Activities.Add(A4);
            Activities.Add(A5);
            Activities.Add(A6);
            Activities.Add(A7);
            Activities.Add(A8);
            Activities.Add(A9);

            Activities.Sort();
            lbAllActivities.ItemsSource = Activities;

           

        }
        public void activityDescription(string SelectedActivity)
        {
            //information for each activity
            if (SelectedActivity == "Mountain Bike")
                tbDescription.Text = ("4 Hour Mountain Bike Ride around the local Mountain Trails -€160");
        else if (SelectedActivity == "Scuba Dive")
                tbDescription.Text = ("1 Hour Scuba Dive -€200");
            else if (SelectedActivity == "WingSuit")
                tbDescription.Text = ("5 wingsuit sessions off the peak of a local mountain -€500");
            else if (SelectedActivity == "Mountain Climb")
                tbDescription.Text = ("~3 Hour Ascend a localmountain trail on foot -€80");
            else if (SelectedActivity == "Kayaking")
                tbDescription.Text = ("1 Hour Kayaking in the local forest lake -€80");
            else if (SelectedActivity == "Hang Glide")
                tbDescription.Text = ("5 Hang gliding sessions -€420");
            else if (SelectedActivity == "Obstacle Course")
                tbDescription.Text = ("1 Hour of trying to complete our obstacle course -€40");
            else if (SelectedActivity == "WindSurf")
                tbDescription.Text = ("1 Hour of Wind Surfing off the coast -€60");
            else if (SelectedActivity == "Skydive")
                tbDescription.Text = ("5 Skydiving sessions -€650");


        }

        //checks what radio button is clicked
        private void Activities_Filtered(object sender, RoutedEventArgs e)
        {
            typeOfActivity.Clear();

            if (rbAll.IsChecked == true)
            {
                lbAllActivities.ItemsSource = Activities;
            }

            else if (rbLand.IsChecked == true)
            {
                foreach (Activity Activity in Activities)
                {
                    if (Activity.typeOfActivity == "Land")
                        typeOfActivity.Add(Activity);
                }
                lbAllActivities.ItemsSource = typeOfActivity;
            }
            else if (rbWater.IsChecked == true)
            {
                foreach (Activity Activity in Activities)
                {
                    if (Activity.typeOfActivity == "Water")
                        typeOfActivity.Add(Activity);
                }
                lbAllActivities.ItemsSource = typeOfActivity;
            }
            else if (rbAir.IsChecked == true)
            {
                foreach (Activity Activity in Activities)
                {
                    if (Activity.typeOfActivity == "Air")
                        typeOfActivity.Add(Activity);
                }
                lbAllActivities.ItemsSource = typeOfActivity;
            }
        }

        private void lbAllActivities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //checks selected activity in list
            Activity selectedActivity = lbAllActivities.SelectedItem as Activity;

            //shows description
           if (selectedActivity != null)
            {
                activityDescription(selectedActivity.activityTitle);
           }
        }


        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            Activity SelectedActivity = lbAllActivities.SelectedItem as Activity;

            if (SelectedActivity == null)
            {
                MessageBox.Show("No activity Selected\nSelect an Activity");
            }
            else
            {
                TotalCost = 0;

                foreach (Activity activity in chosenActivity)
                {
                    if (SelectedActivity.dateOfActivity == activity.dateOfActivity)
                    {
                        MessageBox.Show("This date is unavailable");
                        return;
                    }
                }

                Activities.Remove(SelectedActivity);
                chosenActivity.Add(SelectedActivity);
                lbxSelected.ItemsSource = chosenActivity;
                Activities.Sort();
                lbAllActivities.ItemsSource = null;
                lbAllActivities.ItemsSource = Activities;

                foreach (Activity activity in chosenActivity)
                {
                    TotalCost += activity.Cost;
                    tbTotalCost.Text = TotalCost.ToString();
                }

                if (chosenActivity.Count == 0)
                    TotalCost = 0;

            }
        }
        //removing from right listbox back to left hand side
        private void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            
            Activity SelectedActivity = lbxSelected.SelectedItem as Activity;
            //message if nothing is selected
            if (SelectedActivity == null)
            {
                MessageBox.Show("No activity Selected\nSelect an Activity");
            }
            else
            {
                TotalCost = 0;
                chosenActivity.Remove(SelectedActivity);
                Activities.Add(SelectedActivity);
                lbxSelected.ItemsSource = chosenActivity;
                Activities.Sort();
                lbAllActivities.ItemsSource = null;
                lbAllActivities.ItemsSource = Activities;


                foreach (Activity Activity in chosenActivity)
                {
                    TotalCost += Activity.Cost;
                    tbTotalCost.Text = TotalCost.ToString();
                }
            }

            if (chosenActivity.Count == 0)
                TotalCost = 0;
            tbTotalCost.Text = TotalCost.ToString();
        }

       
    }
}
