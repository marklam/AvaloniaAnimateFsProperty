namespace ControlLibraryCS
{
    using System;
    using Avalonia;
    using Avalonia.Controls;

    public class StringFormatFloatTextBlock : TextBlock
    {
        public StringFormatFloatTextBlock()
        {
        }

        public static readonly StyledProperty<string> StringFormatProperty = AvaloniaProperty.Register<StringFormatFloatTextBlock, string>("StringFormat", "");
        public static readonly StyledProperty<double> ValueProperty = AvaloniaProperty.Register<StringFormatFloatTextBlock, double>("Value");

        private static void UpdateValue(AvaloniaPropertyChangedEventArgs<double> e)
        {
            if (e.Sender is StringFormatFloatTextBlock sender)
            {
                sender.Text = string.Format(sender.StringFormat, e.NewValue.Value);
            }
        }

        private static void UpdateStringFormat(AvaloniaPropertyChangedEventArgs<string> e)
        {
            if (e.Sender is StringFormatFloatTextBlock sender)
            {
                sender.Text = string.Format(e.NewValue.Value, sender.Value);
            }
        }

        static StringFormatFloatTextBlock()
        {
            RegisterRenderDependenicies();
            StringFormatProperty.Changed.Subscribe(UpdateStringFormat);
            ValueProperty.Changed.Subscribe(UpdateValue);
        }

        private static void RegisterRenderDependenicies()
        {
            AffectsRender<StringFormatFloatTextBlock>(
                StringFormatProperty,
                ValueProperty
            );
        }

        public string StringFormat
        {
            get { return GetValue(StringFormatProperty); }
            set { SetValue(StringFormatProperty, value); }
        }

        public double Value
        {
            get { return GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
    }
}