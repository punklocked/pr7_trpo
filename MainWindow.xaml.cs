using System.Text;
using System.Windows;
using pr7_trpo_nab.Pages;
using pr7_trpo_nab.Models;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using pr7_trpo_nab.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Net.Http;
using System.Text.Json;

namespace pr7_trpo_nab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CourseApiService Api { get; } = new();
        public ObservableCollection<Course> Courses { get; } = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            MainFrame.Navigate(new ListPage(this));
        }

        public void ShowEditPage(Course? course) => MainFrame.Navigate(new EditPage(this, course));
        public void ShowListPage() => MainFrame.Navigate(new ListPage(this));
        public async Task SafeExecuteAsync(Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.StatusCode.HasValue ? $"Ошибка {ex.StatusCode.Value}" : "Сервер недоступен", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show("Превышено время ожидания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (JsonException)
            {
                MessageBox.Show("Ошибка JSON", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}