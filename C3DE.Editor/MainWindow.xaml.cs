﻿using System.Windows;
using System.Windows.Controls;

namespace C3DE.Editor
{
    using C3DE.Editor.MonoGameBridge;
    using WpfApplication = System.Windows.Application;

    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private char[] _separator;

        public MainWindow()
        {
            InitializeComponent();
            _separator = new char[1] { '_' };

            editorGameHost.SceneObjectAdded += OnSceneObjectAdded;
        }

        private void OnMenuFileClick(object sender, RoutedEventArgs e)
        {
            var item = sender as MenuItem;

            if (item != null)
            {
                var tmp = item.Name.Split(_separator);

                if (tmp[2] == "Exit")
                    WpfApplication.Current.Shutdown();
            }
        }

        private void OnMenuAddSceneObjectClick(object sender, RoutedEventArgs e)
        {
            var item = sender as MenuItem;

            if (item != null)
            {
                var tmp = item.Name.Split(_separator);
                editorGameHost.Add(tmp[2]);
            }
        }

        private void OnMenuHelpClick(object sender, RoutedEventArgs e)
        {

        }

        private void OnSceneObjectAdded(object sender, SceneObjectAddedEventArgs e)
        {
            var item = new TreeViewItem();
            item.Header = e.SceneObject.Name;
            item.Tag = e.SceneObject.Id;

            sceneTreeView.Items.Add(item);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}