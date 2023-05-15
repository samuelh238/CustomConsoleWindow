using ConsoleWindow.ViewModels;
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

namespace ConsoleWindow
{
    /// <summary>
    /// Interaction logic for CustomCommandWindow.xaml
    /// </summary>
    public partial class CustomCommandWindow : UserControl
    {
        private uint _charTyped = 0;
        private string _command = string.Empty;

        private CustomCommandWindowViewModel _customCommandWindowViewModel;

        public CustomCommandWindow()
        {
            InitializeComponent();
            _customCommandWindowViewModel = new CustomCommandWindowViewModel();
            DataContext = _customCommandWindowViewModel;
        }

        private void tbConsoleWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Back:
                    BackSpaceHandle(e);
                    break;

                case Key.Enter:
                    EnterHandle(e);
                    break;

                case Key.Tab:
                case Key.Up:
                    UpHandle(e);
                    break;
                case Key.Down:
                    DownHandle(e);
                    break;
                    e.Handled = true;
                    break;
                case Key.Left:
                    LeftHandle(e);
                    break;
                case Key.Right:
                    RightHandle(e);
                    break;


                case Key.LeftCtrl:
                case Key.RightCtrl:
                case Key.LeftAlt:
                case Key.RightAlt:
                case Key.LeftShift:
                case Key.RightShift:
                    break;

                default:
                    _charTyped = _charTyped + 1;
                    break;
            }
        }

        private void BackSpaceHandle (KeyEventArgs e)
        {
            if(_charTyped != 0)
            {
                int caretIndex = this.tbConsoleWindow.CaretIndex;
                _customCommandWindowViewModel.CurrentLine = _customCommandWindowViewModel.CurrentLine.Substring(0, _customCommandWindowViewModel.CurrentLine.Length - 1);
                this.tbConsoleWindow.Select(this.tbConsoleWindow.Text.Length, 0);
                _charTyped = _charTyped - 1;
            }

            e.Handled = true;
        }

        private void EnterHandle(KeyEventArgs e)
        {
            if (_customCommandWindowViewModel == null)
                return;
            _charTyped = 0;
            _command = _customCommandWindowViewModel.CurrentLine.Split(">")[1];
            _customCommandWindowViewModel.CommandHistory.Add(_command);
            _customCommandWindowViewModel.CommandId = -1;
            string lastLine = _customCommandWindowViewModel.CurrentLine;
            _customCommandWindowViewModel.PreviousText = $"{_customCommandWindowViewModel.PreviousText}\r\n{lastLine}";
            _customCommandWindowViewModel.CurrentLine = _customCommandWindowViewModel.Dir + ">";
            this.tbConsoleWindow.Select(this.tbConsoleWindow.Text.Length, 0);
        }

        private void UpHandle(KeyEventArgs e)
        {
            if (_customCommandWindowViewModel == null)
                return;

            if(_customCommandWindowViewModel.CommandId == -1)
            {
                _customCommandWindowViewModel.CommandId = _customCommandWindowViewModel.CommandHistory.Count - 1;
                _customCommandWindowViewModel.CurrentLine = $"{_customCommandWindowViewModel.Dir}>{_customCommandWindowViewModel.CommandHistory[_customCommandWindowViewModel.CommandId]}";

            }
            else if(_customCommandWindowViewModel.CommandId > 0 && _customCommandWindowViewModel.CommandId <= _customCommandWindowViewModel.CommandHistory.Count - 1)
            {
                _customCommandWindowViewModel.CommandId = _customCommandWindowViewModel.CommandId - 1;
                _customCommandWindowViewModel.CurrentLine = $"{_customCommandWindowViewModel.Dir}>{_customCommandWindowViewModel.CommandHistory[_customCommandWindowViewModel.CommandId]}";
            }
            else
            {
                //CommandId should be zero, do nothing we are at the begin of the list
            }
            e.Handled = true;
            this.tbConsoleWindow.Select(this.tbConsoleWindow.Text.Length, 0);
        }

        private void DownHandle(KeyEventArgs e)
        {
            if (_customCommandWindowViewModel == null)
                return;

            if (_customCommandWindowViewModel.CommandId == _customCommandWindowViewModel.CommandHistory.Count - 1)
            {
                _customCommandWindowViewModel.CommandId = - 1;
                _customCommandWindowViewModel.CurrentLine = $"{_customCommandWindowViewModel.Dir}>";

            }
            else if (_customCommandWindowViewModel.CommandId > -1 && _customCommandWindowViewModel.CommandId <= _customCommandWindowViewModel.CommandHistory.Count - 1)
            {
                _customCommandWindowViewModel.CommandId = _customCommandWindowViewModel.CommandId + 1;
                _customCommandWindowViewModel.CurrentLine = $"{_customCommandWindowViewModel.Dir}>{_customCommandWindowViewModel.CommandHistory[_customCommandWindowViewModel.CommandId]}";
            }
            else
            {
                //CommandId should be -1, do nothing we are at the begin of the list
            }
            e.Handled = true;
            this.tbConsoleWindow.Select(this.tbConsoleWindow.Text.Length, 0);
        }

        private void LeftHandle(KeyEventArgs e)
        {
            int caretIndex = this.tbConsoleWindow.CaretIndex;
            if(caretIndex == _customCommandWindowViewModel.ConsoleText.Length - _charTyped)
            {
                e.Handled = true;
            }
        }

        private void RightHandle(KeyEventArgs e)
        {

        }
    }
}