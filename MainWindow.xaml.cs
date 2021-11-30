using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Brushes = System.Windows.Media.Brushes;

namespace DesktopProjectsOrganizerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Process process;
        Project[] projects;
        TextParser txtPrs = new TextParser();
        NotifyIcon ni = new NotifyIcon();
        public MainWindow()
        {
            InitializeComponent();
            
            //Tray icon
            ni.Icon = new Icon("Style.ico");
            ni.Visible = true;
            ni.MouseDown += new System.Windows.Forms.MouseEventHandler(Notifier_HandleMouseDown);

            //parse text and get projects
            projects = txtPrs.GetProjectArray();

            // draw projects
            DrawProjects();

            OpenText.MouseEnter += OpenText_MouseEnter;
            OpenText.MouseLeave += OpenText_MouseLeave;
            OpenTextBorder.MouseEnter += OpenText_MouseEnter;
            OpenTextBorder.MouseLeave += OpenText_MouseLeave;

            process = new Process();
            process.EnableRaisingEvents = true;
            process.Exited += Process_Exited;
            process.StartInfo.FileName = "notepad";
            process.StartInfo.Arguments = "text.txt";
        }

        private void DrawProjects()
        {
            ProjectPanel.Children.Clear();
            projects = txtPrs.GetProjectArray();
            for (int i = 0; i < projects.Length; i++)
            {
                Grid g = projects[i].GetDrawing();
                ProjectPanel.Children.Add(g);
            }
        }

        private void OpenText_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            // set border around icon
            OpenTextBorder.Stroke = Brushes.Transparent;
        }

        private void OpenText_Click(object sender, RoutedEventArgs e)
        {
            process.Start();
        }

        private void Process_Exited(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                DrawProjects();
            });
            
        }

        private void OpenText_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            OpenTextBorder.Stroke = Brushes.White;
        }

        private void Notifier_HandleMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            System.Windows.Controls.ContextMenu menu = (System.Windows.Controls.ContextMenu)this.FindResource("NotifierContextMenu");
            menu.IsOpen = true;
            
            // google: wpf get mouse position => check if outside area => close menu
        }

        private void CloseProgram(object sender, RoutedEventArgs e)
        {
            ni.Visible = false;
            this.Close();
        }

        private void CloseNotifierMenu(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.ContextMenu menu = (System.Windows.Controls.ContextMenu)this.FindResource("NotifierContextMenu");
            menu.IsOpen = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ni.Visible = false;
        }
    }
}
