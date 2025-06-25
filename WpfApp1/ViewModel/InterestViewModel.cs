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
    public class InterestViewModel: INotifyPropertyChanged
    {
        private double _principal;
        private double _rate;
        private double _time;
        private string _interestResult;
        private string _totalResult;

        public double Principal
        {
            get => _principal;
            set { _principal = value; OnPropertyChanged(nameof(Principal)); }
        }

        public double Rate
        {
            get => _rate;
            set { _rate = value; OnPropertyChanged(nameof(Rate)); }
        }

        public double Time
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
            CalculateCommand = new RelayCommand(CalculateInterest);
        }

        private void CalculateInterest(object parameter)
        {
            var model = new InterestModel
            {
                Principal = this.Principal,
                Rate = this.Rate,
                Time = this.Time
            };

            double interest = model.CalculateInterest();
            double total = model.CalculateTotal();

            InterestResult = $"Simple Interest: ₹{interest:N2}";
            TotalResult = $"Total Amount: ₹{total:N2}";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
