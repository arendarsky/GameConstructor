using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameConstructor.Core.Models;

namespace GameConstructor.Core.Interfaces
{
    public interface IStorage
    {
        IRepository<User> Users { get; }
        IRepository<Game> PlayingGames { get; }
        Game OpenGame(IGame _game);
        void SaveGame(User user, IGame game);
        User LoadUsersGames(User user);
        void SaveUser(User user);
        void RemoveGame(Game game);
        void RemoveCharacteristic(Characteristic characteristic);
        void RemoveQuestion(Question question);
        void RemoveAnswer(Answer answer);
        void RemoveEffect(Effect effect);
        void CloseGame();
    }
}
