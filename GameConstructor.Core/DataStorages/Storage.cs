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
    internal class FileStorage: IStorage
    {
        IRepository<Game> _games;
        IRepository<Game> _playingGames;
        IRepository<User> _users;
        bool ForDb;
        bool _gameOpened;
        public FileStorage(bool forDb)
        {
            ForDb = forDb;
            Load();
        }
        private void Load()
        {
            if (ForDb)
            {
                _games = new FileRepository<Game>("GameConstructor.Core/Data/Games.json");
                _users = new FileRepository<User>("GameConstructor.Core/Data/Users.json");

            }
            else
            {
                _games = new FileRepository<Game>("../../../GameConstructor.Core/Data/Games.json");
                _users = new FileRepository<User>("../../../GameConstructor.Core/Data/Users.json");
            }
            try
            {
                foreach (var g in _games.Items)
                    foreach (var c in g.Characteristics)
                        foreach (var i in c.Influences)
                            i.Characteristic = c;
            }
            catch
            {

            }
        }
        public IRepository<User> Users
        {
            get
            {
                return _users;
            }
        }
        public IRepository<Game> PlayingGames
        {
            get
            {
                _playingGames = new DatabaseRepository<Game>(
                    _games.Items.Where(g => g.DisplayingInGameMode == true).ToList());
                return _playingGames;
            }
        }
        public User LoadUsersGames(User user)
        {
            List<Game> games = new List<Game>();
            foreach (var g in _games.Items.Where(g => g.UserId == user.Id))
            {
                games.Add(g);
            }
            user.Games = games;
            return user;
        }
        public void SaveGame(User user, IGame game)
        {
            if (!_gameOpened)
            {
                _games.Add(game as Game);
                _games.Save();
            }
            else
            {
                _games.Save();
            }
            _gameOpened = false;
        }
        public void CloseGame()
        {
            _games = new FileRepository<Game>("../../../GameConstructor.Core/Data/Games.json");
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
            Game Game = _games.Items.First(g => g.Name == game.Name);
            _games.Remove(Game);
            _games.Save();
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
        Context context;
        public IRepository<User> Users
        {
            get
            {
                using(context = new Context())
                {
                    _users = new DatabaseRepository<User>(context.Users.ToList());
                    return _users;
                }
            }
        }
       
        public IRepository<Game> PlayingGames
        {
            get
            {
                using(context = new Context())
                {
                    _playingGames = new DatabaseRepository<Game>(context.Games.Where(
                        g => g.DisplayingInGameMode == true).ToList());
                    return _playingGames;
                }
            }
        }
        public void CloseGame()
        {
            context.Dispose();
            _gameOpened = false;
        }
        public void SaveGame(User user, IGame game)
        {
            if (_gameOpened)
            {
                context.SaveChanges();
                context.Dispose();
                _gameOpened = false;
            }
            else
            {
                using (context = new Context())
                {
                    context.Users.First(u => u.Id == user.Id).Games.Add(game as Game);
                    context.SaveChanges();
                }
            }
          
        }
        public void SaveUser(User user)
        {
            using(context = new Context())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
        public IGame OpenGame(IGame _game)
        {
            context = new Context();
            IGame game = context.Games.First(g => g.Id == _game.Id);
            _gameOpened = true;
            return game;
        }
        public User LoadUsersGames(User user)
        {
            using(context = new Context())
            {
                User _user = context.Users.First(u => u.Id == user.Id);
                context.Entry(_user).Collection("Games").Load();
                foreach (var g in _user.Games)
                    context.Entry(g).Reference("Picture").Load();
                return _user;
            }
        }
        public void RemoveGame(IGame game)
        {
            using (context = new Context())
            {
                Game Game = context.Games.First(g => g.Id == game.Id);
                context.Games.Remove(Game);
                if (Game.Picture != null)
                {
                    Picture picture = context.Pictures.First(p => p.Id == Game.Picture.Id);
                    context.Pictures.Remove(picture);
                }
                context.SaveChanges();
            }
        }
        public void RemoveCharacteristic(Characteristic characteristic)
        {
            try
            {
                characteristic = context.Characteristics.First(
                    c => c.Id == characteristic.Id);
                context.Characteristics.Remove(characteristic);
            }
            catch
            {
                return;
            }
        }
        public void RemoveQuestion(Question question)
        {
            question = context.Questions.First(
                q => q.Id == question.Id);
            context.Questions.Remove(question);
        }
        public void RemoveAnswer(Answer answer)
        {
            answer = context.Answers.First(
                c => c.Id == answer.Id);
            context.Answers.Remove(answer);
        }
        public void RemoveEffect(Effect effect)
        {
            effect = context.Effects.First(
                c => c.Id == effect.Id);
            context.Effects.Remove(effect);
        }
    }
    
}
