using LibraryWebAPI.Dtos.RefreshToken;
using LibraryWebAPI.Dtos.Responses;
using System.Threading.Tasks;
using Zheng.Infra.Data.Models;

namespace LibraryWebAPI.Interfaces
{
    public interface ITokenService
    {
        /// <summary>
        /// 新增token紀錄
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<CommonResponse> AddRefreshToken(RefreshToken_PostDto entity);

        /// <summary>
        /// 更新token紀錄
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<CommonResponse> UpdateRefreshToken(RefreshToken_PostDto entity);
    }
}
