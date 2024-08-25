using AlFikr.FrontendUI.Entities;
using AlFikr.FrontendUI.Entities.Exceptions;
using AlFikr.FrontendUI.Web.HttpClients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class ThesisModel : PageModel
{
	private readonly IThesisServiceApiClient thesisServiceApiClient;
	private readonly IWebHostEnvironment _hostEnvironment;
	private readonly ILogger<ThesisModel> _logger;
	private const int ChunkSize = 1024 * 1024; // 1MB

	public ThesisModel(IThesisServiceApiClient thesisServiceApiClient, IWebHostEnvironment hostEnvironment, ILogger<ThesisModel> logger)
	{
		this.thesisServiceApiClient = thesisServiceApiClient;
		_hostEnvironment = hostEnvironment;
		_logger = logger;
	}

	[BindProperty]
	public ThesisEntity Thesis { get; set; }
	[BindProperty]
	public IFormFile attachmentDocument { get; set; }
	[BindProperty]
	public IFormFile attachmentCoverPage { get; set; }
	[BindProperty]
	public int Id { get; set; }
	[BindProperty]
	public int IdAuthor { get; set; }
	[BindProperty]
	public int IdSecondAuthor { get; set; }
	[BindProperty]
	public int IdTheme { get; set; }
	[BindProperty]
	public int IdCollection { get; set; }
	[BindProperty]
	public List<AuthorEntity> AllAuthors { get; set; }
	[BindProperty]
	public List<ThemeEntity> AllThemes { get; set; }
	[BindProperty]
	public List<CollectionEntity> AllCollections { get; set; }
	[BindProperty]
	public List<ReviewerEntity> AllReviewers { get; set; }


	[BindProperty]
	public string supervisors { get; set; }
	[BindProperty]
	public string reviewers { get; set; }
	public List<SupervisorEntity> supervisorList { get; set; }
	public List<KeyValuePair<int, string>> ReviewerIds { get; set; }

	public void OnGet(int id)
	{
		if (id != 0)
		{
			Thesis = thesisServiceApiClient.GetThesis(id).Result;

		}
		AllAuthors = thesisServiceApiClient.GetAuthorsAsync().Result.ToList();
		AllThemes = thesisServiceApiClient.GetThemesAsync().Result.ToList();
		AllCollections = thesisServiceApiClient.GetCollectionsAsync().Result.ToList();
		AllReviewers = thesisServiceApiClient.GetReviewersAsync().Result.ToList();
	}

	public async Task<IActionResult> OnPostAsync()
	{
		try
		{
			Thesis.ThemesIds = new string[] { IdTheme.ToString() };
			Thesis.IdCollection = IdCollection;
			Thesis.MainAuthorsIds = new string[] { IdAuthor.ToString() };
			Thesis.SecondaryAuthorsIds = new string[] { IdSecondAuthor.ToString() };

			if (supervisorList == null)
			{
				supervisorList = new List<SupervisorEntity>();
			}
			string[] SupervisorsSegments = supervisors.Split('|');
			foreach (var segment in SupervisorsSegments)
			{
				string[] parts = segment.Split('-');
				if (parts.Length > 2)
				{
					string name = parts[0].Trim();
					string role = parts[1].Trim();
					string title = parts[2].Trim();
					SupervisorEntity supervisor = new SupervisorEntity
					{
						SupervisorName = name,
						SupervisorRole = role,
						SupervisorTitle = title
					};
					supervisorList.Add(supervisor);
				}
			}
			Thesis.SupervisorList = supervisorList;

			if (ReviewerIds == null)
			{
				ReviewerIds = new List<KeyValuePair<int, string>>();
			}
			string[] ReviewerSegments = reviewers.Split('|');
			foreach (var segment in ReviewerSegments)
			{
				string[] parts = segment.Split('-');
				if (parts.Length > 2)
				{
					string IdString = parts[0].Trim();
					int id = int.Parse(IdString);

					string role = parts[2].Trim();

					ReviewerIds.Add(new KeyValuePair<int, string>(id, role));
				}
			}
			Thesis.ReviewerIds = ReviewerIds;



			Thesis.CataloguesIds = new string[] { "1" };
			Thesis.IdEditor = 1;

			var response = await thesisServiceApiClient.UpsertThesisAsync(Thesis);

			if (response.IsSuccessStatusCode && (attachmentCoverPage != null || attachmentDocument != null))
			{
				await UploadThesisAttachments(response);
			}
		}
		catch (ApiException ex)
		{
			_logger.LogError(ex.Message);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);
		}

		return RedirectToPage("Index");
	}

	private async Task UploadThesisAttachments(HttpResponseMessage response)
	{
		var res = await response.Content.ReadAsStringAsync();

		if (int.TryParse(res, out int thesisId))
		{
			res = thesisId.ToString();
		}


		if (attachmentCoverPage != null)
		{
			var photoFileName = $"{res}.jpg";
			var uploads = Path.Combine(_hostEnvironment.WebRootPath, "images", "coverPage");
			var filePath = Path.Combine(uploads, photoFileName);

			using (var fileStream = new FileStream(filePath, FileMode.Create))
			{
				await attachmentCoverPage.CopyToAsync(fileStream);
			}

			Thesis.Id = int.Parse(res);
			Thesis.CoverPage = photoFileName;
		}

		if (attachmentDocument != null)
		{
			var folderPath = Path.Combine(_hostEnvironment.WebRootPath, "images", "Thesis");
			var chunkPaths = await SlicePdfIntoChunksAsync(attachmentDocument, folderPath);
			var docPath = Path.Combine(folderPath, $"{res}.pdf");

			await RegroupChunksIntoFileAsync(chunkPaths, docPath);

			var directoryInfo = new DirectoryInfo(folderPath);
			foreach (var file in directoryInfo.EnumerateFiles("chunk*.txt"))
			{
				file.Delete();
			}

			Thesis.FileDocument = $"{res}.pdf";
		}

		response = await thesisServiceApiClient.UpsertThesisAsync(Thesis);

		if (!response.IsSuccessStatusCode)
		{
			var errorMessage = await response.Content.ReadAsStringAsync();
			throw new ApiException(errorMessage);
		}
	}

	public async Task<List<string>> SlicePdfIntoChunksAsync(IFormFile file, string folderPath)
	{
		var chunkPaths = new List<string>();
		var chunkIndex = 0;

		using (var stream = file.OpenReadStream())
		{
			while (stream.Position < stream.Length)
			{
				var remainingBytes = (int)(stream.Length - stream.Position);
				var bufferSize = Math.Min(remainingBytes, ChunkSize);
				var buffer = new byte[bufferSize];

				await stream.ReadAsync(buffer, 0, bufferSize);

				var chunkPath = Path.Combine(folderPath, $"chunk{chunkIndex}.pdf");
				await WriteBytesToFileAsync(chunkPath, buffer);

				chunkPaths.Add(chunkPath);
				chunkIndex++;
			}
		}

		return chunkPaths;
	}

	public async Task WriteBytesToFileAsync(string filePath, byte[] bytes)
	{
		using (var stream = new FileStream(filePath, FileMode.Create))
		{
			await stream.WriteAsync(bytes, 0, bytes.Length);
		}
	}

	public async Task RegroupChunksIntoFileAsync(List<string> chunkPaths, string outputPath)
	{
		if (chunkPaths.Count == 0)
		{
			throw new ArgumentException("No chunks to regroup.");
		}

		using (var outputStream = new FileStream(outputPath, FileMode.Create))
		{
			foreach (var chunkPath in chunkPaths.OrderBy(GetChunkIndex))
			{
				var chunkData = await ReadBytesFromFileAsync(chunkPath);
				await outputStream.WriteAsync(chunkData, 0, chunkData.Length);
			}
		}
	}

	public async Task<byte[]> ReadBytesFromFileAsync(string filePath)
	{
		using (var stream = new FileStream(filePath, FileMode.Open))
		{
			var buffer = new byte[stream.Length];
			await stream.ReadAsync(buffer, 0, (int)stream.Length);
			return buffer;
		}
	}

	public int GetChunkIndex(string chunkPath)
	{
		var fileName = Path.GetFileNameWithoutExtension(chunkPath);
		var indexString = fileName.Substring(5); // Assuming the format is "chunkX.pdf"
		return int.Parse(indexString);
	}
}
