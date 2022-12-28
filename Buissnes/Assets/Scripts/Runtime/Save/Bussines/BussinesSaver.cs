using Runtime.Save.Base;
using Runtime.Save.ConcreteModel.Business;

namespace Runtime.Save.Bussines
{
    public class BussinesSaver : Saver<ConcreteBusinessDataModel>
    {
        protected override string DirectoryName => "Bussineses";
        protected override string FileName => "Company";
    }
}