using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using pr7_trpo_nab.Models;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pr7_trpo_nab.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListPage.xaml
    /// </summary>
    public partial class ListPage : Page
    {
        MainWindow _window;
        public ListPage(MainWindow window)
        {
            InitializeComponent();
            _window = window;
            DataContext = window;
        }

        private async Task LoadCourses()
        {
            //await _window.SafeExecuteAsync(async () =>
            //{
            //    string? teacher = string.IsNullOrWhiteSpace(TeacherFilter.Text) ? null : TeacherFilter.Text;
            //    int? minDuration = null;

            //    if (!string.IsNullOrWhiteSpace(MinDurationFilter.Text))
            //    {
            //        if (!int.TryParse(MinDurationFilter.Text.Trim(), out var v) || v < 0)
            //            throw new Exception("Неккоректное число");
            //        minDuration = v;
            //    }

            //    var list = await _window.Api.GetCoursesAsync(teacher, minDuration);
            //    _window.Courses.Clear();
            //    foreach (var c in list) _window.Courses.Add(c);
            //});
        }

        private async void Load_Click(object sender, RoutedEventArgs e)
        {
            await LoadCourses();
        }

        private async void Reset_Click(object sender, RoutedEventArgs e)
        {
            TeacherFilter.Text = "";
            MinDurationFilter.Text = "";
            await LoadCourses();
        }

        private async void OpenById_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(SearchIdBox.Text.Trim(), out var id) || id <= 0)
            {
                MessageBox.Show("Некорректный id", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            await _window.SafeExecuteAsync(async () =>
            {
                var course = await _window.Api.GetCourseAsync(id);
                _window.ShowEditPage(course);
            });
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            _window.ShowEditPage(null);
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (CoursesDataGrid.SelectedItem is Course course)
                _window.ShowEditPage(course);
            else
                MessageBox.Show("Выберите запись", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (CoursesDataGrid.SelectedItem is not Course course)
            {
                MessageBox.Show("Выберите запись для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (MessageBox.Show($"Удалить курс Id = {course.Id} - {course.Title}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;

            await _window.SafeExecuteAsync(async () =>
            {
                await _window.Api.DeleteCourseAsync(course.Id);
                await LoadCourses();
                MessageBox.Show("Запись удалена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            });
        }
    }
}
