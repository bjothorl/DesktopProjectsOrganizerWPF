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
    class LinkButton
    {
        string LinkUrl;
        string LinkLabel;
        Grid g;
        Rectangle r;
        public LinkButton(string linkUrl, string linkLabel)
        {
            LinkUrl = linkUrl;
            LinkLabel = linkLabel;
        }

        public Grid GetButton()
        {
            g = new Grid()
            {
                Height = 50,
                Width = 50,
                VerticalAlignment = VerticalAlignment.Top,
            };

            r = new Rectangle()
            {
                Height = 45,
                Width = 45,
                Stroke = Brushes.White,
                Fill = Brushes.Black,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
            };

            TextBlock t = new TextBlock()
            {
                Width = 40,
                TextWrapping = TextWrapping.Wrap,
                Text = LinkLabel,
                Foreground = Brushes.White,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextAlignment = TextAlignment.Center,
            };
            r.MouseEnter += OnMouseEnter;
            t.MouseEnter += OnMouseEnter;
            r.MouseLeave += OnMouseLeave;
            t.MouseLeave += OnMouseLeave;

            r.MouseUp += OnMouseUp;
            t.MouseUp += OnMouseUp;

            g.Children.Add(r);
            g.Children.Add(t);
            return g;
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            r.Fill = Brushes.Black;
        }

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            r.Fill = Brushes.Gray;
        }

        private void OnMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            Process.Start("CMD.exe", "/c "+LinkUrl);
        }
    }
}
