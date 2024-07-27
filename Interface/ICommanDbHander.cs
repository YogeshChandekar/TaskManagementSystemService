using log4net;
using System.Net;
using TaskManagementService.Models;
namespace TaskManagementService.Interface
{
    public interface ICommanDbHander
    {
        Task<List<T>> GetData<T>(string? query, ILog log, string succMsg, string? errMsg, string? errorCode, string? txn, object? param = null, string? logType = null);

        Task<T> GetSingleData<T>(string? query, ILog log, string succMsg, string? errMsg, string? errorCode, string? txn, object? param = null, string? logType = null);

        Task<BaseResponse> AddUpdateDeleteData(string? query, ILog log, string? succMsg, string? errMsg, string? duplicateRecordError, string? errorCode, string? txn, object? param = null, string? logType = null);

        Task<BaseResponse> AddDataReturnLatestId(string? query, ILog log, string? succMsg, string? errMsg, string? duplicateRecordError, string? errorCode, string? txn, object? param = null, string? logType = null);
    }
}
