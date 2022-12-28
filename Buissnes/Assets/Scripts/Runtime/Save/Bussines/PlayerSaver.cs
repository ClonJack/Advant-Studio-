using Runtime.Save.Base;
using Runtime.Save.ConcreteModel.Player;

namespace Runtime.Save.Bussines
{
    public class PlayerSaver : Saver<ConcretePlayerModel>
    {
        protected override string DirectoryName => "User";
        protected override string FileName => "Player";
    }
}