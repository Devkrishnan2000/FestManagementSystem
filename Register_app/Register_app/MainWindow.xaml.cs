using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Data.SqlClient;
using QRCoder;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Register_app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        bool maxi = false;

        List<string> single_event_list = new List<string>();
        List<string> group_event_list = new List<string>();
        List<int> single_eno_list = new List<int>();
        List<int> grp_eno_list = new List<int>();
        List<int> max_mem_list = new List<int>();
        List<int> SingleNoofRound = new List<int>();
        List<int> grpNoofRound = new List<int>();
        List<string> college_list = new List<string>();
        List<grp_eve_details> grp_details_list = new List<grp_eve_details>();

        public  MainWindow()
        {
            InitializeComponent();
             get_info();
        }

        private SqlConnection set_db()
        {
            
             SqlConnection conect = new SqlConnection();
             conect.ConnectionString = Register_app.Properties.Settings.Default.ConnectionString;
             conect.Open();
             SqlCommand database_sel = new SqlCommand("use "+Properties.Settings.Default.Database, conect);
             database_sel.ExecuteNonQuery();

             return conect;

        }


        private void close_btn_Click(object sender, RoutedEventArgs e)
        {
            Main_window.Close();
        }

        private void max_btn_Click(object sender, RoutedEventArgs e)
        {
            if(maxi==false)
            {
                this.Top = 0;
                this.Left = 0;
                this.Width = SystemParameters.WorkArea.Width;
                this.Height = SystemParameters.WorkArea.Height;
                maxi = true;
            }
            else
            {
                this.Width = 800;
                this.Height = 450;
                maxi = false;
            }
                  
        }

        private void min_btn_Click(object sender, RoutedEventArgs e)
        {
            Main_window.WindowState = WindowState.Minimized;
        }

        private void DockPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if(Mouse.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void new_reg_btn_Click(object sender, RoutedEventArgs e)
        {
            left_side_scroll.Visibility = Visibility.Visible;
            dup_grid.Visibility = Visibility.Collapsed;
        }

        private void dup_btn_Click(object sender, RoutedEventArgs e)
        {
            left_side_scroll.Visibility = Visibility.Collapsed;
            dup_grid.Visibility = Visibility.Visible;
        }

        private async Task get_info()
        {
            debug_info.Text = "Connecting..";
            Bottombar.Fill = (System.Windows.Media.Brush)(new BrushConverter().ConvertFrom("#5352ed"));
              int debug_val =await Task.Run(() => get_info_db());
           
                foreach (string entry in college_list)
                    collegeselct.Items.Add(entry);

                if (single_event_radio.IsChecked == true)
                {
                    foreach (string entry in single_event_list)
                        eventselct.Items.Add(entry);
                }
                else if (grp_event_radio.IsChecked == true)
                {
                    foreach (string entry in group_event_list)
                        eventselct.Items.Add(entry);
                } 
                
                if(debug_val==0)
            {
                debug_info.Text = "Connected And Ready";
                Bottombar.Fill = (System.Windows.Media.Brush)(new BrushConverter().ConvertFrom("#2ecc71"));

            }
                else
            {
                debug_info.Text = "Connection Failed (Error Code :" + debug_val + ")";
                Bottombar.Fill = (System.Windows.Media.Brush)(new BrushConverter().ConvertFrom("#e74c3c"));

            }
                
              
        }

        private int get_info_db()
        {
            try
            {

                SqlConnection connect = set_db();
                SqlCommand get_event_info = new SqlCommand("select * from event", connect);
                SqlCommand get_college = new SqlCommand("select cname from college", connect);

                using (SqlDataReader reader = get_event_info.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        

                        if (int.Parse(reader["ereqmember"].ToString()) > 1)
                        {
                            group_event_list.Add(reader["ename"].ToString());
                            grp_eno_list.Add(int.Parse((reader["eno"]).ToString()));
                            max_mem_list.Add(int.Parse(reader["ereqmember"].ToString()));
                            grpNoofRound.Add(int.Parse(reader["erounds"].ToString()));

                        }
                        else
                        {
                            single_eno_list.Add(int.Parse((reader["eno"]).ToString()));
                            single_event_list.Add(reader["ename"].ToString());
                            SingleNoofRound.Add(int.Parse(reader["erounds"].ToString()));
                        }
                    }

                }

                using (SqlDataReader reader = get_college.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        college_list.Add(reader["cname"].ToString());

                    }
                }
                connect.Close();
                return 0;
            }
            catch(SqlException ex)
            {

                return  ex.Number;


            }
        }

        private void single_event_radio_Checked(object sender, RoutedEventArgs e)
        {
            eventselct.Items.Clear();
            foreach (string entry in single_event_list)
                eventselct.Items.Add(entry);
        }

        private void grp_event_radio_Checked(object sender, RoutedEventArgs e)
        {
            eventselct.Items.Clear();
            foreach (string entry in group_event_list)
                eventselct.Items.Add(entry);
        }

        private void eventselct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(grp_event_radio.IsChecked==true)
            {
                part_head.Visibility = Visibility.Visible;
                participant_list.Visibility = Visibility.Visible;
                part_mem.Visibility = Visibility.Visible;
                add_mem_btn.Visibility = Visibility.Visible;
                remov_mem_btn.Visibility = Visibility.Visible;
                participant_list.Visibility = Visibility.Visible;

            }
            else
            {
                part_head.Visibility = Visibility.Collapsed;
                participant_list.Visibility = Visibility.Collapsed;
                part_mem.Visibility = Visibility.Collapsed;
                add_mem_btn.Visibility = Visibility.Collapsed;
                remov_mem_btn.Visibility = Visibility.Collapsed;
                participant_list.Visibility = Visibility.Collapsed;
            }
        }
       

        private void phnotxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private  int Validation(string email)
        {
            if (string.IsNullOrEmpty(pnametxt.Text))
                return 1;
            else
            if (collegeselct.SelectedIndex == -1)
                return 2;
            else
            if (string.IsNullOrEmpty(phnotxt.Text))
                return 3;
            else
            if(string.IsNullOrEmpty(Emailtxt.Text))
                    return 4;
            else
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(email);                
                }
                catch
                {
                    return 5;
                }
            }
            if (eventselct.SelectedIndex == -1)
                return 6;
            else
            if (eventlist.HasItems == false)
                return 7;
          
            
             
            return 0;
        }

        private async void reg_btn_Click(object sender, RoutedEventArgs e)
        {
            Bottombar.Fill = (System.Windows.Media.Brush)(new BrushConverter().ConvertFrom("#5352ed"));
            switch (Validation(Emailtxt.Text))
            {
                case 0:   var watch = System.Diagnostics.Stopwatch.StartNew();
                          reg_btn.IsEnabled = false;
                          await ins_info();
                          reg_btn.IsEnabled = true;
                   // debug_info.Text = "Successfully Registered Elapsed time "+ watch.ElapsedMilliseconds+"ms";
                    break;
                case 1: debug_info.Text = "Participant Name cannot be Empty";
                    Bottombar.Fill = (System.Windows.Media.Brush)(new BrushConverter().ConvertFrom("#e74c3c"));
                    break;
                case 2: debug_info.Text = "college cannot be Empty";
                    Bottombar.Fill = (System.Windows.Media.Brush)(new BrushConverter().ConvertFrom("#e74c3c"));
                    break;
                case 3: debug_info.Text = "Phone number cannot be Empty";
                    Bottombar.Fill = (System.Windows.Media.Brush)(new BrushConverter().ConvertFrom("#e74c3c"));
                    break;
                case 4: debug_info.Text = "Email cannor be Empty";
                    Bottombar.Fill = (System.Windows.Media.Brush)(new BrushConverter().ConvertFrom("#e74c3c"));
                    break;
                case 5: debug_info.Text =Emailtxt.Text+" Is not a valid Email";
                    Bottombar.Fill = (System.Windows.Media.Brush)(new BrushConverter().ConvertFrom("#e74c3c"));
                    break;
                case 6: debug_info.Text = "Event cant be Empty";
                    Bottombar.Fill = (System.Windows.Media.Brush)(new BrushConverter().ConvertFrom("#e74c3c"));
                    break;
                case 7:debug_info.Text = "Eventlist cant be Empty";
                    Bottombar.Fill = (System.Windows.Media.Brush)(new BrushConverter().ConvertFrom("#e74c3c"));
                    break;
                case 8: debug_info.Text = "Group Events should also Members of the Event";
                    Bottombar.Fill = (System.Windows.Media.Brush)(new BrushConverter().ConvertFrom("#e74c3c"));
                    break;
            }
        }

       

        private void add_event_Click(object sender, RoutedEventArgs e)
        {

            if(eventselct.SelectedIndex!=-1)
            {
                if(!eventlist.Items.Contains(eventselct.SelectedItem.ToString()))
                    eventlist.Items.Add(eventselct.SelectedItem.ToString());
               
           }
            if(grp_event_radio.IsChecked == true)
            {
                List<string> temp_list = new List<string>();
                
                part_mem.Clear();
                part_head.Visibility = Visibility.Collapsed;
                participant_list.Visibility = Visibility.Collapsed;
                part_mem.Visibility = Visibility.Collapsed;
                add_mem_btn.Visibility = Visibility.Collapsed;
                remov_mem_btn.Visibility = Visibility.Collapsed;
                participant_list.Visibility = Visibility.Collapsed;
                foreach(string entry in participant_list.Items)
                {
                    temp_list.Add(entry);
                }

                grp_details_list.Add(new grp_eve_details { event_name = eventselct.SelectedItem.ToString(), participant_names = temp_list });
                participant_list.Items.Clear();
            }
        }

        private void remov_event_Click(object sender, RoutedEventArgs e)
        {
            if(eventlist.HasItems)
            {                       
                    eventlist.Items.Remove(eventlist.SelectedIndex);
            }
        }

        private void add_mem_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!participant_list.Items.Contains(part_mem.Text) && !string.IsNullOrEmpty(part_mem.Text))
            {
                if (participant_list.Items.Count <= max_mem_list[eventselct.SelectedIndex] - 2)
                {
                    participant_list.Items.Add(part_mem.Text);
                    part_mem.Clear();
                }
                else
                {
                    Bottombar.Fill = (System.Windows.Media.Brush)(new BrushConverter().ConvertFrom("#ffa502"));
                    debug_info.Text = max_mem_list[eventselct.SelectedIndex] + " Members are Required for this Event";
                }

            }
            

        }

        private void remov_mem_btn_Click(object sender, RoutedEventArgs e)
        {
            if (participant_list.HasItems)
            {
                participant_list.Items.Remove(participant_list.SelectedItem);
            }
        }

        private async Task ins_info()
        {
            string name = pnametxt.Text;
            string email = Emailtxt.Text;
            string phone = phnotxt.Text;
            string college = collegeselct.SelectedItem.ToString();
            List<string> event_list = new List<string>();
            foreach(string entry in eventlist.Items)
            {
                event_list.Add(entry);
            }

            try
            {
                int pno =  ins_info_participant(event_list, name, email, phone, college);
                Task.Run(() => ins_event_group_details(event_list, name, pno));
                DrawingVisual ticket = ticket_gen(pno, name, college);
                ticket_canvas.Children.Clear();
                preview_grid.Visibility = Visibility.Visible;
                ticket_canvas.Children.Add(new VisualHost { Visual = ticket });
                 save_ticket(ticket, pno);
            }
            catch(SqlException ex)
            {
                debug_info.Text = "Error Occured While Inserting (Error Code:" + ex.Number + ex.LineNumber+ ")";
                Bottombar.Fill = (System.Windows.Media.Brush)(new BrushConverter().ConvertFrom("#e74c3c"));

            }
               
           
            pnametxt.Clear();
            phnotxt.Clear();
            Emailtxt.Clear();
            eventlist.Items.Clear();
            participant_list.Items.Clear();
            grp_details_list.Clear();
        }

        private int save_ticket(DrawingVisual ticket,int pno)
        {

            RenderTargetBitmap visual = new RenderTargetBitmap(700, 700, 128, 128, PixelFormats.Pbgra32);
            visual.Render(ticket);
            PngBitmapEncoder png_img = new PngBitmapEncoder();
             png_img.Frames.Add(BitmapFrame.Create(visual));
            MemoryStream ms = new MemoryStream();
            png_img.Save(ms);
            byte[] img_byte = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(img_byte, 0, img_byte.Length);
           
            SqlConnection connect = set_db();
            string ins_img = "update particpant set ticket =@photo where pno = " + pno + "";
            SqlCommand cmd = new SqlCommand(ins_img, connect);
            cmd.Parameters.AddWithValue("@photo",img_byte);
            cmd.ExecuteNonQuery();
            connect.Close();
            return 0;
        }

        private int ins_info_participant(List<string> event_list, string name, string email,string phone,string college )
        {
         
                SqlConnection connect = set_db();
                int pno = 0;

                string inscmd1 = "insert into particpant (pname,pmail,pphone,pcollege) values('" + name + "','" +email + "','" + phone + "','" + college+ "')";
            
                SqlCommand ins_cmd_part_1 = new SqlCommand(inscmd1, connect);
                ins_cmd_part_1.ExecuteNonQuery();
           
                string read_pno = "select pno from particpant where pname = '" + name + "' AND pmail = '" + email + "'";
                SqlCommand ins_cmd_part_2 = new SqlCommand(read_pno, connect);
               
                using (SqlDataReader reader = ins_cmd_part_2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pno = int.Parse(reader["pno"].ToString());
                    }
                }

                connect.Close();
                return pno;
                      
        }

        private void ins_event_group_details(List<string> event_list, string name,int pno)
        {
                
            SqlConnection connect = set_db();
                for (int i = 0; i < event_list.Count; ++i)
                {
                    int single_num = single_event_list.IndexOf(event_list[i].ToString());
                    if (group_event_list.Contains(event_list[i]))
                    {
                        int grp_num = group_event_list.IndexOf(event_list[i].ToString());
                        grp_eve_details grp_Eve_Details = new grp_eve_details();
                        for (int j = 0; j < grp_details_list.Count; ++j)
                        {
                            grp_Eve_Details = grp_details_list[j];

                            if (event_list[i].ToString() == grp_Eve_Details.event_name)
                            {
                                for (int k = 0; k < grp_Eve_Details.participant_names.Count; ++k)
                                {
                                    SqlCommand grp_event_add = new SqlCommand("insert into grp_mem_details values(" + pno + ",'" + name + "'," + grp_eno_list[grp_num] + ",'" + event_list[i].ToString() + "','" + grp_Eve_Details.participant_names[k].ToString() + "')", connect);
                                    grp_event_add.ExecuteNonQuery();
                                }
                            }
                        }
                        for(int l=1;l<=grpNoofRound[grp_num];++l)
                    {

                        SqlCommand ins_cmd_part_3 = new SqlCommand("insert into event_details (pno,pname,eno,ename,stage) values(" + pno + ",'" + name + "'," + grp_eno_list[grp_num] + ",'" + event_list[i].ToString() + "'," + l + ")", connect);
                        ins_cmd_part_3.ExecuteNonQuery();
                    }

                    }
                    else
                    {
                        single_num = single_event_list.IndexOf(eventlist.Items[i].ToString());
                       for(int l=1;l<=SingleNoofRound[single_num];++l)
                    {
                        SqlCommand ins_cmd_part_3 = new SqlCommand("insert into event_details (pno,pname,eno,ename,stage)  values(" + pno + ",'" + name + "'," + single_eno_list[single_num] + ",'" + event_list[i].ToString() + "'," + l+ ")", connect);
                        ins_cmd_part_3.ExecuteNonQuery();
                    }
                        
                    }
                }

                     
        }


        private System.Drawing.Bitmap qr_code_gen(int pno)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(pno.ToString(), QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            System.Drawing.Bitmap qrCodeImage = qrCode.GetGraphic(20);

            return qrCodeImage;
        }

        private DrawingVisual ticket_gen(int pno,string pname,string cname)
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            Pen ticketborder = new Pen(Brushes.Black, 2);
             
            BitmapImage fest_logo = new BitmapImage();
            fest_logo.BeginInit();                                              //custom logo can be implement by using this code
           fest_logo.UriSource = new Uri(@"pack://application:,,,/Register_app;component/resources/icons/sample.png"); // location of img file
           fest_logo.EndInit();
           
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            Rect border = new Rect(new System.Windows.Point(160, 100), new System.Windows.Size(350, 350));
           Rect logo_cont = new Rect(new Point(190, 120), new Size(300, 50));  // this too
            Rect qr_cont = new Rect(new Point(290, 200), new Size(100, 100));
            FormattedText qrcode_txt = new FormattedText(pno.ToString(), CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("/Register_app;component/resources/#Open Sans"), 10, Brushes.Black, VisualTreeHelper.GetDpi(this).PixelsPerDip) ;
            FormattedText participant_name = new FormattedText(pname.ToUpper().ToString(), CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("/Register_app;component/resources/#Open Sans"), 30, Brushes.Black, VisualTreeHelper.GetDpi(this).PixelsPerDip );
            FormattedText college_name = new FormattedText(cname.ToString(), CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("/Register_app;component/resources/#Open Sans"), 10, Brushes.Black, VisualTreeHelper.GetDpi(this).PixelsPerDip);
            FormattedText From = new FormattedText("FROM", CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("/Register_app;component/resources/#Open Sans"), 20, Brushes.Black, VisualTreeHelper.GetDpi(this).PixelsPerDip);

            participant_name.TextAlignment = TextAlignment.Center;
            qrcode_txt.TextAlignment = TextAlignment.Center;
            college_name.TextAlignment = TextAlignment.Center;
            From.TextAlignment = TextAlignment.Center;

            drawingContext.DrawRoundedRectangle(Brushes.White, ticketborder,border, 5, 5);
            drawingContext.DrawImage(bitmap_to_bitmap_img(qr_code_gen(pno)),qr_cont);
           drawingContext.DrawImage(fest_logo, logo_cont);               
            drawingContext.DrawText(qrcode_txt, new Point(335, 300));
            drawingContext.DrawText(participant_name, new Point(335, 320));
            drawingContext.DrawText(From, new Point(335, 370));
            drawingContext.DrawText(college_name, new Point(335, 400));

           
            drawingContext.Close();

            return drawingVisual;
        }

        private void print_btn_Click(object sender, RoutedEventArgs e)
        {

            PrintDialog prnt_ticket = new PrintDialog();
            prnt_ticket.PrintVisual(ticket_canvas,"printing Ticket");
            
        }

        private BitmapImage bitmap_to_bitmap_img(System.Drawing.Bitmap bitmap)
        {
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();

            return image;
        }

        private void dup_gen_Click(object sender, RoutedEventArgs e)
        {
            dup_ticket_gen();
        }

        private void dup_ticket_gen()
        {

                byte[] ticket_dat = null;
            ticket_canvas.Children.Clear();
            try
            {
                if (!string.IsNullOrEmpty(dup_email.Text))
                {
                    SqlConnection connect = set_db();
                    SqlCommand cmd = new SqlCommand("select ticket from particpant where pmail='" + dup_email.Text + "'", connect);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ticket_dat = (byte[])reader["ticket"];
                            MemoryStream ms = new MemoryStream(ticket_dat);
                            var image = new BitmapImage();
                            image.BeginInit();
                            image.CacheOption = BitmapCacheOption.OnLoad;
                            image.StreamSource = ms;
                            image.EndInit();
                            Image ticket = new Image();
                            ticket.Source = image;
                            ticket_canvas.Children.Add(ticket);
                            preview_grid.Visibility = Visibility.Visible;
                        }
                    }
                    connect.Close();

                   if(ticket_canvas.Children.Count==0)
                    {
                        debug_info.Text = "No participant is Registered under " + dup_email.Text + "";
                        Bottombar.Fill = (System.Windows.Media.Brush)(new BrushConverter().ConvertFrom("#e74c3c"));
                    }
                   else
                    {
                        debug_info.Text = "Ticket Found";
                        Bottombar.Fill = (System.Windows.Media.Brush)(new BrushConverter().ConvertFrom("#2ecc71"));
                    }

                }
            }catch(SqlException ex)
            {
                debug_info.Text = "Error Occured While  (Error Code:" + ex.Number+")";
                Bottombar.Fill = (System.Windows.Media.Brush)(new BrushConverter().ConvertFrom("#e74c3c"));
            }

            
        }
         

        private void chatapp_Click(object sender, RoutedEventArgs e)
        {
            ChatWindow chatWindow = new ChatWindow();
            chatWindow.Show();
        }

        private void Signout_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Reset();
            this.Close();
        }
    }


    public class grp_eve_details
    {
        public string event_name { get; set; }
        public List<string> participant_names { get; set; }
    }

    public class VisualHost : UIElement
    {
        public Visual Visual { get; set; }

        protected override int VisualChildrenCount
        {
            get { return Visual != null ? 1 : 0; }
        }

        protected override Visual GetVisualChild(int index)
        {
            return Visual;
        }
    }


}
