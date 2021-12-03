using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DesktopProjectsOrganizerWPF
{
    class TaskContainer
    {
        string TasksTitle;
        string[] Tasks;
        
        public TaskContainer(string tasksTitle, string[] tasks)
        {
            TasksTitle = tasksTitle;
            Tasks = tasks;
        }

        public Grid GetGrid()
        {
            Grid containerGrid = new Grid()
            {
                Margin = new Thickness { Top = 15 }
            };

            StackPanel mainStack = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
            };

            Border titleTextBlockBorder = new Border()
            {
                BorderBrush = Brushes.White,
                BorderThickness = new Thickness { Bottom = 1 },
                Margin = new Thickness { Bottom = 15 }
            };
            TextBlock titleTextBlock = new TextBlock()
            {
                Text = TasksTitle,
                Width = 60,
                FontSize = 15,
                Foreground = Brushes.White,
                Padding = new Thickness { Top = 15, Right = 15, Left = 15 },
            };
            titleTextBlockBorder.Child = titleTextBlock;

            Border tasksStackBorder = new Border()
            {
                BorderBrush = Brushes.White,
                BorderThickness = new Thickness { Left = 1 },
            };
            StackPanel tasksStack = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness { Left = 8 },
            };
            tasksStackBorder.Child = tasksStack;

            for (int i = 0; i <Tasks.Length; i++)
            {
                Border taskTextBlockBorder = new Border()
                {
                    BorderBrush = Brushes.White,
                    BorderThickness = new Thickness { Bottom = 1 },
                    Margin = new Thickness { Right = 15 },
                };
                TextBlock taskTextBlock = new TextBlock()
                {
                    Text = Tasks[i],
                    Width = Double.NaN,
                    FontSize = 11,
                    Foreground = Brushes.White,
                    Margin = new Thickness { Left = 8 },
                };
                taskTextBlockBorder.Child = taskTextBlock;
                tasksStack.Children.Add(taskTextBlockBorder);
            }

            mainStack.Children.Add(titleTextBlockBorder);
            mainStack.Children.Add(tasksStackBorder);
            containerGrid.Children.Add(mainStack);

            return containerGrid;
        }
    }
}
