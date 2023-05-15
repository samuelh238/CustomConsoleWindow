using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;

namespace ConsoleWindow.ViewModels
{
    public class CustomCommandWindowViewModel : INotifyPropertyChanged
    {
        private static string _version = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion!;

        private readonly string CONSOLE_HEADER = $"_____CSW_____ Version:{_version}";

        public List<string> CommandHistory;

        public int CommandId { get; set; } = -1;

        public event PropertyChangedEventHandler PropertyChanged; 
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _dir = Environment.CurrentDirectory;
        public string Dir
        {
            get { return _dir; }
            set
            {
                _dir = value;
                NotifyPropertyChanged();
            }
        }

        private string _currentLine = String.Empty;
        public string CurrentLine
        {
            get { return _currentLine; }
            set
            {
                _currentLine = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("ConsoleText");
            }
        }

        private string _previousText = String.Empty;
        public string PreviousText
        {
            get { return _previousText; }
            set
            {
                _previousText = value;
                NotifyPropertyChanged();
                //NotifyPropertyChanged(ConsoleText);
            }
        }

        private string _consoleText = String.Empty;
        public string ConsoleText
        {
            get { return $"{PreviousText}\r\n{CurrentLine}"; }
            set
            {
                var lines = value.Split("\r\n");
                if(lines.Length > 0)
                    CurrentLine = lines[lines.Length-1];
                //NotifyPropertyChanged();
            }
        }

        public CustomCommandWindowViewModel()
        {
            this.CommandHistory = new List<string>();
            this.PreviousText = CONSOLE_HEADER;
            this.CurrentLine = $"{Dir}>";
            
        }

        private void LoadCommandHistory()
        {

        }
    }
}
