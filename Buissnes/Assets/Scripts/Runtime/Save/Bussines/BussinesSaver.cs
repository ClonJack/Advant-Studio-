using Runtime.Save.Base;
using Runtime.Save.ConcreteModel.Business;

namespace Runtime.Save.Bussines
{
    public class BussinesSaver : Saver<ConcreteBusinessDataModel>
    {
        public override string DirectoryName => "Companies";
        public override string FileName => "Company";
    }
}