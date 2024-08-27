using AlFikr.ThesisService.Data.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace AlFikr.ThesisService.Business;

public class ServiceBase<T>
{
	protected readonly AlFikrContext alFikrContext;
	protected readonly ILogger<T> _logger;

	public ServiceBase(AlFikrContext alFikrContext)
	{
		this.alFikrContext = alFikrContext;
	}

	public ServiceBase(AlFikrContext alFikrContext, ILogger<T> logger) : this(alFikrContext)
	{
		this._logger = logger;
	}

	public IDbContextTransaction BeginTransaction()
	{
		return alFikrContext.Database.BeginTransaction();
	}

	public async Task<IDbContextTransaction> BeginTransactionAsync()
	{
		return await alFikrContext.Database.BeginTransactionAsync();
	}

	public int SaveContextChanges()
	{
		if (alFikrContext.ChangeTracker.HasChanges())
		{
			return alFikrContext.SaveChanges();
		}

		return 0;
	}
}
