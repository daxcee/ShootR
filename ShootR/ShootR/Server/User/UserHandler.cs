﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShootR
{
    public class UserHandler
    {
        private ConcurrentDictionary<string, User> _userList;
        public UserHandler()
        {
            _userList = new ConcurrentDictionary<string, User>();
        }

        public bool UserExists(string connectionId)
        {
            return _userList.ContainsKey(connectionId);
        }

        public void RemoveUser(string connectionId)
        {
            User u;
            _userList.TryRemove(connectionId, out u);
            u.MyShip.Dispose();
            u.MyShip.Host = null; // Remove linking from the ship
        }

        public void AddUser(User user)
        {
            _userList.TryAdd(user.ConnectionID, user);
        }

        public User GetUser(string connectionId)
        {
            return _userList[connectionId];
        }

        public Ship GetUserShip(string connectionId)
        {
            return _userList[connectionId].MyShip;
        }

        public ICollection<User> GetUsers()
        {
            return _userList.Values;
        }

        public ICollection<string> GetUserConnectionIds()
        {
            return _userList.Keys;
        }
    }
}