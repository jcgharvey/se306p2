using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Touch_App_Tutorial_2
{
    public class ScatterViewDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;
            if (element != null && item != null)
            {
                if (item is Person)
                    return element.FindResource("ScatterViewItemDataTemplate") as DataTemplate;
                else if (item is Animal)
                    return element.FindResource("ScatterViewItemDataTemplateForAnimal") as DataTemplate;
            }
            return null;
        }
    }
}
