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
        User _user;
        bool _gameOpened;

        public FileStorage(User user)
        {
            _user = user;
            Load();
        }
        private void Load()
        {

            _users = new FileRepository<User>($"../../../GameConstructor.Core/Data/{_user.Login}.json");

            try
            {
                foreach (var u in _users.Items)
                    foreach (var g in u.Games)
                        foreach (var c in g.Characteristics)
                            foreach (var i in c.Influences)
                            {
                                i.Characteristic = c;
                                foreach (var q in g.Questions)
                                    foreach (var a in q.Answers)
                                        a.Effects.First(e => e.Id == i.EffectId).Influences.Add(i);
                            }


            }

            catch
            {

            }
            _user = _users.Items.First();
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
                try
                {
                    user = context.Users.First(us => us.Login == _user.Login);
                    context.Users.Remove(user);
                }
                catch
                {

                }
                context.Users.Add(_user);                                        
                context.SaveChanges();
            }
        }
        public void LoadToFile()
        {
            using (Context context = new Context())
            {
                User user = context.Users.First(u => u.Login == _user.Login);
                foreach (var g in user.Games)
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
                _user = _users.Items.First(u => u.Login == _user.Login);
                _users.Remove(_user);
                _users.Add(user);
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
        public void RemoveResult(Result result)
        {

        }
        public void RemoveCondition(Condition condition)
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
                    _playingGames = new DatabaseRepository<Game>(_context.Games
                        .Include("Picture").ToList());
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
                User _user = _context.Users.First(u => u.Login == user.Login);
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
                foreach (var i in _context.Influences.Where(
                    inf => inf.Characteristic.Id == characteristic.Id))
                    _context.Influences.Remove(i);
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
            try
            {
                question = _context.Questions.First(
                    q => q.Id == question.Id);
                _context.Questions.Remove(question);
            }
            catch
            {

            }
        }

        public void RemoveAnswer(Answer answer)
        {
            try
            {
                answer = _context.Answers.First(
                    c => c.Id == answer.Id);
                _context.Answers.Remove(answer);
            }
            catch
            {

            }
        }

        public void RemoveEffect(Effect effect)
        {
            try
            {
                effect = _context.Effects.First(
                    c => c.Id == effect.Id);
                _context.Effects.Remove(effect);
            }
            catch
            {

            }
        }
        public void RemoveResult(Result result)
        {
            try
            {
                result = _context.Results.First(
                    c => c.Id == result.Id);
                _context.Results.Remove(result);
                
            }
            catch
            {

            }
        }
        public void RemoveCondition(Condition condition)
        {
            try
            {
                condition = _context.Conditions.First(c => c.Id == condition.Id);
                _context.Conditions.Remove(condition);
            }
            catch
            {

            }
        }
    }    
}
