using System;
using System.Windows;
using System.Windows.Input;

namespace IisReader.Views
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {

        public Home()
        {
            InitializeComponent();
            windowTitle.PreviewMouseLeftButtonDown += _moveBorder_PreviewMouseLeftButtonDown;

        }

        Point _startPoint;
        bool _isDragging = false;

        private void _moveBorder_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Capture(this))
            {
                _isDragging = true;
                _startPoint = PointToScreen(Mouse.GetPosition(this));
            }
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            if (_isDragging)
            {
                Point newPoint = PointToScreen(Mouse.GetPosition(this));
                int diffX = (int)(newPoint.X - _startPoint.X);
                int diffY = (int)(newPoint.Y - _startPoint.Y);
                if (Math.Abs(diffX) > 1 || Math.Abs(diffY) > 1)
                {
                    Left += diffX;
                    Top += diffY;
                    InvalidateVisual();
                    _startPoint = newPoint;
                }
            }
        }
        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (_isDragging)
            {
                _isDragging = false;
                Mouse.Capture(null);
            }
        }

    }
}
