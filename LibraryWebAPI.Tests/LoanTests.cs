using LibraryWebAPI.Dtos.AccountDto;
using LibraryWebAPI.Dtos.LoanDto;
using LibraryWebAPI.Interfaces;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWebAPI.Tests
{
    [TestFixture]
    public class LoanTests
    {
        #region Variables

        private Mock<ILoanService> _loanService;

        #endregion

        [SetUp]
        public void Setup()
        {
            _loanService = new Mock<ILoanService>();
            // Arragne: 測試物件的初始化、定義需要使用的參數資訊

            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44323/api/Account/Login");

            //建立 HttpClient
            HttpClient client = new HttpClient() { BaseAddress = new Uri("https://localhost:44323/api/Account/Login") };
            // 準備寫入的 data
            Account_LoginDto postData = new Account_LoginDto() { UserId = "zheng", Password = "123456" };
            // 將 data 轉為 json
            string json = JsonConvert.SerializeObject(postData);
            // 將轉為 string 的 json 依編碼並指定 content type 存為 httpcontent
            HttpContent contentPost = new StringContent(json, Encoding.UTF8, "application/json");
            // 發出 post 並取得結果
            HttpResponseMessage response = client.PostAsync("test", contentPost).GetAwaiter().GetResult();
            
        }

        [Test]
        public async Task Test_新增借閱Async()
        {
            // Art: 呼叫被測試的方法
            var loan_PostDto = new Loan_PostDto()
            {
                AccountId = new Guid("E4961475-3DD3-43D8-8EE3-21AE51E6EFFD"),
                BookId = new Guid("2B380CAE-9FDD-42D7-898A-0003DA362C21"),
                IssueDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(10),
                ReturnDate = null,
            };

            //var loan = await _loanService.Add(loan_PostDto);
            var loan = await _loanService.Object.Get(new Guid("8D85A02D-9148-41C5-AD2E-78F4E096E8E7"));

            var _compare = await _loanService.Object.GetLast();
            // Assert: 驗證結果
            Assert.That(loan, Is.EqualTo(_compare));
        }
    }
}
