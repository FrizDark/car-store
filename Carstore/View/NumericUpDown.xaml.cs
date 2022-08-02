using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Carstore.View
{
    /// <summary>
    /// Interaction logic for NumericUpDown.xaml
    /// </summary>
    public partial class NumericUpDown : UserControl
    {

        public readonly static DependencyProperty MaximumProperty;
        public readonly static DependencyProperty MinimumProperty;
        public readonly static DependencyProperty ValueProperty;
        public readonly static DependencyProperty StepProperty;

        static NumericUpDown()
        {
            MaximumProperty = DependencyProperty.Register("Maximum", typeof(int), typeof(NumericUpDown), new UIPropertyMetadata(10));
            MinimumProperty = DependencyProperty.Register("Minimum", typeof(int), typeof(NumericUpDown), new UIPropertyMetadata(0));
            StepProperty = DependencyProperty.Register("StepValue", typeof(int), typeof(NumericUpDown), new FrameworkPropertyMetadata(1));
            ValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(NumericUpDown), new FrameworkPropertyMetadata(0));
        }

        public NumericUpDown()
        {
            InitializeComponent();
        }

        public int Maximum
        {
            get { return (int)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }
        public int Minimum
        {
            get { return (int)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set 
            {
                SetCurrentValue(ValueProperty, value);
                Field.Text = Value.ToString();
            }
        }
        public int StepValue
        {
            get { return (int)GetValue(StepProperty); }
            set { SetValue(StepProperty, value); }
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            if (Value > Minimum)
            {
                Value -= StepValue;
            }
            if (Value < Minimum)
                Value = Minimum;
        }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            if (Value < Maximum)
            {
                Value += StepValue;
            }
            if (Value > Maximum)
                Value = Maximum;
        }

        private void Field_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(Field.Text, out int value))
                Value = value;
            else
                Field.Text = Value.ToString();
        }

    }
}
