using LibraryWebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Zheng.Infrastructure.Data;
using Zheng.Infrastructure.Models;

namespace LibraryWebAPI.Services
{
    public class OpenDataService
    {
        private readonly IHttpClientFactory _clientFactory;
        public IEnumerable<OpenDataBook> OpenDataBookList { get; private set; }
        public bool GetOpenDataError { get; private set; }

        private readonly UploadFileService _uploadFileService;
        private readonly LibraryDbContext _context;
        private readonly IUserService _userService;


        public OpenDataService(IHttpClientFactory clientFactory,
            UploadFileService uploadFileService,
            LibraryDbContext context,
            IUserService userService)
        {
            _clientFactory = clientFactory;
            _uploadFileService = uploadFileService;
            _context = context;
            _userService = userService;
        }


        /// <summary>
        /// 從政府資料開放平台，匯入
        /// <para>政府出版品資訊網收錄之出版品書目資料</para>
        /// </summary>
        /// <returns></returns>
        public async Task<List<OpenDataBook>> GetOpenDataBook()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            "https://gpi.culture.tw/download/opendata/2021books.json");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                //反序列化，還原成物件
                OpenDataBookList = await JsonSerializer.DeserializeAsync
                    <List<OpenDataBook>>(responseStream);

                foreach (var book in OpenDataBookList)
                {
                    var strPrice = Regex.Replace(book.price, "[^0-9]", "");
                    int number;
                    bool success = int.TryParse(strPrice, out number);
                    //轉換不成功，價格設定0
                    number = success ? number : 0;

                    //書籍的主鍵guid
                    var id = Guid.NewGuid();
                    var entity = new Book()
                    {
                        Id = id,
                        Title = book.bookTitle,
                        Status = 0,
                        Isbn = book.ISBN,
                        Issn = book.ISSN,
                        Gpn = book.GPN,
                        Publisher = book.publisher,
                        RightCondition = book.RightCondition,
                        Creator = book.creator,
                        PublishDate = book.PublishDate,
                        Edition = book.edition,
                        Cover = book.cover,
                        Classify = book.classify,
                        Gpntype = book.GPNType,
                        Subject = book.subject,
                        Governance = book.governance,
                        Grade = book.grade,
                        Pages = int.Parse(book.pages),
                        Size = book.size,
                        Binding = book.binding,
                        Language = book.language,
                        Introduction = book.@abstract,
                        Catalog = book.Catalog,
                        Price = number,
                        TargetPeople = book.TargetPeople,
                        Types = book.types,
                        Attachment = book.attachment,
                        Url = book.url,
                        Duration = book.duration,
                        Numbers = book.numbers,
                        Restriction = book.restriction,
                        CeasedDate = string.IsNullOrEmpty(book.CeasedDate) ? null : DateTime.Parse(book.CeasedDate),
                        Authority = book.Authority,
                        CreatedAt = DateTime.Now,
                        CreatedBy = _userService.CurrentAccountId,
                        UpdatedAt = DateTime.Now,
                        UpdatedBy = _userService.CurrentAccountId,
                    };

                    await _context.Books.AddAsync(entity);
                   
                    var imgUrl = book.cover;

                    using var httpResponse = await client.GetAsync(imgUrl).ConfigureAwait(false);

                    //如果圖片來源不是404
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        using (var stream = await client.GetStreamAsync(imgUrl).ConfigureAwait(false))
                        {
                            var photoId = await _uploadFileService.AddByStream(stream, imgUrl);

                            entity.BookPhotos.Add(new BookPhoto()
                            {
                                Id = Guid.NewGuid(),
                                UploadFileId = photoId,
                                SystemDate = DateTime.Now,
                            });
                        }
                    }

                    _context.SaveChanges();
                }
            }
            else
            {
                GetOpenDataError = true;
                OpenDataBookList = Array.Empty<OpenDataBook>();
            }

            return (List<OpenDataBook>)OpenDataBookList;
        }
    }


    /// <summary>
    /// 出版書目資料
    /// <para>https://data.gov.tw/dataset/35215</para>
    /// </summary>
    public class OpenDataBook
    {
        /// <summary>
        /// (書名)
        /// </summary>
        public string bookTitle { get; set; }
        /// <summary>
        /// (ISBN)
        /// </summary>
        public string ISBN { get; set; }
        /// <summary>
        /// (ISSN)
        /// </summary>
        public string ISSN { get; set; }
        /// <summary>
        /// (GPN)
        /// </summary>
        public string GPN { get; set; }
        /// <summary>
        /// (出版單位)
        /// </summary>
        public string publisher { get; set; }
        /// <summary>
        /// (出版情況)
        /// </summary>
        public string RightCondition { get; set; }
        /// <summary>
        /// (作者資訊)
        /// </summary>
        public string creator { get; set; }
        /// <summary>
        /// (出版日期)
        /// </summary>
        public string PublishDate { get; set; }
        /// <summary>
        /// (版次)
        /// </summary>
        public string edition { get; set; }
        /// <summary>
        /// (書封連結)
        /// </summary>
        public string cover { get; set; }
        /// <summary>
        /// (書籍分類)
        /// </summary>
        public string classify { get; set; }
        /// <summary>
        /// (出版品分類)
        /// </summary>
        public string GPNType { get; set; }
        /// <summary>
        /// (主題分類)
        /// </summary>
        public string subject { get; set; }
        /// <summary>
        /// (施政分類) 
        /// </summary>
        public string governance { get; set; }
        /// <summary>
        /// (級別) 
        /// </summary>
        public string grade { get; set; }
        /// <summary>
        /// (頁數) 
        /// </summary>
        public string pages { get; set; }
        /// <summary>
        /// (開數)
        /// </summary>
        public string size { get; set; }
        /// <summary>
        /// (裝訂)
        /// </summary>
        public string binding { get; set; }
        /// <summary>
        /// (語言)
        /// </summary>
        public string language { get; set; }
        /// <summary>
        ///  (書籍介紹)
        /// </summary>
        public string @abstract { get; set; }
        /// <summary>
        /// (目次)
        /// </summary>
        public string Catalog { get; set; }
        /// <summary>
        /// (價格)
        /// </summary>
        public string price { get; set; }
        /// <summary>
        /// (適用對象)
        /// </summary>
        public string TargetPeople { get; set; }
        /// <summary>
        /// (資料類型)
        /// </summary>
        public string types { get; set; }
        /// <summary>
        /// (附件) 
        /// </summary>
        public string attachment { get; set; }
        /// <summary>
        /// (出版品網址-線上版或試閱版)
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// (播放時間長度)
        /// </summary>
        public string duration { get; set; }
        /// <summary>
        /// (字號)
        /// </summary>
        public string numbers { get; set; }
        /// <summary>
        /// (權利範圍) 
        /// </summary>
        public string restriction { get; set; }
        /// <summary>
        /// (停刊註記)
        /// </summary>
        public string CeasedDate { get; set; }
        /// <summary>
        /// (授權資訊)
        /// </summary>
        public string Authority { get; set; }
    }
}
