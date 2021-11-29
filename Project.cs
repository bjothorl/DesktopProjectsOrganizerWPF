using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DesktopProjectsOrganizerWPF
{
    class Project
    {
        string Title;
        string Nodev;
        string[] LinkUrls;
        string[] LinkLabels;
        string[] TasksTodo;
        string[] TasksDoing;
        string[] TasksDone;

        bool open = false;
        Grid mainGrid;
        Rectangle projectButtonBorder;
        LinkButton[] linkButtons;
        StackPanel mainStack;
        Grid mainStackGrid;
        ScrollViewer mainStackGridScrollViewer;

        public Project(string title, string nodev, string[] linkUrls, string[] linkLables, string[] tasksTodo, string[] tasksDoing, string[] tasksDone)
        {
            Title = title;
            Nodev = nodev;
            LinkUrls = linkUrls;
            LinkLabels = linkLables;
            TasksTodo = tasksTodo;
            TasksDoing = tasksDoing;
            TasksDone = tasksDone;
            linkButtons = new LinkButton[linkUrls.Length];
        }

        public Grid GetDrawing()
        {


            mainGrid = new Grid()
            {
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                VerticalAlignment = System.Windows.VerticalAlignment.Top,
                Height = 35,
            };

            projectButtonBorder = new Rectangle()
            {
                Width = 250,
                Height = 30,
                Fill = Brushes.Black,
                Stroke = Brushes.White,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                VerticalAlignment = System.Windows.VerticalAlignment.Top,
            };

            TextBlock titleTextblock = new TextBlock()
            {
                Text = Title,
                FontSize = 15,
                Foreground = Brushes.White,
                Margin = new Thickness { Top = 5, Left = 10 },
            };
            TextBlock nodevTextblock = new TextBlock()
            {
                Text = Nodev,
                FontSize = 15,
                Foreground = Brushes.White,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                Margin = new Thickness { Top = 5, Right = 10 },
            };

            mainStackGridScrollViewer = new ScrollViewer()
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Visible,
                Margin = new Thickness { Top = 30 },
            };

            mainStackGrid = new Grid()
            {
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                VerticalAlignment = System.Windows.VerticalAlignment.Top,
                Margin = new Thickness { Left = 20 },
                Background = Brushes.Black,
            };

            Border mainStackGridBorder = new Border()
            {
                BorderBrush = Brushes.White,
                BorderThickness = new Thickness { Left = 1 }
            };
 
            mainStack = new StackPanel()
            {
                Orientation = Orientation.Vertical,
            };

            StackPanel linkStack = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness { Left = 0 },
            };

            Border linksTextBlockBorder = new Border()
            {
                BorderBrush = Brushes.White,
                BorderThickness = new Thickness { Bottom = 1 },
                Margin = new Thickness { Bottom = 15 }
            };
            TextBlock linksTextBlock = new TextBlock()
            {
                Text = "Links",
                FontSize = 15,
                Foreground = Brushes.White,
                Padding = new Thickness { Top = 15, Right = 15, Left = 15 },
            };

            linksTextBlockBorder.Child = linksTextBlock;
            linkStack.Children.Add(linksTextBlockBorder);

            for (int i = 0; i < linkButtons.Length; i++)
            {
                linkButtons[i] = new LinkButton(LinkUrls[i], LinkLabels[i]);
                linkStack.Children.Add(linkButtons[i].GetButton());
            }

            TaskContainer tasksTodoContainer = new TaskContainer("Todo", TasksTodo);
            TaskContainer tasksDoingContainer = new TaskContainer("Doing", TasksDoing);
            TaskContainer tasksDoneContainer = new TaskContainer("Done", TasksDone);

            mainStackGridScrollViewer.Visibility = Visibility.Hidden;

            projectButtonBorder.MouseEnter += OnMouseEnter;
            titleTextblock.MouseEnter += OnMouseEnter;
            nodevTextblock.MouseEnter += OnMouseEnter;
            projectButtonBorder.MouseLeave += OnMouseLeave;
            titleTextblock.MouseLeave += OnMouseLeave;
            nodevTextblock.MouseLeave += OnMouseLeave;
            projectButtonBorder.MouseUp += OnMouseUp;
            titleTextblock.MouseUp += OnMouseUp;
            nodevTextblock.MouseUp += OnMouseUp;

            mainStack.Children.Add(linkStack);
            mainStack.Children.Add(tasksTodoContainer.GetGrid());
            mainStack.Children.Add(tasksDoingContainer.GetGrid());
            mainStack.Children.Add(tasksDoneContainer.GetGrid());
            mainStackGrid.Children.Add(mainStackGridBorder);
            mainStackGrid.Children.Add(mainStack);

            mainStackGridScrollViewer.Content = mainStackGrid;

            mainGrid.Children.Add(projectButtonBorder);
            mainGrid.Children.Add(titleTextblock);
            mainGrid.Children.Add(nodevTextblock);
            mainGrid.Children.Add(mainStackGridScrollViewer);
            return mainGrid;
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)   
        {
            mainGrid.Height = open?35:250;
            mainStackGridScrollViewer.Visibility = open ? Visibility.Hidden : Visibility.Visible;
            open = !open;
        }

        void OnMouseEnter(object sender, MouseEventArgs e)
        {
            projectButtonBorder.Fill = Brushes.Gray;
        }
        void OnMouseLeave(object sender, MouseEventArgs e)
        {
            projectButtonBorder.Fill = Brushes.Black;
        }

    }
}
