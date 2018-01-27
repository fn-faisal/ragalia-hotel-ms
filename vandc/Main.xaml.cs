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
using System.Windows.Shapes;

namespace RagaliaHotelMS.vandc
{

    public enum TAB_TYPE
    { 
        TAB_BOOKING_CHECK_IN,
        TAB_BOOKING_CHECK_OUT,
        TAB_SERVICE_FOOD
    };

    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {

        private List<TabItem> tabList;
        
        public Main()
        {
            InitializeComponent();

            try
            {
            
                // initialize tabItem array
                tabList = new List<TabItem>();

                // add a tabItem with + in header 
                //TabItem tabAdd = new TabItem();
                //tabAdd.Header = "+";

                //tabList.Add(tabAdd);

                // add first tab
                //TabItem firstTab = this.AddTabItem(TAB_TYPE.TAB_BOOKING_CHECK_IN);
                //tabList.Add(firstTab);

                // bind tab control
                //tabDynamic.DataContext = tabList;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private TabItem AddTabItem( TAB_TYPE tabType )
        {

            int count = tabList.Count;

            // create new tab item
            TabItem tab = new TabItem();
            tab.Header = getTabTitleFromTabType(tabType);
            tab.Name = getTabNameFromTabType(tabType);
            tab.HeaderTemplate = tabDynamic.FindResource("TabHeader") as DataTemplate;
            tab.ContentTemplate = getTabContentFromTabType(tabType);

            if (CheckTabExists(tab.Name))
                return null;

            // insert tab item right before the last (+) tab item
            if (count == 0)
                count += 1;

            
            tabList.Insert(count - 1, tab);
            return tab;
        }
        /*
        private void tabDynamic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem tab = tabDynamic.SelectedItem as TabItem;

            if (tab != null && tab.Header != null)
            {
                if (tab.Header.Equals("+"))
                {
                    // clear tab control binding
                    tabDynamic.DataContext = null;

                    // add new tab
                    TabItem newTab = this.AddTabItem(TAB_TYPE.TAB_BOOKING_CHECK_IN);

                    // bind tab control
                    tabDynamic.DataContext = tabList;

                    // select newly added tab item
                    tabDynamic.SelectedItem = newTab;
                }
                else
                {
                    // your code here...
                }
            }
        }
        */

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string tabName = (sender as Button).CommandParameter.ToString();
            int indexOfTab = getTabIndexFromList(tabName);

            MessageBox.Show("index of current tab : "+indexOfTab
                +" , tab list length : "+tabList.Count);

            if (indexOfTab >= 0) {

                tabDynamic.DataContext = null;

                tabList.RemoveAt(indexOfTab);

                if (tabList.Count > 0)
                {

                    tabDynamic.DataContext = tabList;

                }

            }
            else {
                MessageBox.Show("Error, tab does not exists");
            }
            //var item = tabDynamic.Items.Cast<TabItem>().Where(i => i.Name.Equals(tabName)).SingleOrDefault();
            //TabItem tab = item as TabItem;
            
            /*
            if (tab != null)
            {
                if (tabList.Count < 2)
                {
                    MessageBox.Show("Cannot remove last tab.");
                }
                else if (MessageBox.Show(string.Format("Are you sure you want to remove the tab '{0}'?", tab.Header.ToString()),
                    "Remove Tab", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // get selected tab
                    TabItem selectedTab = tabDynamic.SelectedItem as TabItem;

                    // clear tab control binding
                    tabDynamic.DataContext = null;

                    tabList.Remove(tab);

                    // bind tab control
                    tabDynamic.DataContext = tabList;

                    // select previously selected tab. if that is removed then select first tab
                    if (selectedTab == null || selectedTab.Equals(tab))
                    {
                        selectedTab = tabList[0];
                    }
                    tabDynamic.SelectedItem = selectedTab;
                }
            }*/

        }


        /// <summary>
        /// Close the application, perform the 
        /// required operations before the window
        /// closes!
        /// </summary>
        /// <param name="sender"> sender object </param>
        /// <param name="e"></param>
        private void ExitToWindows(object sender, RoutedEventArgs e)
        {

            //do on destroy exit

            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                System.Environment.Exit(1);
            }
        }

        //+++++++++++++ private helper methods +++++++++++++++++


        public int getTabIndexFromList(string tabName)
        {

            int index = -1;

            foreach (TabItem tempItem in tabList) {
                index++;
                if (tempItem.Name.Equals(tabName))
                    return index;

            }

            return index;
        }

        public bool CheckTabExists( string tabName ) {

            foreach (TabItem tempItem in tabList) {
                if (tempItem.Name.Equals(tabName))
                    return true;
            }

            return false;
        }
        /// <summary>
        /// Get the index of the tab
        /// </summary>
        /// <param name="tabItem"> tab to seatch for </param>
        /// <returns> the index of tab, returns zero if not found </returns>
        private int getTabIndex(TabItem tabItem)
        {
           //var item = tabDynamic.Items.Cast<TabItem>().Where(i => i.Name.Equals(tabName)).SingleOrDefault();

            foreach (TabItem tempItem in tabDynamic.Items.Cast<TabItem>())
            {

                Console.Out.WriteLine("Tab Given : "+tabItem.Header+" , Tab cur : "+tempItem.Header);

                if (tabItem.Header.Equals(tempItem.Header))
                {
                    Console.Out.WriteLine("Tab : "+tempItem.Header+" , found in list");
                    return tempItem.TabIndex;
                }
            }

            return -1;
        }

        /// <summary>
        /// Get the index of the tab by tab name
        /// </summary>
        /// <param name="tabItem"> tab to seatch for </param>
        /// <returns> the index of tab, returns zero if not found </returns>
        private int getTabIndexByName(string tabName)
        {
            try
            {
                var item = tabDynamic.Items.Cast<TabItem>().Where(i => i.Name.Equals(tabName)).SingleOrDefault();

                TabItem tabItem = item as TabItem;

                return tabItem.TabIndex;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return -1;
            }
            
        }

        /// <summary>
        /// This method returns the title for 
        /// requested tab.
        /// </summary>
        /// <param name="tabType"> the type of tab (ENUM : Main.TAB_TYPE) </param>
        /// <returns>requested title</returns>
        private string getTabTitleFromTabType(TAB_TYPE tabType){

            switch (tabType)
            {
                case TAB_TYPE.TAB_BOOKING_CHECK_OUT:
                    return "Check out";
                case TAB_TYPE.TAB_BOOKING_CHECK_IN:
                    return "Check in";
                case TAB_TYPE.TAB_SERVICE_FOOD:
                    return "Food Service";
            }

            return "";
        }



        /// <summary>
        /// This method return the template according to
        /// the tab requested by the user!
        /// </summary>
        /// <param name="tabType"> Type of tab (ENUM : Main.TAB_TYPE) </param>
        /// <returns> The requested template </returns>
        private DataTemplate getTabContentFromTabType(TAB_TYPE tabType)
        {
            switch (tabType)
            {
                case TAB_TYPE.TAB_BOOKING_CHECK_IN:
                    return tabDynamic.FindResource("TabBookingCheckIn") as DataTemplate;
                case TAB_TYPE.TAB_BOOKING_CHECK_OUT:
                    return tabDynamic.FindResource("TabBookingCheckOut") as DataTemplate;
                case TAB_TYPE.TAB_SERVICE_FOOD:
                    return tabDynamic.FindResource("TabServiceFood") as DataTemplate; 
            }
            return null;
        }

        /// <summary>
        /// Get the name of the tab from tab type
        /// </summary>
        /// <param name="tabType"></param>
        /// <returns></returns>
        public String getTabNameFromTabType(TAB_TYPE tabType)
        {
            switch (tabType)
            {
                case TAB_TYPE.TAB_BOOKING_CHECK_IN:
                    return "bookingCheckIn";
                case TAB_TYPE.TAB_BOOKING_CHECK_OUT:
                    return "bookingCheckOut";
                case TAB_TYPE.TAB_SERVICE_FOOD:
                    return "serviceFood";
            }
            return "";
        }

        private void BookingCheckOut_Click(object sender, RoutedEventArgs e)
        {

                // add new tab
                TabItem newTab = this.AddTabItem(TAB_TYPE.TAB_BOOKING_CHECK_OUT);

                
                if (newTab != null)
                {
                
                    // clear tab control binding
                    tabDynamic.DataContext = null;

                    // bind tab control
                    tabDynamic.DataContext = tabList;

                    // select newly added tab item
                    tabDynamic.SelectedItem = newTab;

                }
                else
                {
                    //tabDynamic.SelectedIndex = getTabIndexFromList(getTabNameFromTabType(TAB_TYPE.TAB_BOOKING_CHECK_IN));
                    var item = tabDynamic.Items.Cast<TabItem>().Where(i => i.Name.Equals(getTabNameFromTabType(TAB_TYPE.TAB_BOOKING_CHECK_OUT))).SingleOrDefault();
                    TabItem tab = item as TabItem;

                    tabDynamic.SelectedItem = tab;


                }

            
        }

        private void BookingCheckIn_Click(object sender, RoutedEventArgs e)
        {

            // add new tab
            TabItem newTab = this.AddTabItem(TAB_TYPE.TAB_BOOKING_CHECK_IN);

            if (newTab != null)
            {
                // clear tab control binding
                tabDynamic.DataContext = null;

                // bind tab control
                tabDynamic.DataContext = tabList;

                // select newly added tab item
                tabDynamic.SelectedItem = newTab;
            }
            else
            {
                //tabDynamic.SelectedIndex = getTabIndexFromList( getTabNameFromTabType(TAB_TYPE.TAB_BOOKING_CHECK_IN) );

                var item = tabDynamic.Items.Cast<TabItem>().Where(i => i.Name.Equals(getTabNameFromTabType(TAB_TYPE.TAB_BOOKING_CHECK_IN))).SingleOrDefault();
                TabItem tab = item as TabItem;

                tabDynamic.SelectedItem = tab;
            }


        }
    }
}
