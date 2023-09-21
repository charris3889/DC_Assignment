﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDLL;

namespace ChatServerDLL
    {
        public class DatabaseSingleton
        {
            private static Database instance;

            public DatabaseSingleton()
            {
                if (instance == null)
                {
                    instance = new Database();
                }
            }
            public bool HasUser(string username)
            {
                return instance.users.Exists(u => u.Name == username);
            }

            public User CreateUser(string username)
            {
                if (string.IsNullOrEmpty(username)) return null; //Handle invalid usernames.

                User newUser = new User(username);
                instance.users.Add(newUser);
                return newUser;
            }

            public bool HasChatroom(string chatroom)
            {
                if (instance.chatrooms.ContainsKey(chatroom))
                {
                    return true;
                }

                return false;
            }

            public void CreateChatroom(string chatname)
            {
                if (string.IsNullOrEmpty(chatname)) return; //Handle invalid chat names.

                if (!instance.chatrooms.ContainsKey(chatname))
                {
                    instance.chatrooms[chatname] = new List<string>();
                }
            }

            public void CreatePersonalRoom(User user1, User user2)
            {
                if (user1 == null || user2 == null) return; //Handle null users.

                string personalRoomName = $"{user1.Name}_{user2.Name}";
                if (!instance.chatrooms.ContainsKey(personalRoomName))
                {
                    instance.chatrooms[personalRoomName] = new List<string>();
                }
            }

            public void EnterChatroom(User user, string chatname)
            {
                if (user == null || string.IsNullOrEmpty(chatname)) return; //Handle edge cases.

                if (instance.chatrooms.ContainsKey(chatname))
                {
                    Console.WriteLine($"[{DateTime.Now}]{user.Name} has entered the chatroom.");
                }
            }

            public void SendMessage(User user, string chatname, string message)
            {
                if (user == null || string.IsNullOrEmpty(chatname) || string.IsNullOrEmpty(message)) return; //Handle edge cases.

                if (instance.chatrooms.ContainsKey(chatname))
                {
                    instance.chatrooms[chatname].Add($"[{DateTime.Now}] --> {user.Name}: {message}");
                }
            }

            public List<string> ReceiveMessage(string chatname)
            {
                if (string.IsNullOrEmpty(chatname)) return null; //Handle invalid chat names.

                if (instance.chatrooms.ContainsKey(chatname))
                {
                    return instance.chatrooms[chatname];
                }
                return null; //Or return an empty list.
            }

            public List<string> ForDisplayChatrooms()
            {
                return new List<string>(instance.chatrooms.Keys);
            }
        }
    }
