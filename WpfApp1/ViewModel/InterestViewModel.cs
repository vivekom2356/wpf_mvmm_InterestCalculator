using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using WpfApp1.Model;

namespace WpfApp1.ViewModel
{
    public class InterestViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private string _principal = "";
        private string _rate = "";
        private string _time = "";
        private string _interestResult = "";
        private string _totalResult = "";

        public string Principal
        {
            get => _principal;
            set { _principal = value; OnPropertyChanged(nameof(Principal)); }
        }

        public string Rate
        {
            get => _rate;
            set { _rate = value; OnPropertyChanged(nameof(Rate)); }
        }

        public string Time
        {
            get => _time;
            set { _time = value; OnPropertyChanged(nameof(Time)); }
        }

        public string InterestResult
        {
            get => _interestResult;
            set { _interestResult = value; OnPropertyChanged(nameof(InterestResult)); }
        }

        public string TotalResult
        {
            get => _totalResult;
            set { _totalResult = value; OnPropertyChanged(nameof(TotalResult)); }
        }

        public ICommand CalculateCommand { get; }

        public InterestViewModel()
        {
            CalculateCommand = new RelayCommand(CalculateInterest, CanCalculate);
        }

        private void CalculateInterest(object parameter)
        {
            double principal = double.Parse(Principal);
            double rate = double.Parse(Rate);
            int time = int.Parse(Time);

            var model = new InterestModel
            {
                Principal = principal,
                Rate = rate,
                Time = time
            };

            double interest = model.CalculateInterest();
            double total = model.CalculateTotal();

            InterestResult = $"Simple Interest: ₹{interest:N2}";
            TotalResult = $"Total Amount: ₹{total:N2}";
        }

        private bool CanCalculate(object parameter)
        {
            return string.IsNullOrEmpty(this["Principal"]) &&
                   string.IsNullOrEmpty(this["Rate"]) &&
                   string.IsNullOrEmpty(this["Time"]);
        }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Principal):
                        if (string.IsNullOrWhiteSpace(Principal) || !double.TryParse(Principal, out _))
                            return "Enter a valid number for Principal";
                        break;
                    case nameof(Rate):
                        if (string.IsNullOrWhiteSpace(Rate) || !double.TryParse(Rate, out _))
                            return "Enter a valid number for Rate";
                        break;
                    case nameof(Time):
                        if (string.IsNullOrWhiteSpace(Time) || !int.TryParse(Time, out _))
                            return "Enter a valid integer for Time";
                        break;
                }
                return null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
