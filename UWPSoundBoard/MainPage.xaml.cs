using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWPSoundBoard.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace UWPSoundBoard
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private ObservableCollection<Sound> mSounds;
      
        public MainPage()
        {
            this.InitializeComponent();

            mSounds = new ObservableCollection<Sound>();
            SoundManager.GetAllSounds(mSounds);


            SetHeader("All Sounds");
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
           
            
                // find NavigationViewItem with Content that equals InvokedItem
                var item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);
                NavView_Navigate(item as NavigationViewItem);
            
            
        }

        public void SetHeader(string header)
        {
            NavView.Header = header;
        }

        private void NavView_Navigate(NavigationViewItem item)
        {
            
            switch (item.Tag)
            {
                case "home":
                    SetHeader("All Sounds");
                    SoundManager.GetAllSounds(mSounds);
                    NavView.IsBackEnabled = false;
                    break;

                case "animals":
                    SetHeader("Animal Sounds");
                    SoundManager.GetSoundsByCategory(mSounds, SoundCategory.Animals);
                    NavView.IsBackEnabled = true;
                    break;

                case "cartoon":
                    SetHeader("Cartoon Sounds");
                    SoundManager.GetSoundsByCategory(mSounds, SoundCategory.Cartoons);
                    NavView.IsBackEnabled = true;
                    break;

                case "taunt":
                    SetHeader("Taunt Sounds");
                    SoundManager.GetSoundsByCategory(mSounds, SoundCategory.Taunts);
                    NavView.IsBackEnabled = true;
                    break;

                case "warning":
                    SetHeader("Warning Sounds");
                    SoundManager.GetSoundsByCategory(mSounds, SoundCategory.Warnings);
                    NavView.IsBackEnabled = true;
                    break;
            }
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (NavigationViewItemBase item in NavView.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "home")
                {
                    NavView.SelectedItem = item;
                    break;
                }
            }


        }

        private void MyAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            SetHeader("Search");
        }

        private void SoundGridView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }


        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            sender.SelectedItem = sender.MenuItems.First();
            SetHeader("All Sounds");
            SoundManager.GetAllSounds(mSounds);
            NavView.IsBackEnabled = false;

        }
    }
}
