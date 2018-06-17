﻿using System;
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

        //Create a Observable Collection to store all out Sound Object
        private ObservableCollection<Sound> mSounds;
      
        public MainPage()
        {
            this.InitializeComponent();

            //Initialize the mSounds to all the available sounds
            mSounds = new ObservableCollection<Sound>();
            SoundManager.GetAllSounds(mSounds);

            //Set the title for the page
            SetHeader("All Sounds");
        }






        private void MyAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            SetHeader("Search");
        }



        /// <summary>
        /// Invoked when we have a click event on one of our object in GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SoundGridView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    

        //From here we are making method calls for our NavigationView



        /// <summary>
        /// Helper method to change the Heading in the NavigationHeader pane
        /// </summary>
        /// <param name="header"> The String which we want to change the header to</param>
        public void SetHeader(string header)
        {
            NavView.Header = header;
        }


        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {

            //Once we have loaded the Navigation View we iterate through every menuitem and set home as 
            //selected by default
            foreach (NavigationViewItemBase item in NavView.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "home")
                {
                    NavView.SelectedItem = item;
                    break;
                }
            }

        }


        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
                // find NavigationViewItem with Content that equals InvokedItem
                var item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);
                NavView_Navigate(item as NavigationViewItem);
            
            
        }


        /// <summary>
        /// Helper method to manage GUI when we click on a NavigationViewItem
        /// </summary>
        /// <param name="item">The NavigationViewItem which we clicked</param>
        private void NavView_Navigate(NavigationViewItem item)
        {
            //Switch the item to idetify its tag
            switch (item.Tag)
            {
                case "home":
                    SetHeader("All Sounds");
                    SoundManager.GetAllSounds(mSounds);
                    //As we are already at home no need to enable back button
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



        /// <summary>
        /// Here we have set the back button to take us to the home page directly as it makes no sense to
        /// go through a backstack when theres only two in each list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            //Selecting the first menu Item i.e. Home
            sender.SelectedItem = sender.MenuItems.First();

            SetHeader("All Sounds");
            SoundManager.GetAllSounds(mSounds);

            NavView.IsBackEnabled = false;

        }







      


       
    }
}
