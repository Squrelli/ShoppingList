using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShopingList2.Models
{
    public class Product : INotifyPropertyChanged
    {
        private string name = string.Empty;
        private double amount;
        private string unit = string.Empty;
        private bool isPurchased;

        public string Name { get => name; set { name = value; OnPropertyChanged(); } }
        public double Amount { get => amount; set { amount = value; OnPropertyChanged(); } }
        public string Unit { get => unit; set { unit = value; OnPropertyChanged(); } }
        public bool IsPurchased { get => isPurchased; set { isPurchased = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}