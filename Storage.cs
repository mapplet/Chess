using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using System.ComponentModel;
using System.Threading;
using System.Runtime.CompilerServices;

namespace Chess
{
    class Storage : INotifyPropertyChanged
    {
        private Gameboard gameboard = new Gameboard();
        private State currentState;
        private FileSystemWatcher watcher;
        public event PropertyChangedEventHandler propertyChanged;

        public Storage()
        {
            watcher = new FileSystemWatcher();
            watcher.Path = "../Debug";
            watcher.Filter = GameComponents.saveLocation;
            watcher.Changed += new FileSystemEventHandler(OnDatabaseChanged);
            watcher.Created += new FileSystemEventHandler(OnDatabaseChanged);
            watcher.Deleted += new FileSystemEventHandler(OnDatabaseDeleted);
            watcher.Renamed += new RenamedEventHandler(OnDatabaseDeleted);
            watcher.EnableRaisingEvents = true;
            gameboard.propertyChanged += new PropertyChangedEventHandler(OnGameboardChanged);
        }
        
        public void manuallyInitialize(Gameboard gameboard, State state)
        {
            this.gameboard = gameboard;
            gameboard.propertyChanged += new PropertyChangedEventHandler(OnGameboardChanged);
            this.currentState = state;
        }

        private void notifyDatabaseChanged([CallerMemberName] String propertyName = "")
        {
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public FileSystemWatcher getWatcher()
        {
            return this.watcher;
        }

        public void saveGame()
        {
            XDocument savedGame;
            Console.WriteLine("# Saving Game");
            savedGame = new XDocument(new XElement("Chess"));
            serializeAppend(gameboard, savedGame);
            serializeAppend(currentState, savedGame);
            savedGame.Save(GameComponents.saveLocation);
        }

        private void serializeAppend(object obj, XDocument xml)
        {
            using (var writer = xml.Root.CreateWriter())
            {
                var serializer = new XmlSerializer(obj.GetType());
                writer.WriteWhitespace("");
                serializer.Serialize(writer, obj);
                writer.Close();
            }
        }

        private T deserialize<T>(XElement element)
        {
            if (element != null)
            {
                using (var reader = element.CreateReader())
                {
                    var serializer = new XmlSerializer(typeof(T));
                    return (T)serializer.Deserialize(reader);
                }
            }
            else
            {
                Console.WriteLine("# ERROR: XElement was null.");
                return default(T);
            }
        }

        public Tuple<Gameboard, State> LoadGame()
        {
            XDocument loadedGame;
            Console.WriteLine("# Loads XML from file.");
            if (!isFileLocked(GameComponents.saveLocation))
            {
                using (var sr = new StreamReader(GameComponents.saveLocation))
                {
                    loadedGame = XDocument.Load(sr);

                    //gameboard = new Gameboard();
                    gameboard.Clear();

                    XElement parentElem = loadedGame.Root.Element(XName.Get("Gameboard")).Element(XName.Get("blackTeam"));
                    var blackTeam = from list in parentElem.Elements("Piece") select list;
                    foreach (XElement pieceElem in blackTeam)
                    {
                        Piece piece = new Piece(Convert.ToBoolean(pieceElem.Element("empty").Value),
                           Convert.ToInt16(pieceElem.Element("team").Value),
                           Convert.ToInt16(pieceElem.Element("type").Value),
                           Convert.ToInt16(pieceElem.Element("row").Value),
                           Convert.ToInt16(pieceElem.Element("column").Value));
                        gameboard.blackTeam.Add(piece);
                    }

                    parentElem = loadedGame.Root.Element(XName.Get("Gameboard")).Element(XName.Get("whiteTeam"));
                    var whiteTeam = from list in parentElem.Elements("Piece") select list;
                    foreach (XElement pieceElem in whiteTeam)
                    {
                        Piece piece = new Piece(Convert.ToBoolean(pieceElem.Element("empty").Value),
                            Convert.ToInt16(pieceElem.Element("team").Value),
                            Convert.ToInt16(pieceElem.Element("type").Value),
                            Convert.ToInt16(pieceElem.Element("row").Value),
                            Convert.ToInt16(pieceElem.Element("column").Value));
                        gameboard.whiteTeam.Add(piece);
                    }

                    parentElem = loadedGame.Root.Element(XName.Get("Gameboard")).Element(XName.Get("blackDead"));
                    var blackDead = from list in parentElem.Elements("Piece") select list;
                    foreach (XElement pieceElem in blackDead)
                    {
                        Piece piece = new Piece(Convert.ToBoolean(pieceElem.Element("empty").Value),
                            Convert.ToInt16(pieceElem.Element("team").Value),
                            Convert.ToInt16(pieceElem.Element("type").Value),
                            Convert.ToInt16(pieceElem.Element("row").Value),
                            Convert.ToInt16(pieceElem.Element("column").Value));
                        gameboard.blackDead.Add(piece);
                    }

                    parentElem = loadedGame.Root.Element(XName.Get("Gameboard")).Element(XName.Get("whiteDead"));
                    var whiteDead = from list in parentElem.Elements("Piece") select list;
                    foreach (XElement pieceElem in whiteDead)
                    {
                        Piece piece = new Piece(Convert.ToBoolean(pieceElem.Element("empty").Value),
                            Convert.ToInt16(pieceElem.Element("team").Value),
                            Convert.ToInt16(pieceElem.Element("type").Value),
                            Convert.ToInt16(pieceElem.Element("row").Value),
                            Convert.ToInt16(pieceElem.Element("column").Value));
                        gameboard.whiteDead.Add(piece);
                    }

                    currentState = new State();
                    parentElem = loadedGame.Root;
                    var state = from list in parentElem.Elements("State") select list;
                    foreach (XElement stateElem in state)
                    {
                        currentState.myTeam = Convert.ToInt16(stateElem.Element("myTeam").Value);
                    }

                    return new Tuple<Gameboard, State>(gameboard, currentState);
                }
            }
            else
                return null;
        }

        public void OnDatabaseChanged(object source, FileSystemEventArgs e)
        {
            //databaseChanged();
            Console.WriteLine("# Database was changed from file.");
            LoadGame();
            gameboard.updateBoard();
            currentState.updateState(currentState.getMyTeam());
            //gameboard.propertyChanged += new PropertyChangedEventHandler(OnGameboardChanged);
            //updateBoard();
            notifyDatabaseChanged();
        }

        public void OnDatabaseDeleted(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("# Database XML was deleted.");
            gameboard.initialize();
            //updateBoard();
            notifyDatabaseChanged();
        }

        private bool isFileLocked(string pathToFile)
        {
            FileInfo file = new FileInfo(pathToFile);

            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                Console.WriteLine("# File was locked.");
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        public void OnGameboardChanged(object source, PropertyChangedEventArgs e)
        {
            watcher.EnableRaisingEvents = false;
            //gameboardChanged();
            Console.WriteLine("\n# Gameboard was changed in game.");
            saveGame();
            watcher.EnableRaisingEvents = true;
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }
    }
}
