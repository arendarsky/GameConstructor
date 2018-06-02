using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameConstructor.Core.Interfaces;
using GameConstructor.Core.Models;

namespace GameConstructor.Core.DataStorages
{
    internal class FileStorage : IStorage
    {
        IRepository<Game> _playingGames;
        IRepository<User> _users;
        bool _forDb;
        bool _gameOpened;


        public FileStorage(bool forDb)
        {
            _forDb = forDb;
            Load();
        }


        private void Load()
        {
            if (_forDb)
            {
                _users = new FileRepository<User>("../GameConstructor.Core/Data/Users.json");

            }

            else
            {
                _users = new FileRepository<User>("../../../GameConstructor.Core/Data/Users.json");
            }

            try
            {
                foreach (var u in _users.Items)
                    foreach (var g in u.Games)
                        foreach (var c in g.Characteristics)
                            foreach (var i in c.Influences)
                                i.Characteristic = c;
                    
            }

            catch
            {

            }
        }


        public IRepository<User> Users => _users;
         
        public IRepository<Game> PlayableGames
        {
            get
            {
                List<Game> playingGames = new List<Game>();
                foreach (var u in _users.Items)
                    playingGames.AddRange(u.Games.Where(g => g.DisplayingInGameMode == true));
                _playingGames = new DatabaseRepository<Game>(playingGames);
                return _playingGames;
            }
        }


        public User LoadUsersGames(User user)
        {
            return user;
        }
        public void LoadToDatabase()
        {
            using(Context context = new Context())
            {
                User user;
                foreach (var u in _users.Items)
                {
                    try
                    {
                        user = context.Users.First(us => us.Login == u.Login);
                        context.Users.Remove(user);
                    }
                    catch
                    {

                    }
                    context.Users.Add(u);
                }
                    
                    
                context.SaveChanges();
            }
        }
        public void Synchronize()
        {
            using (Context context = new Context())
            {
                IRepository<User> usersFromDatabase = new DatabaseRepository<User>(context.Users.ToList());
                foreach (var u in usersFromDatabase.Items)
                {
                    foreach (var g in u.Games)
                    {
                        foreach (var c in g.Characteristics)
                            context.Entry(c).Collection(ch => ch.Influences).Load();
                        foreach (var q in g.Questions)
                        {
                            foreach (var a in q.Answers)
                            {
                                foreach (var e in a.Effects)
                                {
                                    foreach (var i in e.Influences)
                                        context.Entry(i).Reference(inf => inf.Characteristic).Load();
                                    context.Entry(e).Collection(ef => ef.Influences).Load();
                                }
                                context.Entry(a).Collection(an => an.Effects).Load();
                            }
                            context.Entry(q).Collection(qu => qu.Answers).Load();
                        }
                        context.Entry(g).Collection(gm => gm.Questions).Load();
                        context.Entry(g).Reference(gm => gm.Picture).Load();
                        context.Entry(g).Collection(gm => gm.Results).Load();
                    }
                }
                foreach (var u in usersFromDatabase.Items)
                {
                    if (_users.Items.FirstOrDefault(us => us.Login == u.Login) == null)
                        _users.Add(u);
                    else
                    {
                        User user = _users.Items.First(us => us.Login == u.Login);
                        _users.Remove(user);
                        _users.Add(u);
                    }
                }

                _users.Save();
            }
        }


        public void SaveGame(User user, IGame game)
        {
            if (!_gameOpened)
            {
                user.Games.Add(game as Game);
                _users.Save();
            }

            else
            {
                _users.Save();
            }

            _gameOpened = false;
        }

        public void CloseGame()
        {
            _users = new FileRepository<User>("../../../GameConstructor.Core/Data/Users.json");
            _gameOpened = false;
        }

        public void SaveUser(User user)
        {
            _users.Add(user);
            _users.Save();
        }

        public IGame OpenGame(IGame _game)
        {
            _gameOpened = true;
            return _game;
        }
   
        public void RemoveGame(IGame game)
        {
            User user = _users.Items.First(u => u.Games.FirstOrDefault(g => g.Name == game.Name) != null);
            Game Game = user.Games.First(g => g.Name == game.Name);
            user.Games.Remove(Game);
            _users.Save();
        }

        public void RemoveCharacteristic(Characteristic characteristic)
        {

        }

        public void RemoveQuestion(Question question)
        {

        }

        public void RemoveAnswer(Answer answer)
        {

        }

        public void RemoveEffect(Effect effect)
        {

        }
    }



    internal class DatabaseStorage: IStorage
    {
        IRepository<Game> _playingGames;
        IRepository<User> _users;
        bool _gameOpened;
        Context _context;


        public IRepository<User> Users
        {
            get
            {
                using(_context = new Context())
                {
                    _users = new DatabaseRepository<User>(_context.Users.ToList());
                    return _users;
                }
            }
        }
       
        public IRepository<Game> PlayableGames
        {
            get
            {
                using(_context = new Context())
                {
                    _playingGames = new DatabaseRepository<Game>(_context.Games.Where(
                        g => g.DisplayingInGameMode == true).ToList());
                    return _playingGames;
                }
            }
        }


        public void CloseGame()
        {
            _context.Dispose();
            _gameOpened = false;
        }

        public void SaveGame(User user, IGame game)
        {
            if (_gameOpened)
            {
                _context.SaveChanges();
                _context.Dispose();
                _gameOpened = false;
            }

            else
            {
                using (_context = new Context())
                {
                    _context.Users.First(u => u.Id == user.Id).Games.Add(game as Game);
                    _context.SaveChanges();
                }
            }
          
        }


        public void SaveUser(User user)
        {
            using(_context = new Context())
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
        }


        public IGame OpenGame(IGame _game)
        {
            _context = new Context();
            IGame game = _context.Games.First(g => g.Id == _game.Id);
            _gameOpened = true;
            return game;
        }


        public User LoadUsersGames(User user)
        {
            using(_context = new Context())
            {
                User _user = _context.Users.First(u => u.Id == user.Id);
                _context.Entry(_user).Collection("Games").Load();
                foreach (var g in _user.Games)
                    _context.Entry(g).Reference("Picture").Load();
                return _user;
            }
        }

        public void RemoveGame(IGame game)
        {
            using (_context = new Context())
            {
                Game Game = _context.Games.First(g => g.Id == game.Id);
                _context.Games.Remove(Game);
                if (Game.Picture != null)
                {
                    Picture picture = _context.Pictures.First(p => p.Id == Game.Picture.Id);
                    _context.Pictures.Remove(picture);
                }
                _context.SaveChanges();
            }
        }

        public void RemoveCharacteristic(Characteristic characteristic)
        {
            try
            {
                characteristic = _context.Characteristics.First(
                    c => c.Id == characteristic.Id);
                _context.Characteristics.Remove(characteristic);
            }

            catch
            {
                return;
            }
        }

        public void RemoveQuestion(Question question)
        {
            question = _context.Questions.First(
                q => q.Id == question.Id);
            _context.Questions.Remove(question);
        }

        public void RemoveAnswer(Answer answer)
        {
            answer = _context.Answers.First(
                c => c.Id == answer.Id);
            _context.Answers.Remove(answer);
        }

        public void RemoveEffect(Effect effect)
        {
            effect = _context.Effects.First(
                c => c.Id == effect.Id);
            _context.Effects.Remove(effect);
        }
    }    
}
