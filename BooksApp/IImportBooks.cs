using System.Threading.Tasks;

namespace Application;

/// <summary>
/// Data Orchestration to request from API and then Persist into database
/// </summary>
internal interface IImportBooks
{
    Task Import();
}