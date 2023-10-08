using elFinder.NetCore;
using elFinder.NetCore.Drivers.FileSystem;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace SWP391.OnlineShop.Portal.Areas.Files.Controllers
{
    [Area("Files")]
    public class FileManagerController : Controller
    {
        private readonly IWebHostEnvironment _env;
        public FileManagerController(
            IWebHostEnvironment env)
        {
            _env = env;
        }

        [Route("/file-manager")]
        public IActionResult Index()
        {
            return View();
        }

        // Url để client-side kết nối đến backend
        // /el-finder-file-system/connector
        [Route("file-manager-connector")]
        public async Task<IActionResult> Connector()
        {
            var connector = GetConnector();
            return await connector.ProcessAsync(Request);
        }

        // Địa chỉ để truy vấn thumbnail
        // /el-finder-file-system/thumb
        [Route("file-manager-thumb/{hash}")]
        public async Task<IActionResult> Thumbs(string hash)
        {
            var connector = GetConnector();
            return await connector.GetThumbnailAsync(HttpContext.Request, HttpContext.Response, hash);
        }

        private Connector GetConnector()
        {
            // Thư mục gốc lưu trữ là wwwwroot/files (đảm bảo có tạo thư mục này)
            var pathRoot = "Uploads";
            var requestUrl = "uploads";

            var driver = new FileSystemDriver();

            var absoluteUrl = UriHelper.BuildAbsolute(Request.Scheme, Request.Host);
            var uri = new Uri(absoluteUrl);

            // /Uploads
            var rootDirectory = Path.Combine(_env.ContentRootPath, pathRoot);
            var url = $"{uri.Scheme}://{uri.Authority}/{requestUrl}/";
            var urlThumb = $"{uri.Scheme}://{uri.Authority}/file-manager-thumb/";


            var root = new RootVolume(rootDirectory, url, urlThumb)
            {
                IsLocked = false, // If locked, files and directories cannot be deleted, renamed or moved
                Alias = "Files", // Beautiful name given to the root/home folder
                MaxUploadSizeInKb = 102400, // Limit imposed to user uploaded file <= 10MB
                ThumbnailSize = 100,
                IsReadOnly = false,
                //UploadAllow = new[] { "application/pdf", "image/jpg", "image/png", "image/jpeg" },
                //LockedFolders = new List<string>(new string[] { "Folder1" }
            };

            driver.AddRoot(root);

            return new Connector(driver)
            {
                // This allows support for the "onlyMimes" option on the client.
                MimeDetect = MimeDetectOption.Internal
            };
        }
    }
}
