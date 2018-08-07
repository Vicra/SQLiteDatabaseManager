using System;
using System.Collections.Generic;
using System.IO;

namespace SQLite_DataBaseManager
{
    public class FileConnections
    {
        private List<DatabaseConnection> _connections;
        private string fileLocation = Directory.GetCurrentDirectory() + "\\fileConnections.csv";

        public FileConnections()
        {
            _connections = getConnections();
        }

        public void addConnection(string name, string path, string password)
        {
            if (!connectionExists(name, path))
            {
                _connections.Add(new DatabaseConnection()
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Path = path,
                    Password = password
                });
                setConnections();
            }
            else
            {
                throw new Exception("Connection already exists.");
            }
        }

        public void addConnection(string name, string path)
        {
            if (!connectionExists(name, path))
            {
                _connections.Add(new DatabaseConnection()
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Path = path,
                    Password = ""
                });
                setConnections();
            }
            else
            {
                throw new Exception("Connection already exists.");
            }
        }

        private bool connectionExists(string name, string path)
        {
            foreach (var connection in _connections)
            {
                if (connection.Name == name && connection.Path == path)
                {
                    return true;
                }
            }
            return false;
        }

        public void editConnection(Guid id, string name, string path, string password)
        {
            foreach (var connection in _connections)
            {
                if (connection.Id == id)
                {
                    connection.Name = name;
                    connection.Path = path;
                    connection.Password = password;
                    break;
                }
            }
        }

        public bool removeConnection(Guid id)
        {
            int itemToRemove = -1;
            for (int i = 0; i < _connections.Count; i++)
            {
                if (_connections[i].Id == id)
                {
                    itemToRemove = i;
                    break;
                }
            }
            if (itemToRemove != -1)
            {
                _connections.RemoveAt(itemToRemove);
                return true;
            }
            return false;
        }

        public List<DatabaseConnection> getConnections()
        {
            var returnConnections = new List<DatabaseConnection>();
            if (File.Exists(fileLocation))
            {
                string[] lines = File.ReadAllLines(fileLocation);
                foreach (var line in lines)
                {
                    string[] connectionProperties = line.Split(',');
                    if (connectionProperties.Length > 1)
                    {
                        DatabaseConnection dbConnection = new DatabaseConnection();
                        dbConnection.Name = connectionProperties[0];
                        dbConnection.Path = connectionProperties[1];
                        dbConnection.Password = connectionProperties[2];
                        returnConnections.Add(dbConnection);
                    }
                }
            }
            return returnConnections;
        }

        //Write-append to a CSV File
        public bool saveConnection(string name, string path)
        {
            if (!File.Exists(fileLocation))
            {
                File.WriteAllText(fileLocation, name + "," + path + Environment.NewLine);
            }
            else
            {
                File.AppendAllText(fileLocation, name + "," + path + Environment.NewLine);
            }
            return true;
        }

        public void setConnections()
        {
            File.WriteAllText(fileLocation, "");
            foreach (var connection in _connections)
            {
                File.AppendAllText(fileLocation, 
                    connection.Name + "," + connection.Path + "," + connection.Password + Environment.NewLine);
            }
        }
    }

    public class DatabaseConnection
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Password { get; set; }
    }
}
