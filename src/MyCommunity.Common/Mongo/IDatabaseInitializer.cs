using System.Threading.Tasks;

namespace MyCommunity.Common.Mongo
{
    public interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }
}