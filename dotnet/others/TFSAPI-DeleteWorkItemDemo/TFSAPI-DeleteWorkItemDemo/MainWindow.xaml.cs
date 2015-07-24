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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TFSAPI_DeleteWorkItemDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WorkItemStore store;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_server_Click(object sender, RoutedEventArgs e)
        {
            btn_delete.IsEnabled = false;
            TeamProjectPicker pp = new TeamProjectPicker(TeamProjectPickerMode.SingleProject, false);
            pp.ShowDialog();

            if (pp.SelectedTeamProjectCollection != null)
            {
                pp.SelectedTeamProjectCollection.EnsureAuthenticated();
                store = (WorkItemStore)pp.SelectedTeamProjectCollection.GetService(typeof(WorkItemStore));
                btn_delete.IsEnabled = true;
            }
        }

        IEnumerable<WorkItemOperationError> DeleteWorkItem(int[] ids)
        {
            try
            {
                return store.DestroyWorkItems(ids);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        void DeleteStatus(IEnumerable<WorkItemOperationError> enumerable)
        {
            List<WorkItemOperationError> list = new List<WorkItemOperationError>(enumerable);
            if (list.Count > 0)
            {
                StringBuilder builder = new StringBuilder();
                for (int j = 0; j < list.Count; j++)
                {
                    builder.AppendLine(string.Format("Work Item Id:{0}, {1}", list[j].Id, list[j].Exception.Message));
                }
                MessageBox.Show(builder.ToString(), "Error Deleting Work Item", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Done");
                txt_id.Text = string.Empty;
            }
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_id.Text) && IsInteger(txt_id.Text))
            {
                IEnumerable<WorkItemOperationError> enumerable;
                int[] ids = new int[] { Convert.ToInt32(txt_id.Text) };
                enumerable = DeleteWorkItem(ids);
                DeleteStatus(enumerable);
            }
        }

        public static bool IsInteger(string theValue)
        {
            try
            {
                Convert.ToInt32(theValue);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
