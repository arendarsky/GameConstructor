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
        IRepository<Question> _questions;
        IRepository<Answer> _answers;
        IRepository<Influence> _influences;
        IRepository<Effect> _effects;
        IRepository<Characteristic> _characteristics;
        IRepository<Result> _results;
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
                _questions = new FileRepository<Question>(
                    "GameConstructor.Core/Data/Questions.json");
                _answers = new FileRepository<Answer>(
                    "GameConstructor.Core/Data/Answers.json");
                _effects = new FileRepository<Effect>(
                    "GameConstructor.Core/Data/Effects.json");
                _influences = new FileRepository<Influence>(
                    "GameConstructor.Core/Data/Influences.json");
                _characteristics = new FileRepository<Characteristic>(
                    "GameConstructor.Core/Data/Characteristics.json");
                _results = new FileRepository<Result>(
                    "GameConstructor.Core/Data/Results.json");
            }
            else
            {
                _games = new FileRepository<Game>("../../../GameConstructor.Core/Data/Games.json");
                _users = new FileRepository<User>("../../../GameConstructor.Core/Data/Users.json");
                _questions = new FileRepository<Question>(
                    "../../../GameConstructor.Core/Data/Questions.json");
                _answers = new FileRepository<Answer>(
                    "../../../GameConstructor.Core/Data/Answers.json");
                _effects = new FileRepository<Effect>(
                    "../../../GameConstructor.Core/Data/Effects.json");
                _influences = new FileRepository<Influence>(
                    "../../../GameConstructor.Core/Data/Influences.json");
                _characteristics = new FileRepository<Characteristic>(
                    "../../../GameConstructor.Core/Data/Characteristics.json");
                _results = new FileRepository<Result>(
                    "../../../GameConstructor.Core/Data/Results.json");
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
            foreach (var g in _games.Items.Where(g => g.UserId == user.Id))
            {
                user.Games.Add(g);
            }
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
            List<Question> questions = new List<Question>();
            List<Characteristic> characteristics = new List<Characteristic>();
            foreach (var q in _questions.Items.Where(c => c.GameId == _game.Id))
            {
                foreach (var a in _answers.Items.Where(c => c.QuestionId == q.Id))
                {
                    foreach (var e in _effects.Items.Where(c => c.AnswerId == a.Id))
                    {
                        foreach (var i in _influences.Items.Where(c => c.EffectId == e.Id))
                            e.Influences.Add(i);
                        a.Effects.Add(e);
                    }
                    q.Answers.Add(a);
                }
                questions.Add(q);
            }
            foreach (var c in _characteristics.Items.Where(c => c.GameId == _game.Id))
                characteristics.Add(c);
            _game.UpdateQuestions(questions);
            _game.UpdateCharacteristics(characteristics);
            _gameOpened = true;
            return _game;
        }
        public void RemoveGame(IGame game)
        {
            _games.Remove(game as Game);
            _games.Save();
        }
        public void RemoveCharacteristic(Characteristic characteristic)
        {
            _characteristics.Remove(characteristic);
            _characteristics.Save();
        }
        public void RemoveQuestion(Question question)
        {
            _questions.Remove(question);
            _questions.Save();
        }
        public void RemoveAnswer(Answer answer)
        {
            _answers.Remove(answer);
            _answers.Save();
        }
        public void RemoveEffect(Effect effect)
        {
            _effects.Remove(effect);
            _effects.Save();
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
