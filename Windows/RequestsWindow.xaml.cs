﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SuperbetBeclean.Pages;
using SuperbetBeclean.Services;
using SuperbetBeclean.Windows; // Assuming you have a service for retrieving requests

namespace SuperbetBeclean
{
    /// <summary>
    /// Interaction logic for RequestsWindow.xaml
    /// </summary>
    public partial class RequestsWindow : Window
    {
        private DBService _dbService;
        string _currentUserName;
        private List<string> requests;
        private SqlConnection sqlConnection;
        private DBService dbService;
        private string connectionString;
        private LobbyPage _lobbyPage;
        public string _userName;
        public RequestsWindow(string currentUserName,LobbyPage lobbyPage,string userName)
        {
            InitializeComponent();
            _userName = userName;
            connectionString = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            _dbService = new DBService(new SqlConnection(connectionString)); // Initialize the database service
            _currentUserName = currentUserName;
            _lobbyPage = lobbyPage;
            // Call a method to load and display requests
            LoadRequests();
            chipsInRequestPage.Text = _dbService.GetChipsByUserId(_dbService.GetUserIdByUserName(_currentUserName)).ToString();
        }

        private void LoadRequests()
        {
            requests = _dbService.GetAllRequestsByToUserID(_dbService.GetUserIdByUserName(_currentUserName)); // Get requests from the database
            RequestsStackPanel.Children.Clear();
            // Create and add request items dynamically
            foreach (string requestInfo in requests)
            {
                Border requestBorder = new Border();
                requestBorder.CornerRadius = new CornerRadius(5);
                requestBorder.Background = Brushes.White;
                requestBorder.Margin = new Thickness(5);

                StackPanel requestPanel = new StackPanel();
                requestPanel.Orientation = Orientation.Horizontal;
                requestPanel.HorizontalAlignment = HorizontalAlignment.Stretch;

                TextBlock requestTextBlock = new TextBlock();
                requestTextBlock.Text = requestInfo;
                requestTextBlock.Margin = new Thickness(10);
                requestTextBlock.VerticalAlignment = VerticalAlignment.Center;

                

                requestPanel.Children.Add(requestTextBlock);
            

                requestBorder.Child = requestPanel;

                RequestsStackPanel.Children.Add(requestBorder);
                

            }
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle accept button click event
        }

        private void DeclineButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle decline button click event
        }
        //Accept all
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Tuple<int, int>> requests = _dbService.GetAllRequestsByToUserIDSimplified(_dbService.GetUserIdByUserName(_currentUserName));

            foreach (Tuple<int, int> request in requests)
            {
                int fromUserID = request.Item1;
                int toUserID = request.Item2;
                int numberChips = _dbService.GetChipsByUserId(fromUserID) + 3000;
                _dbService.UpdateUserChips(fromUserID, _dbService.GetChipsByUserId(fromUserID) + 3000);

                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(RequestsWindow))
                    {
                        RequestsWindow requestWindow = (RequestsWindow)window;
                        if (requestWindow._userName == _dbService.GetUserNameByUserId(fromUserID)){ 
                        
                        //_lobbyPage.PlayerChipsTextBox.Text = _dbService.GetChipsByUserId(fromUserID).ToString();
                        requestWindow.chipsInRequestPage.Text = _dbService.GetChipsByUserId(fromUserID).ToString();
                        requestWindow._lobbyPage.PlayerChipsTextBox.Text = _dbService.GetChipsByUserId(fromUserID).ToString();

                        }

                    }
                    

                }

               
        }
            
            _dbService.DeleteRequestsByUserId(_dbService.GetUserIdByUserName(_currentUserName));
            LoadRequests();
            
        }
        //Decline all
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _dbService.DeleteRequestsByUserId(_dbService.GetUserIdByUserName(_currentUserName));
            LoadRequests();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {   if(_dbService.GetChipsByUserId(_dbService.GetUserIdByUserName(_currentUserName)) == 0) { 
            try {
                string playerToSend = playerToSendRequest.Text;
                    if (_dbService.GetUserIdByUserName(playerToSend) == -1)
                    {
                        MessageBox.Show("Can't find the specified player.");
                        return;
                    }
                int firstPlayerID = _dbService.GetUserIdByUserName(_currentUserName);
                int secondPlayerID = _dbService.GetUserIdByUserName(playerToSend);
                _dbService.CreateRequest(firstPlayerID, secondPlayerID);

            }
            catch
            {
                MessageBox.Show("Can't send multiple request on the same day");
            }
            }
            else
            {
                MessageBox.Show("You must have 0 chips to be able to send requests!");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
