using LibraryWebAPI.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Zheng.Infrastructure.Data;
using Zheng.Infrastructure.Models;
using Microsoft.AspNetCore.StaticFiles;


namespace LibraryWebAPI.Services
{
    public class UploadFileService
    {
        private readonly LibraryDbContext _context;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
      
        public UploadFileService(LibraryDbContext context, 
            IUserService userService, 
            IWebHostEnvironment env,
            IConfiguration configuration)
        {
            _context = context;
            _userService = userService;
            _env = env;
            _configuration = configuration;
        }

        public async Task<UploadFile> Get(Guid id)
        {
            return await _context.UploadFiles.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<UploadFile>> Get()
        {
            return await _context.UploadFiles.ToListAsync();
        }

        public async Task<Guid> AddByStream(Stream source, string path)
        {
            //檔名要儲存成guid
            var id = Guid.NewGuid();

            using (var stream = File.Create($"{RootPath}{id}{Path.GetExtension(path)}"))
            {
                //複製檔案
                await source.CopyToAsync(stream);

                #region 取得ContentType

                const string DefaultContentType = "application/octet-stream";

                var provider = new FileExtensionContentTypeProvider();

                if (!provider.TryGetContentType(path, out string contentType))
                {
                    contentType = DefaultContentType;
                }

                #endregion

                var uploadFile = new UploadFile()
                {
                    Id = id,
                    Name = Path.GetFileNameWithoutExtension(path),
                    ContentType = contentType,
                    Extension = Path.GetExtension(path),
                    Length = stream.Length,
                    CreatedAt = DateTime.Now,
                    CreatedBy = _userService.CurrentAccountId,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = _userService.CurrentAccountId,
                };

                _context.UploadFiles.Add(uploadFile);
                _context.SaveChanges();
            }

            return id;
        }

        /// <summary>
        /// 單一檔案上傳
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<bool> Add(IFormFile file)
        {
            ICollection<IFormFile> x = new List<IFormFile>();
            x.Add(file);
            return await AddMultiple(x);
        }

        /// <summary>
        /// 多檔上傳
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public async Task<bool> AddMultiple(ICollection<IFormFile> files)
        {
            List<UploadFile> uploadFiles = new List<UploadFile>();

            //路徑如果不存在，創建目錄起來
            if (!Directory.Exists(RootPath))
                Directory.CreateDirectory(RootPath);

            foreach (var file in files)
            {
                if (file != null && file.Length > 0)
                {
                    //檔名要儲存成guid
                    var id = Guid.NewGuid();

                    using (var stream = File.Create($"{RootPath}{id}{Path.GetExtension(file.FileName)}"))
                    {
                        //複製檔案
                        file.CopyTo(stream);

                        var uploadFile = new UploadFile()
                        {
                            Id = id,
                            Name = Path.GetFileNameWithoutExtension(file.FileName),
                            ContentType = file.ContentType,
                            Extension = Path.GetExtension(file.FileName),
                            Length = file.Length,
                            CreatedAt = DateTime.Now,
                            CreatedBy = _userService.CurrentAccountId,
                            UpdatedAt = DateTime.Now,
                            UpdatedBy = _userService.CurrentAccountId,
                        };

                        uploadFiles.Add(uploadFile);
                    }
                }
            }

            try
            {
                await _context.UploadFiles.AddRangeAsync(uploadFiles);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                //新增失敗
                return false;
            }

            return true;
        }

        public bool AddMultiple(ICollection<IFormFile> files, out List<Guid> guidList)
        {
            List<UploadFile> uploadFiles = new List<UploadFile>();
            guidList = new List<Guid>();

            //路徑如果不存在，創建目錄起來
            if (!Directory.Exists(RootPath))
                Directory.CreateDirectory(RootPath);

            foreach (var file in files)
            {
                if (file != null && file.Length > 0)
                {
                    //檔名要儲存成guid
                    var id = Guid.NewGuid();

                    guidList.Add(id);

                    using (var stream = File.Create($"{RootPath}{id}{Path.GetExtension(file.FileName)}"))
                    {
                        //複製檔案
                        file.CopyTo(stream);

                        var uploadFile = new UploadFile()
                        {
                            Id = id,
                            Name = Path.GetFileNameWithoutExtension(file.FileName),
                            ContentType = file.ContentType,
                            Extension = Path.GetExtension(file.FileName),
                            Length = file.Length,
                            CreatedAt = DateTime.Now,
                            CreatedBy = _userService.CurrentAccountId,
                            UpdatedAt = DateTime.Now,
                            UpdatedBy = _userService.CurrentAccountId,
                        };

                        uploadFiles.Add(uploadFile);
                    }
                }
            }

            try
            {
                _context.UploadFiles.AddRange(uploadFiles);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                //新增失敗
                return false;
            }

            return true;
        }

        public async Task<bool> Check(Guid id)
        {
            return await _context.UploadFiles.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> Exits(string accountId)
        {
            return await _context.Accounts.AnyAsync(x => x.AccountId == accountId);
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var entity = await Get(id);
                //刪除檔案
                File.Delete($"{RootPath}{entity.Id}{entity.Extension}");
                //刪除資料庫的檔案紀錄
                _context.UploadFiles.Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 完整路徑(檔案放置位置)
        /// </summary>
        private string RootPath
        {
            get
            {
                return $"{_env.ContentRootPath}\\{FileDirectoryPath}\\";
            }
        }

        /// <summary>
        /// 存放檔案的資料夾
        /// </summary>
        private string FileDirectoryPath
        {
            get
            {
                return _configuration.GetValue<string>("UploadFilePath"); ;
            }
        } 
    }
}
