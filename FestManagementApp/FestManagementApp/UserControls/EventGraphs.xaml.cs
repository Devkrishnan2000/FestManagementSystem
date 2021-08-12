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
using FestManagementApp.Classes;
using LiveCharts;
using LiveCharts.Wpf;

namespace FestManagementApp.UserControls
{
    /// <summary>
    /// Interaction logic for EventGraphs.xaml
    /// </summary>
    public partial class EventGraphs : UserControl
    {
        
        public EventGraphs()
        {
            InitializeComponent();

            List<double> count_val = new List<double>();
            List<string> enames = new List<string>();
            Database database = new Database();
            List<StatModel> itemlist = new List<StatModel>();
            try
            {
                itemlist = database.popcollege("ename", "event_details");
                foreach (StatModel item in itemlist)
                {
                    count_val.Add(item.Count / 3);
                    enames.Add(item.Itemname);
                }
                SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title ="participants",
                    Values = new ChartValues<double>(count_val)
                }
            };
                Labels = enames.ToArray();
                Formatter = value => value.ToString("N");

                DataContext = this;
            }
            catch(Exception)
            {

            }
            
        }

        

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

    }
}
