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
        IRepository<Game> _games;
        IRepository<User> _users;
        IRepository<Question> _questions;
        IRepository<Answer> _answers;
        IRepository<Influence> _influences;
        IRepository<Effect> _effects;
        IRepository<Picture> _pictures;
        IRepository<Characteristic> _characteristics;
        bool _loaded;
        bool ForDb;
        public FileStorage(bool forDb)
        {
            ForDb = forDb;
        }
        public IRepository<Game> Games
        {
            get
            {
                Load();
                return _games;
            }
        }
        public IRepository<User> Users
        {
            get
            {
                Load();
                return _users;
            }
        }
        private void Load()
        {
            if (_loaded)
                return;
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
                _pictures = new FileRepository<Picture>(
                    "GameConstructor.Core/Data/Pictures.json");
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
                _pictures = new FileRepository<Picture>(
                    "../../../GameConstructor.Core/Data/Pictures.json");
            }
            foreach (var i in _influences.Items)
                i.Characteristic = _characteristics.Items.First(c => c.Id == i.CharacteristicId);
            foreach (var e in _effects.Items)
                e.Influences = _influences.Items.Where(i => i.EffectId == e.Id).ToList();
            foreach (var a in _answers.Items)
                a.Effects = _effects.Items.Where(e => e.AnswerId == a.Id).ToList();
            foreach (var q in _questions.Items)
                q.Answers = _answers.Items.Where(a => a.QuestionId == q.Id).ToList();
            foreach (var g in _games.Items)
            {
                g.Questions = _questions.Items.Where(q => q.GameId == g.Id).ToList();
                g.Picture = _pictures.Items.First(p => p.Id == g.PictureId);
                g.Characteristics = _characteristics.Items.Where(c => c.GameId == g.Id).ToList();
            }
            foreach (var u in _users.Items)
                u.Games = _games.Items.Where(g => g.UserId == u.Id).ToList();
            _loaded = true;
        }
        public void SaveAll()
        {
            
        }
    }
    
}
