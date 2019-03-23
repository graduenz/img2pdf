using img2pdf.Impl;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace img2pdf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IConverter _converter;

        private string[] _sourcePaths;

        public MainWindow()
        {
            InitializeComponent();

            this.Title = $"img2pdf [Guilherme Augusto Raduenz © 2019] - {Assembly.GetExecutingAssembly().GetName().Version}";

            _converter = new DefaultConverter();
        }

        private void BtnSource_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Multiselect = true;

            if (d.ShowDialog() == true) {
                _sourcePaths = d.FileNames;
                txbSource.Text = string.Join("\n", _sourcePaths);
            }
        }

        private void BtnTarget_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog d = new SaveFileDialog();
            d.Filter = "PDF Files (*.pdf)|*.pdf";
            d.DefaultExt = "pdf";
            d.AddExtension = true;

            if (d.ShowDialog() == true) {
                txbTarget.Text = d.FileName;
            }
        }

        private void BtnConvert_Click(object sender, RoutedEventArgs e)
        {
            PdfOrientation orientation = rbA4Portrait.IsChecked == true ? PdfOrientation.A4_Portrait : PdfOrientation.A4_Landscape;

            _converter.ImagesToPdf(_sourcePaths, txbTarget.Text, orientation);

            MessageBox.Show("Arquivo PDF gerado com sucesso!");
        }
    }
}
