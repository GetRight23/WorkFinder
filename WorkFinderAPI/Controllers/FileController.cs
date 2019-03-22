using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DatabaseDao;
using JsonConvertor;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WorkFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
		private HostingEnvironment environment;
		private Storage storage;
		private JsonConvertorEngine jsonConvertor;

		public FileController(Storage storage, JsonConvertorEngine jsonConvertor, HostingEnvironment environment)
		{
			this.storage = storage;
			this.jsonConvertor = jsonConvertor;
			this.environment = environment;
		}

		[HttpPost]
		public async void AddFile(IFormFile uploadedFile)
		{
			if (uploadedFile != null)
			{
				byte[] array = new byte[uploadedFile.Length];
				using (var ms = new MemoryStream())
				{
					await uploadedFile.CopyToAsync(ms);
					var fileBytes = ms.ToArray();
					string s = Convert.ToBase64String(fileBytes);
					User user = storage.UserDao.selectEntityById(1);
					Photo photo = new Photo { Data = fileBytes, IdUser = user.Id };
					storage.PhotoDao.insertEntity(photo);
				}
			}
		}
    }
}
