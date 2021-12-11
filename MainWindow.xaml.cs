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

        Process textProcess;
        Process nodeProcess;
        System.Timers.Timer nodeTimer;
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

            // draw projects
            DrawProjects();

            OpenText.MouseEnter += OpenText_MouseEnter;
            OpenText.MouseLeave += OpenText_MouseLeave;
            OpenTextBorder.MouseEnter += OpenText_MouseEnter;
            OpenTextBorder.MouseLeave += OpenText_MouseLeave;

            textProcess = new Process();
            textProcess.EnableRaisingEvents = true;
            textProcess.Exited += Process_Exited;
            textProcess.StartInfo.FileName = "notepad";
            textProcess.StartInfo.CreateNoWindow = true;
            textProcess.StartInfo.Arguments = "text.txt";

            //check node version
            nodeTimer = new System.Timers.Timer();
            nodeTimer.Interval = 1000;
            nodeTimer.Elapsed += CheckNodeVersion; ;
            nodeTimer.AutoReset = true;
            nodeTimer.Enabled = true;
        }

        private void CheckNodeVersion(object sender, System.Timers.ElapsedEventArgs e)
        {
            nodeProcess = new Process();
            nodeProcess.StartInfo.CreateNoWindow = true;
            nodeProcess.StartInfo.FileName = "node";
            nodeProcess.StartInfo.Arguments = "-v";
            nodeProcess.StartInfo.UseShellExecute = false;
            nodeProcess.StartInfo.RedirectStandardOutput = true;

            nodeProcess.Start();
            while (!nodeProcess.StandardOutput.EndOfStream)
            {
                this.Dispatcher.Invoke(() =>
                {
                    nodevTextBlock.Text = nodeProcess.StandardOutput.ReadLine();
                });
                
            }
        }

        private void DrawProjects()
        {
            ProjectPanel.Children.Clear();
            projects = txtPrs.GetProjectArrayFromJson();
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
            textProcess.Start();
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
