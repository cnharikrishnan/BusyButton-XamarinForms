using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BusyButtonDemo.Control
{
    public class BusyButton : RelativeLayout, IDisposable
    {
        #region Constructor

        public BusyButton()
        {
            this.Button = new Button();
            this.Loader = new ActivityIndicator
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 30,
                WidthRequest = 30
            };

            Children.Add(this.Button, Constraint.Constant(0), Constraint.Constant(0),
                Constraint.RelativeToParent((p) =>
                {
                    return p.Width;
                }), 
                Constraint.RelativeToParent((p) =>
                {
                    return p.Height;
                }));

            Children.Add(this.Loader, Constraint.RelativeToParent((p) =>
            {
                return p.Width / 2 - 15;
            }),
            Constraint.RelativeToParent((p) =>
            {
                return p.Height / 2 - 15;
            }));
        }

        #endregion

        #region Properties

        public Button Button { get; set; }

        public ActivityIndicator Loader { get; set; }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly BindableProperty TextProperty = 
            BindableProperty.Create(nameof(Text), typeof(string), typeof(BusyButton), string.Empty, propertyChanged: OnTextChanged);

        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        public static readonly BindableProperty IsBusyProperty = 
            BindableProperty.Create(nameof(IsBusy), typeof(bool), typeof(BusyButton), false, propertyChanged: OnIsBusyChanged);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty CommandProperty = 
            BindableProperty.Create(nameof(IsBusy), typeof(ICommand), typeof(BusyButton), null, propertyChanged: OnCommandChanged);

        #endregion

        #region Callback

        private void SetTextBasedOnBusy(bool isBusy, string text)
        {
            var activityIndicator = this.Loader;
            var button = this.Button;
            if (activityIndicator == null || button == null)
                return;

            activityIndicator.IsVisible = activityIndicator.IsRunning = isBusy;
            button.Text = isBusy ? string.Empty : text;
        }

        private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as BusyButton;
            if (control == null)
                return;

            control.SetTextBasedOnBusy(control.IsBusy, newValue.ToString());
        }

        private static void OnIsBusyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as BusyButton;
            if (control == null)
                return;

            control.SetTextBasedOnBusy((bool)newValue, control.Text);
        }

        private static void OnCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as BusyButton;
            var button = control.Button;
            if (control == null || button == null)
                return;

            button.Command = control.Command;
        }

        #endregion

        #region Disposable

        public void Dispose()
        {
            this.Button = null;
            this.Loader = null;
        }

        #endregion
    }
}
