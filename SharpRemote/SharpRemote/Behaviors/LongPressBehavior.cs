using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SharpRemote.Behaviors
{
    public class LongPressBehavior : Behavior<Button>
    {
        private readonly object syncObject = new object();
        private const int Duration = 1000;

        //timer to track long press
        private Timer timer;
        //the timeout value for long press
        private readonly int duration;
        //whether the button was released after press
        private volatile bool isReleased;

        public LongPressBehavior()
        {
            isReleased = true;
            duration = Duration;
        }

        public LongPressBehavior(int duration) : this()
        {
            this.duration = duration;
        }

        /// <summary>
        /// Occurs when the associated button is long pressed.
        /// </summary>
        public event EventHandler LongPressed;

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command),
            typeof(ICommand), typeof(LongPressBehavior), default(ICommand));

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(LongPressBehavior), default);

        /// <summary>
        /// Gets or sets the command parameter.
        /// </summary>
        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        protected override void OnAttachedTo(Button button)
        {
            base.OnAttachedTo(button);

            button.BindingContextChanged += OnButtonBindingContextChanged;
            button.Pressed += Button_Pressed;
            button.Released += Button_Released;
        }

        private void OnButtonBindingContextChanged(object sender, EventArgs e)
        {
            BindingContext = ((Button)sender).BindingContext;
        }

        protected override void OnDetachingFrom(Button button)
        {
            base.OnDetachingFrom(button);
            button.BindingContextChanged += OnButtonBindingContextChanged;
            button.Pressed -= Button_Pressed;
            button.Released -= Button_Released;
        }

        /// <summary>
        /// DeInitializes and disposes the timer.
        /// </summary>
        private void DeInitializeTimer()
        {
            lock (syncObject)
            {
                if (timer is null)
                {
                    return;
                }

                timer.Change(Timeout.Infinite, Timeout.Infinite);
                timer.Dispose();
                timer = null;
            }
        }

        /// <summary>
        /// Initializes the timer.
        /// </summary>
        private void InitializeTimer()
        {
            lock (syncObject)
            {
                timer = new Timer(Timer_Elapsed, null, duration, Timeout.Infinite);
            }
        }

        private void Button_Pressed(object sender, EventArgs e)
        {
            //ExecuteEventAndCommand();

            isReleased = false;

            InitializeTimer();
        }

        private void Button_Released(object sender, EventArgs e)
        {
            isReleased = true;

            DeInitializeTimer();
        }

        protected virtual async Task OnLongPressedAsync()
        {
            while (!isReleased)
            {
                ExecuteEventAndCommand();

                await Task.Delay(100);
            }
        }

        private void ExecuteEventAndCommand()
        {
            LongPressed?.Invoke(this, EventArgs.Empty);

            Command?.Execute(CommandParameter);
        }

        private void Timer_Elapsed(object state)
        {
            DeInitializeTimer();

            if (isReleased)
            {
                return;
            }

            Device.BeginInvokeOnMainThread(async () => await OnLongPressedAsync());
        }
    }
}
