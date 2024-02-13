using Microsoft.Win32;
using ScottPlot.WPF;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using ScottPlot.Plottables;

namespace Reader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OpenFileDialog fileDialog = new OpenFileDialog();
        WpfPlot plt;
        List<Signal> signals;
        public MainWindow()
        {
            InitializeComponent();
            fileDialog.FileName = Settings.Default.FilePath;
            fileDialog.Filter = "txt files (*.txt)|*.txt";
            FilePathLabel.Content = fileDialog.FileName;
            plt = WpfPlot1;
            signals = new List<Signal>();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            DrawFile();
        }

        private void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (fileDialog.ShowDialog() == true)
                {
                    Settings.Default.FilePath = fileDialog.FileName;
                    Settings.Default.Save();
                    FilePathLabel.Content = fileDialog.FileName;
                    DrawFile();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                return;
            }
        }

        private void DrawFile()
        {
            try
            {
                if (!File.Exists(fileDialog.FileName))
                {
                    MessageBox.Show("File does not exist or is not specified");
                    return;
                }
                plt.Plot.Clear();
                // Read file
                string file = File.ReadAllText(fileDialog.FileName);
                if (file == string.Empty) { MessageBox.Show("File is empty", "Error"); return; }
                string[] line = file.TrimEnd('\n').Split('\n');
                if (line.Length < 2) { MessageBox.Show("No data to draw", "Error"); return; }
                string[] dataName = line[0].Split('\t');
                // Parse file data
                double[][] data = new double[dataName.Length][];
                for (int i = 0; i < dataName.Length; i++)
                {
                    data[i] = new double[line.Length - 1];
                }
                for (int i = 1; i < line.Length; i++)
                {
                    string[] l = line[i].Split("\t");
                    for (int j = 0; j < l.Length; j++)
                    {
                        try
                        {
                            data[j][i - 1] = double.Parse(l[j]);
                        }
                        catch (Exception)
                        {
                            data[j][i - 1] = 0f;
                        }
                    }
                }
                // Add signals
                signals = new List<Signal>();
                for (int i = 0; i < data.Length; i++)
                {
                    var sig = plt.Plot.Add.Signal(data[i]);
                    sig.Label = dataName[i];
                    sig.LineWidth = 3;
                    sig.MaximumMarkerSize = 5;
                    signals.Add(sig);
                }
                // Draw labels
                SignalNamesStackPanel.Children.Clear();
                for (int i = 0; i < dataName.Length; i++)
                {
                    CheckBox checkBox = new CheckBox();
                    checkBox.IsChecked = true;
                    checkBox.VerticalContentAlignment = VerticalAlignment.Center;
                    checkBox.Tag = i.ToString();
                    checkBox.Checked += (s, e) => { signals[Convert.ToInt16(checkBox.Tag)].IsVisible = true; plt.Refresh(); };
                    checkBox.Unchecked += (s, e) => { signals[Convert.ToInt16(checkBox.Tag)].IsVisible = false; plt.Refresh(); };
                    SignalNamesStackPanel.Children.Add(checkBox);

                    Label label = new Label();
                    label.Content = signals[i].Label;
                    label.Margin = new Thickness(0, 0, 6, 0);
                    label.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    SignalNamesStackPanel.Children.Add(label);
                }
                // Draw plot
                plt.Plot.ShowLegend();
                plt.Plot.Axes.AutoScale();
                plt.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}