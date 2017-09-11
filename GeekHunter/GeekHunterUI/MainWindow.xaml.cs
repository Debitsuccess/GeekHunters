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

namespace GeekHunterUI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow() {
			InitializeComponent();
		}

		private void Add(object sender, RoutedEventArgs e)
		{
			var viewModel = (MainViewModel) DataContext;

			var skillIds = viewModel.Skills.Where(skill => skill.IsSelected).Select(skill => skill.Id).ToList();

			viewModel.AddCandidate(firstName.Text, lastName.Text, skillIds);

			// Delete Names, Set skills back to unticked

		}

		private void Search(object sender, RoutedEventArgs e)
		{
			var viewModel = (MainViewModel)DataContext;
			var skillIds = viewModel.Skills.Where(skill => skill.IsSelected).Select(skill => skill.Id).ToList();

			viewModel.SearchCandidate(skillIds);
		}

		private void Clear(object sender, RoutedEventArgs e) {
			var viewModel = (MainViewModel)DataContext;
			
			viewModel.ReloadAllCandidates();
		}
	}
}
