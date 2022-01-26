using System.Threading.Tasks;

namespace Application;

/// <summary>
/// Data Orchestration to request from API and then Persist into database
/// </summary>
public interface IImportBooks
{
    Task Import();
}