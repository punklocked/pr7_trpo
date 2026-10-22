using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace pr7_trpo_nab.Models
{
    public class CourseFilter : INotifyPropertyChanged
    {
        private string _teacher = "";
        public string Teacher
        {
            get { return _teacher; }
            set { _teacher = value; OnPropertyChanged(); }
        }

        private string _minDuration = "";
        public string MinDuration
        {
            get { return _minDuration; }
            set { _minDuration = value; OnPropertyChanged(); }
        }

        public void Clear()
        {
            Teacher = "";
            MinDuration = "";
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }
    }
}
