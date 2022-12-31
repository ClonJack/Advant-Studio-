using Runtime.Save.Base;
using Runtime.Save.ConcreteModel.Player;

namespace Runtime.Save.Bussines
{
    public class PlayerSaver : Saver<ConcretePlayerModel>
    {
        public override string DirectoryName => "User";
        public override string FileName => "Player";
    }
}