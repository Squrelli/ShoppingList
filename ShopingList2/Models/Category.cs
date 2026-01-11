using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShopingList2.Models
{
    public class Category : INotifyPropertyChanged
    {
        private string name = string.Empty;
        private bool isExpanded = true;

        public string Name { get => name; set { name = value; OnPropertyChanged(); } }
        public bool IsExpanded { get => isExpanded; set { isExpanded = value; OnPropertyChanged(); } }
        public List<Product> Products { get; set; } = new List<Product>();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}