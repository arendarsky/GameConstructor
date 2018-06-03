using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GameConstructor.GUI.UICore
{
    public class UIMethods
    {
        public static UIElement GetUIElementChildByNumberFromListBox(ListBox listBox, int listBoxIndex, int childNumber)
        {
            ListBoxItem itemContainer = listBox.ItemContainerGenerator.ContainerFromIndex(listBoxIndex) as ListBoxItem;

            Border itemContainerBorder = VisualTreeHelper.GetChild(itemContainer, 0) as Border;
            ContentPresenter itemContainerContentPresenter = VisualTreeHelper.GetChild(itemContainerBorder, 0) as ContentPresenter;
            Grid itemUIParent = VisualTreeHelper.GetChild(itemContainerContentPresenter, 0) as Grid;

            return itemUIParent.Children[childNumber];
        }


        public static bool FindCurrentTextInTextBoxesOfTheListBox(ListBox listBox, int childNumber, string wantedStringValue, string messageBoxText)
        {
            for (int i = 0; i < listBox.Items.Count; i++)
            {
                TextBox TextBox = GetUIElementChildByNumberFromListBox(listBox, i, childNumber) as TextBox;

                if (TextBox.Text.ToUpperInvariant() == wantedStringValue.ToUpperInvariant())
                {
                    MessageBox.Show(messageBoxText, "Ошибка!");

                    TextBox.Focus();

                    return true;
                }                
            }

            return false;
        }
    }
}
