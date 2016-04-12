using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace code_in.Managers
{
    public class ResourcesEvent : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty ResourceProperty = DependencyProperty.Register(
            "Resource", typeof(object), typeof(ResourcesEvent), new PropertyMetadata(default(object), ResourceChangedCallback));

        public event EventHandler<ResourcesEventArgs> ResourceChanged;

        public object Resource
        {
            get { return GetValue(ResourceProperty); }
            set { SetValue(ResourceProperty, value); }
        }

        private static void ResourceChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            var resourceChangeNotifier = dependencyObject as ResourcesEvent;
            if (resourceChangeNotifier == null)
                return;

            resourceChangeNotifier.OnResourceChanged(new ResourcesEventArgs(args.OldValue, args.NewValue));
        }

        private void OnResourceChanged(ResourcesEventArgs args)
        {
            EventHandler<ResourcesEventArgs> handler = ResourceChanged;
            if (handler != null) handler(this, args);
        }
    }
}
