using LibraryWebAPI.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Zheng.Infra.Data.Data;
using Zheng.Infra.Data.Models;


namespace LibraryWebAPI.Services
{
    public class FileService
    {
        private readonly LibraryDbContext _context;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public FileService(LibraryDbContext context,
            IUserService userService,
            IWebHostEnvironment env,
            IConfiguration configuration)
        {
            _context = context;
            _userService = userService;
            _env = env;
            _configuration = configuration;
        }

        /// <summary>
        /// 多檔案下載，壓縮為一個zip
        /// </summary>
        /// <param name="guids"></param>
        /// <returns></returns>
        public (string fileType, byte[] archiveData, string achiveName) GetFilesToZip(List<string> guids)
        {
            //參考 https://www.youtube.com/watch?v=mecsXGuPoqk
            var fileName = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}.zip";

            var filesPath = Directory.GetFiles(RootPath, "*.*", SearchOption.AllDirectories)
                .Where(x => guids.Contains(Path.GetFileNameWithoutExtension(x)));

            using (var memoryStream = new MemoryStream())
            {
                using (var achive = new ZipArchive(memoryStream, ZipArchiveMode.Create))
                {
                    foreach (var file in filesPath)
                    {
                        achive.CreateEntryFromFile(file, Path.GetFileName(file));
                    }
                }

                return ("application/zip", memoryStream.ToArray(), fileName);
            }
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
